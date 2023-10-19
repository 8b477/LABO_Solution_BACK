using LABO_DAL.DTO;
using LABO_DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize("RequireToken")]
    [ApiController]
    public class ProjetController : ControllerBase
    {

        #region Dependancy injection

        #region Fields


        private readonly ProjetRepo _projetRepo;


        #endregion


        public ProjetController(ProjetRepo projetRepo)
        {
            _projetRepo = projetRepo;
        }


        #endregion


        /// <summary>
        /// Récupère la liste des projets.
        /// </summary>
        /// <returns>La liste des projets.</returns>
        [HttpGet]                           // --> 'NICE HAVE' : PEUT ETRE MASQUER ID_USER ET ID_PROJET
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Get()
        {
            var result = await _projetRepo.Get();

            if(result is not null) return Ok(result);

            return NoContent();
        }




        /// <summary>
        /// Récupère un projet par son ID.
        /// </summary>
        /// <param name="id">ID du projet à récupérer.</param>
        /// <returns>Les informations du projet.</returns>
        [HttpGet("{id}")]                // --> 'NICE HAVE' : CHERCHER VIA LE NOM DU PROJET
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Get(int id)
        {
            var result = await _projetRepo.GetById(id);

            if(result is not null) return Ok(result);

            return BadRequest();
        }



        /// <summary>
        /// Crée un nouveaux projet.
        /// </summary>
        /// <param name="model">Modèle projetDTOCreate contenant les informations du projet à créer.</param>
        /// <returns>Le modèle de l'utilisateur créé.</returns>
        [HttpPost]                              // --> 'NICE HAVE' : NOM DU PROJET UNIQUE 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Post([FromBody] ProjetDTOCreate model)
        {

            ProjetDTO? user = _projetRepo.ToModelCreate(model);

            if(user is not null)
            {
                if(await _projetRepo.Create(user))
                    return CreatedAtAction(nameof(Post), model);
            }
            return BadRequest();
        }




        /// <summary>
        /// Supprime un projet par son ID.
        /// </summary>
        /// <param name="id">ID du projet à supprimer.</param>
        /// <returns>Statut NoContent en cas de suppression réussie.</returns>
        [HttpPut("{id}")]              // --> 'NICE HAVE' : UN PROJET UNE FOIS VALIDER NE PEUT PLUS ETRE MODIFIER
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Put(int id, [FromBody] ProjetDTOCreate model)
        {
            ProjetDTO? user = _projetRepo.ToModelCreate(model);

            if (user is not null)
            {
                var result = await _projetRepo.Update(id, user);

                if (result is not null)
                    return Ok(model);
            }
            return BadRequest();
        }



        /// <summary>
        /// Met à jour d'un projet par son ID.
        /// </summary>
        /// <param name="id">ID du projet à mettre à jour.</param>
        /// <param name="model">Modèle projetDTOCreate contenant les informations mises à jour du projet.</param>
        /// <returns>Le modèle du projet mis à jour.</returns>
        [HttpDelete("{id}")]  // --> 'NICE HAVE' : LAISSER LA POSIBILITER DE SUPPRIMER ?
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _projetRepo.Delete(id);

            if (result) return NoContent();

            return BadRequest();
        }


    }
}
