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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(
    options=>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddAutoMapper(typeof(Program).Assembly);
var autoMapper = new MapperConfiguration(item => item.AddProfile(new AutoMapperProfile()));
IMapper mapper = autoMapper.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();


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
// if(!string.IsNullOrEmpty(logPath)){

// }


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
