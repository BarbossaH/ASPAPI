using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPAPI.Models;
using ASPAPI.Dtos.CharacterDto;
namespace ASPAPI.Services.CharacterService
{
    public interface ICharacterService
    {
    Task<ServiceResponse< List<GetCharacterDto>>> GetCharacters();
    Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);

    Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto C);
    Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto newC);

    Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
  }
}