using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPAPI.Models;

namespace ASPAPI.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private readonly IMapper _mapper;
    // private IMapper _mapper { get; }

    private readonly DataContext _context;

    public CharacterService(IMapper mapper, DataContext context)
    {
      _mapper = mapper;
      _context = context;
    }
  
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newC)
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      var character = _mapper.Map<Character>(newC);
      var dbCharacters = await _context.Characters.ToListAsync();
      // Console.WriteLine($"{dbCharacters} ,{dbCharacters.Count}999999");
      
      character.Id = dbCharacters.Count>0 ?dbCharacters.Max(c => c.Id) + 1:1;
      dbCharacters.Add(character);
        // 向数据库中添加新角色
     _context.Characters.Add(character);
        // 保存更改到数据库
      await _context.SaveChangesAsync();
      serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return serviceResponse;
    }

   public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
   {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>> ();
      var dbCharacters = await _context.Characters.ToListAsync();      
      try
      {
        var character = dbCharacters.FirstOrDefault(c => c.Id == id);
        if(character is null)
          {
            throw new Exception($"Id {id} not found");
          }else{
            dbCharacters.Remove(character);
          _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
           }
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }   
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();
      var dbCharacters = await _context.Characters.ToListAsync();
        var character =dbCharacters.FirstOrDefault(c => c.Id == id);
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(character) ;

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters()
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      var dbCharacters = await _context.Characters.ToListAsync();
      serviceResponse.Data = dbCharacters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto newC)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
      var dbCharacters = await _context.Characters.ToListAsync();
      try
      {
        var character = dbCharacters.FirstOrDefault(c => c.Id == newC.Id);
        if (character is null)
        {
          throw new Exception($"Character with ID {newC.Id} not found");
        }
         _mapper.Map(newC, character);

        await _context.SaveChangesAsync();
        //response the dto, instead of the original data
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }
  }
}
