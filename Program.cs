//when using global keyword should be careful to avoid conflicts. Compiler must know the exact information.
global using ASPAPI.Models;
global using AutoMapper;
global using ASPAPI.Dtos.CharacterDto;
global using Microsoft.EntityFrameworkCore;
global using ASPAPI.Data;
global using ASPAPI.Dtos.CustomerDto;
using ASPAPI.Services.CharacterService;
using ASPAPI.Services.CustomerService;
using ASPAPI;
using Serilog;
using Microsoft.AspNetCore.RateLimiting;
using ASPAPI.Helper;
using Microsoft.AspNetCore.Authentication;
using ASPAPI.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ASPAPI.Services.RefreshTokenService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(
    options=>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


//AddScheme,AddCookie,AddJwtBearer,AddOAuth and so on, 
// builder.Services.AddAuthentication("BasicAuthentication")
// .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication",null);

var _authKey = builder.Configuration.GetValue<string>("JwtSetting:SecurityKey")??"thisispossiblekeyfromjulian";
builder.Services.AddAuthentication(item =>
{
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item => {
    item.RequireHttpsMetadata = true;
    item.SaveToken = true;
    item.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authKey)),
        ValidateIssuer = false,
        ValidateAudience=false,
        ClockSkew=TimeSpan.Zero
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddAutoMapper(typeof(Program).Assembly);
var autoMapper = new MapperConfiguration(item => item.AddProfile(new AutoMapperProfile()));
IMapper mapper = autoMapper.CreateMapper();
builder.Services.AddSingleton(mapper);

//cors
builder.Services.AddCors(policy => policy.AddDefaultPolicy( build => {
    build.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader();
 }));

#region 
// builder.Services.AddCors(policy => policy.AddPolicy("corsPolicy", build => {
//     build.WithOrigins("*")
//     .AllowAnyMethod()
//     .AllowAnyHeader();
//  }));

// builder.Services.AddCors(policy => policy.AddPolicy("corsPolicy1", build => {
//     build.WithOrigins("https://www.google.com", "https://www.google.com.hk")
//     .AllowAnyMethod()
//     .AllowAnyHeader();
//  }));

// builder.Services.AddCors(policy => policy.AddPolicy("corsPolicy2", build => {
//     build.WithOrigins("https://www.github.com", "https://www.sina.com")
//     .AllowAnyMethod()
//     .AllowAnyHeader();
//  }));
#endregion

//dependency injection
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IRefreshTokenHandler, RefreshTokenHandler>();

builder.Services.AddRateLimiter(_ => _.AddFixedWindowLimiter(policyName:"fixedWindow", options=>{
    options.Window = TimeSpan.FromSeconds(10);
    options.PermitLimit = 1;
    options.QueueLimit = 0;
    options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
}).RejectionStatusCode=401);

//serilog
string appRoot = AppDomain.CurrentDomain.BaseDirectory;
string defaultPath = Path.Combine(appRoot, "Logs", "API_Log.txt");
string logPath = builder.Configuration.GetSection("Logging:LogPath").Value ??defaultPath ;
// string logPath = builder.Configuration.GetSection("Logging:LogPath").Value  ;
var _logger = new LoggerConfiguration()
// .MinimumLevel.Debug()
.MinimumLevel.Information()
.MinimumLevel.Override("microsoft", Serilog.Events.LogEventLevel.Warning)
.Enrich.FromLogContext()
.WriteTo.File(logPath).CreateLogger();
builder.Logging.AddSerilog(_logger);

var _jwtSetting = builder.Configuration.GetSection("JwtSetting");
builder.Services.Configure<JwtSettings>(_jwtSetting);
// Console.WriteLine(_jwtSetting["SecurityKey"]+"99999");

var app = builder.Build();
app.UseRateLimiter();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseCors("corsPolicy1");
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
