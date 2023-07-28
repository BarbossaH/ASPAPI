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
    public ICharacterService _characterService { get; }

    public CharacterController(ICharacterService characterService)
    {
      _characterService = characterService;
        
    }

    // [HttpGet]
    // [Route("GetAll")]
    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<Character>>>> GetCharacter()
    {
      return Ok(await _characterService.GetCharacters());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Character>>> GetSingle(int id)
    {
      return Ok(await _characterService.GetCharacterById(id));
    }
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<Character>>>> AddCharacter(Character newCharacter)
    {
      
      return Ok(await _characterService.AddCharacter(newCharacter));
    }
  }
}