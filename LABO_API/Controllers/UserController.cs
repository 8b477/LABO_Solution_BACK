using LABO_DAL.DTO;

using LABO_DAL.Repositories;

using Microsoft.AspNetCore.Mvc;




namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        #region Injection de dépendance

        private readonly UserRepo _UserRepo;

        public UserController(UserRepo userRepo)
        {
            _UserRepo = userRepo;
        } 

        #endregion

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {

            var result =_UserRepo.Get();

            if(result is not null)
            {
                return Ok(result);

            }
            return BadRequest();
        }



        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _UserRepo.GetById(id);

            if(result is not null)
            {
                return Ok(result);
            }
            return NotFound();
        }



        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDTOCreateModel model)
        {
            int result =_UserRepo.Create(model);


            if(result > 0)
            {
                return CreatedAtAction(nameof(Post), model);
            }

            return BadRequest();
        }




        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = _UserRepo.Delete(id);

            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }



        // PUT api/<UserController>/5
        [HttpPut("{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] UserDTOCreateModel model)
        {
            var result = _UserRepo.Update(id, model);

            if(result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
