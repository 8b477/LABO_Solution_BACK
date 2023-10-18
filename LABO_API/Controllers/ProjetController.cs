using LABO_DAL.DTO;
using LABO_DAL.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize("RequireToken")]
    [ApiController]
    public class ProjetController : ControllerBase
    {

        private readonly ProjetRepo _projetRepo;

        public ProjetController(ProjetRepo projetRepo)
        {
            _projetRepo = projetRepo;
        }



        // GET: api/<ProjetController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _projetRepo.Get();

            if(result is not null) return Ok(result);

            return NoContent();
        }



        // GET api/<ProjetController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _projetRepo.GetById(id);

            if(result is not null) return Ok(result);

            return BadRequest();
        }



        // POST api/<ProjetController>
        [HttpPost]
        public IActionResult Post([FromBody] ProjetDTOCreate model)
        {

            ProjetDTO? user = _projetRepo.ToModelCreate(model);

            if(user is not null)
            {
                if(_projetRepo.Create(user)) return CreatedAtAction(nameof(Post), model);
            }
            return BadRequest();
        }



        // UN PROJET UNE FOIS CREER NE PEUT PLUS ETRE MODIFIER

        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] ProjetDTOCreate model)
        //{
        //    ProjetDTO? user = _projetRepo.ToModelCreate(model);

        //    if (user is not null)
        //    {
        //        var result = _projetRepo.Update(id, user);

        //        if (result is not null)  return Ok(model);
        //    }
        //    return BadRequest();
        //}



        // DELETE api/<ProjetController>/5
        [HttpDelete("{id}")]


        public IActionResult Delete(int id)
        {
            bool result = _projetRepo.Delete(id);

            if (result) return NoContent();

            return BadRequest();
        }


    }
}
