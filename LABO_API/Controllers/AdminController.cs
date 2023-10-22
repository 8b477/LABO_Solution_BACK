using LABO_DAL.DTO;
using LABO_DAL.Repositories;
using LABO_Tools.Filters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CancellationFilter))]
    [Authorize("RequireAdminRole")]
    public class AdminController : ControllerBase
    {
        #region Dependancy injection

        private readonly UserRepo _UserRepo;

        /// <summary>
        /// Initialise une nouvelle instance de la classe AdminController avec l'injection de dépendance de UserRepo.
        /// Ajoute aussi un système de log
        /// </summary>
        /// <param name="userRepo">Instance de UserRepo pour interagir avec les données utilisateur.</param>
        public AdminController(UserRepo userRepo)
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // =====> NICE HAVE : VOIRE TOUTE LES INFOS

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
        /// Récupère un utilisateur par son ID.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à récupérer.</param>
        /// <returns>Les informations de l'utilisateur.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult?> Get(int id)
        {

            var result = await _UserRepo.GetById(id);

            if (result is not null)
                return Ok(result);

            return NotFound();
        }





        /// <summary>
        /// Supprime un utilisateur par son ID si UserRole = Admin.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à supprimer.</param>
        /// <returns>Statut NoContent en cas de suppression réussie.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult?> Delete(int id)
        {

            //Récupère l'id de la personne préalablement connecter
            string? identifiant = HttpContext?.Items["identifiant"]?.ToString();
            int identifiantInt = int.Parse(identifiant!);

            UserDTOList? user = await _UserRepo.GetById(identifiantInt);

            if (user is not null && user.UserRole == "Admin")
            {
                bool result = await _UserRepo.Delete(id);
                if (result)
                    return NoContent();
            }
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

        public async Task<IActionResult?> Put([FromRoute] int id, [FromBody] UserDTOCreate model)
        {

            UserDTO? user = _UserRepo.ToModelCreate(model);

            if (user is not null)
            {
                var result = await _UserRepo.Update(id, user);

                if (result is not null)
                    return Ok(model);
            }

            return BadRequest();
        }




        /// <summary>
        /// Permet d'accéder à son profil personnel si l'utilisateur est connecté.
        /// </summary>
        /// <returns>Retourne le profil de l'utilisateur sous forme de UserDTO</returns>
        [ServiceFilter(typeof(JwtUserIdentifiantFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("Profil")]

        public async Task<IActionResult> Profil()
        {
            //Récupère l'id de la personne préalablement connecter
            string? identifiant = HttpContext?.Items["identifiant"]?.ToString();
            int id = int.Parse(identifiant!);

            UserDTOList? user = await _UserRepo.GetById(id);

            if (user is not null)
                return Ok(user);

            return Unauthorized();
        }
    }
}
