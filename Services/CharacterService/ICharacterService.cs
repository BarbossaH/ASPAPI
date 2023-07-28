using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPAPI.Models;
namespace ASPAPI.Services.CharacterService
{
    public interface ICharacterService
    {
    Task<ServiceResponse< List<Character>>> GetCharacters();
    Task<ServiceResponse<Character>> GetCharacterById(int id);

    Task<ServiceResponse<List<Character>>> AddCharacter(Character c);
  }
}