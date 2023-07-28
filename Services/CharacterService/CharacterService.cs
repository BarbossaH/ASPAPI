using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPAPI.Models;

namespace ASPAPI.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    public static List<Character> characters = new List<Character>{
        new Character(),
        new Character{Id=1,Name="Xia"}
  };
    public async Task<ServiceResponse<List<Character>>> AddCharacter(Character c)
    {
      var serviceResponse = new ServiceResponse<List<Character>>();
      characters.Add(c);
      serviceResponse.Data = characters;
      return serviceResponse;
    }

    public async Task<ServiceResponse<Character>> GetCharacterById(int id)
    {
      var serviceResponse = new ServiceResponse<Character>();

        var character =characters.FirstOrDefault(c => c.Id == id);
        serviceResponse.Data = character;
      //   if(character is not null)   return character;
      // if(character!=null)   return character;
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<Character>>> GetCharacters()
    {
      var serviceResponse = new ServiceResponse<List<Character>>();
      serviceResponse.Data = characters;
      return serviceResponse;
    }
  }
}