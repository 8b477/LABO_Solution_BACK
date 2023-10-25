using LABO_DAL.DTO;
using LABO_DAL.Interfaces;
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

            private readonly IUserRepo _UserRepo;

            /// <summary>
            /// Initialise une nouvelle instance de la classe UserController avec l'injection de dépendance de UserRepo.
            /// Ajoute aussi un système de log
            /// </summary>
            /// <param name="userRepo">Instance de UserRepo pour interagir avec les données utilisateur.</param>
            public VisitorController(IUserRepo userRepo)
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
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> Get()
            {
            #region Test CancellationFilter + Log
            // await Task.Delay(10000);  // => Simule une longue attente  
            #endregion
                try
                {
                    var result = await _UserRepo.Get();

                    if (result is not null)
                    {
                        var users = result.Select(u => _UserRepo.ToModelDisplay(u)).ToList();
                        return Ok(users);
                    }
                    return NoContent();
                }

                catch (Exception ex)
                {
                    return StatusCode(500, "Une erreur s'est produite lors de la récupération des utilisateurs. Source :" + ex.Source);
                }
            }




            /// <summary>
            /// Crée un nouvel utilisateur.
            /// </summary>
            /// <param name="model">Modèle UserDTOCreate contenant les informations de l'utilisateur à créer.</param>
            /// <returns>Le modèle de l'utilisateur créé.</returns>
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)] //--> Test le côté unique de l'email ?
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> Post([FromBody] UserDTOCreate model)
            {
                try
                {
                    UserDTO? user = _UserRepo.ToModelCreate(model);

                    if (user is not null)
                    {
                        if (await _UserRepo.Create(user))
                            return CreatedAtAction(nameof(Post), model);
                    }           
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Une erreur s'est produite lors de l'insertion de l'utilisateur. Source :" + ex.Source);
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
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            [HttpPost(nameof(Logg))]
            public async Task<IActionResult> Logg(UserDTORegister model)
            {
                try
                {
                    UserDTO? user = await _UserRepo.Logger(model.Email, model.MotDePasse);

                    if (user is null)
                        return BadRequest();

                    //add some data in session
                    HttpContext.Session.SetInt32("ID", user.IDUtilisateur);
                    HttpContext.Session.SetString("Role", user.UserRole);

                    if (user.UserRole == "Visiteur")
                    {
                        user.UserRole = "Register";
                        UserDTO? upUser = await _UserRepo.Update(user.IDUtilisateur, user);

                        if (upUser is not null)
                            return new ObjectResult(GenerateTokenHandler.GenerateToken(upUser.IDUtilisateur.ToString(), upUser.UserRole));
                    }

                    return new ObjectResult(GenerateTokenHandler.GenerateToken(user.IDUtilisateur.ToString(), user.UserRole));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Une erreur s'est produite lors de la connexion. Source :" + ex.Source);
                }
            }

        }
    }
