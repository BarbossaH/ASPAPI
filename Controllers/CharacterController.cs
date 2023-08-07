using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPAPI.Models;
using ASPAPI.Services.CharacterService;

namespace ASPAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController:ControllerBase
    {
    // public ICharacterService _characterService { get; }
    private readonly ICharacterService _characterService;

      public CharacterController(ICharacterService characterService)
      {
        _characterService = characterService;
          
      }

    // [HttpGet]
    // [Route("GetAll")]
    [HttpGet("GetAll")]
    // public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
    public async Task<IActionResult> GetAll()
    {
      return Ok(await _characterService.GetCharacters());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id)
    {
      return Ok(await _characterService.GetCharacterById(id));
    }
    [HttpPost("characters")]
    public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
    {
      
      return Ok(await _characterService.AddCharacter(newCharacter));
    }

    [HttpPut("characters/{id}")]
    public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto newC)
    {
      var response = await _characterService.UpdateCharacter(newC);
      if(response.Data is null){
        return NotFound(response);
      }
      return Ok(response);
    }

    [HttpDelete("characters/{id}")]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
      var response = await _characterService.DeleteCharacter(id);
      if(response.Data is null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }
  }
}