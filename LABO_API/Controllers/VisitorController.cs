using LABO_DAL.DTO;
using LABO_DAL.Repositories;
using LABO_Tools.Filters;
using LABO_Tools.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LABO_API.Controllers
    {
        [Route("api/[controller]")]
        [AllowAnonymous]
        [ServiceFilter(typeof(CancellationFilter))]
        [ApiController]
        public class VisitorController : ControllerBase
        {

            #region Dependancy injection

            private readonly UserRepo _UserRepo;

            /// <summary>
            /// Initialise une nouvelle instance de la classe UserController avec l'injection de dépendance de UserRepo.
            /// Ajoute aussi un système de log
            /// </summary>
            /// <param name="userRepo">Instance de UserRepo pour interagir avec les données utilisateur.</param>
            public VisitorController(UserRepo userRepo)
            {
                _UserRepo = userRepo;
            }
            #endregion



            /// <summary>
            /// Récupère la liste des utilisateurs.
            /// </summary>
            /// <returns>La liste des utilisateurs.</returns>
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]

            public async Task<IActionResult> Get()
            {
                // Test CancellationFilter + Log
                // await Task.Delay(10000);  // => Simule une longue attente 

                var result = await _UserRepo.Get();

                if (result is not null)
                {
                    // Converti les utilisateurs pour masqué le champ MotDePasse (sans éffacer les datas)
                    var users = result.Select(u => _UserRepo.ToModelDisplay(u)).ToList();

                    return Ok(users);
                
                }
            return BadRequest();
            }




            /// <summary>
            /// Crée un nouvel utilisateur.
            /// </summary>
            /// <param name="model">Modèle UserDTOCreate contenant les informations de l'utilisateur à créer.</param>
            /// <returns>Le modèle de l'utilisateur créé.</returns>
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]

            public async Task<IActionResult?> Post([FromBody] UserDTOCreate model)
            {

            UserDTO? user = _UserRepo.ToModelCreate(model);

                if (user is not null)
                {
                    if (await _UserRepo.Create(user)) 
                        return CreatedAtAction(nameof(Post), model);
                }
                return BadRequest();
            }




            /// <summary>
            /// Connecte un utilisateur si les datas son correct
            /// </summary>
            /// <param name="user">Modèle UserDTORegister pour une connection</param>
            /// <returns>Retourne un token avec des infos sur l'utilisateur</returns>
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [HttpPost(nameof(Logg))]

            public async Task<IActionResult> Logg(UserDTORegister model)
            {

            UserDTO? user = await _UserRepo.Logger(model.Email, model.MotDePasse);

            if(user is not null)
            {
                user.UserRole = "Register";

                return new ObjectResult(GenerateTokenHandler.GenerateToken(user.IDUtilisateur.ToString(),user.UserRole));
            }

            return BadRequest();
            }

    }
}
