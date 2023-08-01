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
    public CharacterService(IMapper mapper)
    {
      _mapper = mapper;
    }
    public static List<Character> characters = new List<Character>{
        new Character(),
        new Character{Id=1,Name="Xia"}
  };
    public async Task<ServiceResponse<List<GetCharacterResDto>>> AddCharacter(AddCharacterReqDto newC)
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterResDto>>();
      var character = _mapper.Map<Character>(newC);
      character.Id = characters.Max(c => c.Id) + 1;
      characters.Add(character);
      serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterResDto>(c)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterResDto>> GetCharacterById(int id)
    {
      var serviceResponse = new ServiceResponse<GetCharacterResDto>();

        var character =characters.FirstOrDefault(c => c.Id == id);
        serviceResponse.Data = _mapper.Map<GetCharacterResDto>(character) ;

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterResDto>>> GetCharacters()
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterResDto>>();
      serviceResponse.Data = characters.Select(c=>_mapper.Map<GetCharacterResDto>(c)).ToList();
      return serviceResponse;
    }
  }
}
