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
    [Authorize("RequireRegisterRole")]
    public class RegisterController : ControllerBase
    {

        #region Injection de dépendance

        #region Fields

        private readonly UserRepo _userRepo;

        #endregion

        #region Constructeur

        public RegisterController(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        #endregion

        #endregion


        private int GetLoggedInUserId()
        {
            string? identifiant = HttpContext?.Items["identifiant"]?.ToString();

            if (int.TryParse(identifiant, out int id))
            {
                return id;
            }

            return 0;
            // Gérez ici le cas où la conversion échoue, par exemple en renvoyant une valeur par défaut ou en levant une exception.
            // Vous pouvez personnaliser cette partie en fonction de votre logique.

            // Exemple : return -1; ou throw new Exception("Impossible de récupérer l'ID de l'utilisateur connecté.");
        }



        /// <summary>
        /// Récupère la liste des utilisateurs.
        /// </summary>
        /// <returns>La liste des utilisateurs.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _userRepo.Get();

            if (result is not null)
            {
                // Converti les utilisateurs pour masqué le champ MotDePasse (sans éffacer les datas)
                var users = result.Select(u => _userRepo.ToModelDisplay(u)).ToList();

                return Ok(users);

            }
            return BadRequest();
        }


        /// <summary>
        /// Recherche un utilisareur(s).
        /// </summary>
        /// <param name="id">le nom de l'utilisateur(s) à récupérer.</param>
        /// <returns>Les informations de l'utilisateur(s).</returns>
        [HttpGet(nameof(Search))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult?> Search([FromHeader] string name)
        {
            var result = await _userRepo.GetByString(name);

            if(result is not null)
            {
                IEnumerable<UserDTOList?> users = result.Select(u => _userRepo.ToModelDisplay(u)).ToList();

                return Ok(users);
            }

            return NotFound();
        }




        /// <summary>
        /// Supprime le compte de l'utilisateur.
        /// </summary>
        /// <returns>Statut NoContent en cas de suppression réussie.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult?> Delete()
        {
            //Récupère l'id de la personne préalablement connecter
            int id = GetLoggedInUserId(); // ----------------------------->FIXE ME je ne recupere pas l'id

            if(id != 0)
            {
                bool result = await _userRepo.Delete(id);
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
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult?> Put([FromBody] UserDTOCreate model)
        {
            //Récupère l'id de la personne préalablement connecter
            int id = GetLoggedInUserId(); // ----------------------------->FIXE ME je ne recupere pas l'id

            UserDTO? user = _userRepo.ToModelCreate(model);

            if (user is not null && id != 0)
            {
                var result = await _userRepo.Update(id, user);

                if (result is not null)
                    return Ok(model);
            }

            return BadRequest();
        }




        /// <summary>
        /// Permet d'accéder à son profil personnel quand si l'utilisateur est connecté.
        /// </summary>
        /// <returns>Retourne le profil de l'utilisateur sous forme de UserDTO</returns>
        [ServiceFilter(typeof(JwtUserIdentifiantFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("Profil")]
        public async Task<IActionResult> Profil()
        {
            //Récupère l'id de la personne préalablement connecter
            int id = GetLoggedInUserId();

            if(id != 0)
            { 
                UserDTOList? user = await _userRepo.GetById(id);

                if (user is not null)
                    return Ok(user);
            }

            return BadRequest();
        }
    }
}
