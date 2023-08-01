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
    Task<ServiceResponse< List<GetCharacterResDto>>> GetCharacters();
    Task<ServiceResponse<GetCharacterResDto>> GetCharacterById(int id);

    Task<ServiceResponse<List<GetCharacterResDto>>> AddCharacter(AddCharacterReqDto c);
  }
}