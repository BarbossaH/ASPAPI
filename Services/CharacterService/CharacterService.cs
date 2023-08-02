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

    public CharacterService(IMapper mapper)
    {
      _mapper = mapper;
    }
    public static List<Character> characters = new List<Character>{
        new Character(),
        new Character{Id=1,Name="Xia"}
  };
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newC)
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      var character = _mapper.Map<Character>(newC);
      character.Id = characters.Max(c => c.Id) + 1;
      characters.Add(character);
      serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return serviceResponse;
    }

   public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
   {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>> ();
      try
      {
          var character = characters.FirstOrDefault(c => c.Id == id);
        //if ensuring there must be an object, then First can be used, otherwise it will throw an error
        //var character = characters.First(c => c.Id == id);
        if(character is null)
          {
            throw new Exception($"Id {id} not found");
          }
            characters.Remove(character);
          serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
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

        var character =characters.FirstOrDefault(c => c.Id == id);
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(character) ;

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters()
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      serviceResponse.Data = characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto newC)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();

      try
      {
        var character = characters.FirstOrDefault(c => c.Id == newC.Id);
        if (character is null)
        {
          throw new Exception($"Character with ID {newC.Id} not found");
        }
        // _mapper.Map<Character>(newC); //this is ok too
        _mapper.Map(newC, character);
        character.Defense = newC.Defense;
        character.HitPoints = newC.HitPoints;
        character.Intelligence = newC.Intelligence;
        character.Name = newC.Name;
        character.Roles = newC.Roles;
        character.Strength = newC.Strength;

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
