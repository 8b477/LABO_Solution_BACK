using LABO_DAL.DTO;
using LABO_DAL.Repositories;
using LABO_Tools.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LABO_API.Controllers
    {
        [Route("api/[controller]")]
        [Authorize("RequireToken")]
        [ApiController]
        public class UserController : ControllerBase
        {

        #region Dependancy injection

        private readonly UserRepo _UserRepo;

        /// <summary>
        /// Initialise une nouvelle instance de la classe UserController avec l'injection de dépendance de UserRepo.
        /// </summary>
        /// <param name="userRepo">Instance de UserRepo pour interagir avec les données utilisateur.</param>
        public UserController(UserRepo userRepo)
        {
            _UserRepo = userRepo;

        }
        #endregion



        /// <summary>
        /// Récupère la liste des utilisateurs.
        /// </summary>
        /// <returns>La liste des utilisateurs.</returns>
            [HttpGet]
            [AllowAnonymous]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]

            public IActionResult Get()
            {
                var result = _UserRepo.Get();

            if (result is not null) return Ok(result);

                return BadRequest();
            }




            /// <summary>
            /// Récupère un utilisateur par son ID.
            /// </summary>
            /// <param name="id">ID de l'utilisateur à récupérer.</param>
            /// <returns>Les informations de l'utilisateur.</returns>
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]

            public IActionResult Get(int id)
            {
                var result = _UserRepo.GetById(id);

                if (result is not null) return Ok(result);
  
                return NotFound();
            }




            /// <summary>
            /// Crée un nouvel utilisateur.
            /// </summary>
            /// <param name="model">Modèle UserDTOCreate contenant les informations de l'utilisateur à créer.</param>
            /// <returns>Le modèle de l'utilisateur créé.</returns>
            [HttpPost]                  // --------------> TODO ADD CONTRAINTE EMAIL UNIQUE !!
            [AllowAnonymous]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]

            public IActionResult Post([FromBody] UserDTOCreate model)
            {
                UserDTO? user = _UserRepo.ToModelCreate(model);

                if (user is not null)
                {
                    if (_UserRepo.Create(user)) return CreatedAtAction(nameof(Post), model);
                }
                return BadRequest();
            }




            /// <summary>
            /// Supprime un utilisateur par son ID.
            /// </summary>
            /// <param name="id">ID de l'utilisateur à supprimer.</param>
            /// <returns>Statut NoContent en cas de suppression réussie.</returns>
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]

            public IActionResult Delete(int id)
            {
                bool result = _UserRepo.Delete(id);

                if (result) return NoContent();

                return BadRequest();
            }




            /// <summary>
            /// Met à jour un utilisateur par son ID.
            /// </summary>
            /// <param name="id">ID de l'utilisateur à mettre à jour.</param>
            /// <param name="model">Modèle UserDTOCreate contenant les informations mises à jour de l'utilisateur.</param>
            /// <returns>Le modèle de l'utilisateur mis à jour.</returns>
            [HttpPut("{id:int}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]

            public IActionResult Put([FromRoute] int id, [FromBody] UserDTOCreate model)
            {
                UserDTO? user = _UserRepo.ToModelCreate(model);

                if (user is not null)
                {
                    var result = _UserRepo.Update(id, user);

                    if (result is not null) return Ok(model);
                }
                return BadRequest();
            }




            [AllowAnonymous]
            [HttpPost("Log")]
            public IActionResult Get(UserDTORegister user)
            {

            if (_UserRepo.GetById(user.Email, user.MotDePasse))
                return new ObjectResult(GenerateTokenHandler.GenerateToken(user.Email));

            return BadRequest();

            }
    }
}
