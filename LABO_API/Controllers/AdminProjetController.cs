using LABO_DAL.DTO;
using LABO_DAL.Interfaces;
using LABO_Tools.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CancellationFilter))]
    [ServiceFilter(typeof(JwtUserIdentifiantFilter))]
    [Authorize("RequireAdminRole")]
    public class AdminProjetController : ControllerBase
    {

        #region Dependancy injection

        #region Fields


        private readonly IProjetRepo _projetRepo;


        #endregion


        public AdminProjetController(IProjetRepo projetRepo)
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _projetRepo.Get();

                if (result is not null)
                    return Ok(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la récupération des projets. Source : " + ex.Source + " Message : " + ex.Message);
            }
        }



        /// <summary>
        /// Récupère un projet par son ID.
        /// </summary>
        /// <param name="id">ID du projet à récupérer.</param>
        /// <returns>Les informations du projet.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _projetRepo.GetById(id);

                if (result is not null)
                    return Ok(result);

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la récupération d'un projet via son id. Source : " + ex.Source + " Message : " + ex.Message);
            }
        }




        /// <summary>
        /// Met à jour un projet par son ID.
        /// </summary>
        /// <param name="id">ID du projet à mettre à jour.</param>
        /// <returns>Statut Ok() en cas de mise à jour réussi.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] ProjetDTOCreate model)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la mise à jour d'un projet. Source : " + ex.Source + " Message : " + ex.Message);
            }
        }



        /// <summary>
        /// Supprime d'un projet par son ID.
        /// </summary>
        /// <param name="id">ID du projet à suprrimer.</param>
        /// <returns>Retourne NoContent() si réussi.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _projetRepo.Delete(id);

                if (result)
                    return NoContent();

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la suppresion du projet par son ID. Source : " + ex.Source + " Message : " + ex.Message);
            }
        }
    }
}
