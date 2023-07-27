using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPAPI.Models;
namespace ASPAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController:ControllerBase
    {

    private static List<Character> characters = new List<Character>
    {
        new Character(),
        new Character{Id=1,Name="Xia"}
    };
    // [HttpGet]
    // [Route("GetAll")]
    [HttpGet("GetAll")]
    public ActionResult<List<Character>> GetCharacter()
    {
      return Ok(characters);
    }

    [HttpGet("{id}")]
    public ActionResult<List<Character>> GetSingle(int id)
    {
      return Ok(characters.FirstOrDefault(c=>c.Id==id));
    }
    [HttpPost]
    public ActionResult<List<Character>> AddCharacter(Character newCharacter)
    {
      characters.Add(newCharacter);
      return Ok(characters);
    }
  }
}