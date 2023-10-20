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
    public class AdminProjetController : ControllerBase
    {
        #region Dependancy injection

        #region Fields


        private readonly ProjetRepo _projetRepo;


        #endregion


        public AdminProjetController(ProjetRepo projetRepo)
        {
            _projetRepo = projetRepo;
        }


        #endregion


        /// <summary>
        /// Récupère la liste des projets.
        /// </summary>
        /// <returns>La liste des projets.</returns>
        [HttpGet]  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Get()
        {
            var result = await _projetRepo.Get();

            if (result is not null)
                return Ok(result);

            return NoContent();
        }



        /// <summary>
        /// Récupère un projet par son ID.
        /// </summary>
        /// <param name="id">ID du projet à récupérer.</param>
        /// <returns>Les informations du projet.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Get(int id)
        {
            var result = await _projetRepo.GetById(id);

            if (result is not null)
                return Ok(result);

            return BadRequest();
        }




        /// <summary>
        /// Supprime un projet par son ID.
        /// </summary>
        /// <param name="id">ID du projet à supprimer.</param>
        /// <returns>Statut NoContent en cas de suppression réussie.</returns>
        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _projetRepo.Delete(id);

            if (result)
                return NoContent();

            return BadRequest();
        }
    }
}
