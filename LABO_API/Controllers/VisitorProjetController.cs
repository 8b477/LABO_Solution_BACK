using LABO_DAL.Repositories;
using LABO_Tools.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CancellationFilter))]
    public class VisitorProjetController : ControllerBase
    {
        #region Dependancy injection

        #region Fields

        private readonly ProjetRepo _projetRepo;

        #endregion

        public VisitorProjetController(ProjetRepo projetRepo)
        {
            _projetRepo = projetRepo;
        }

        #endregion


        /// <summary>
        /// Récupère la liste des projets.
        /// </summary>
        /// <returns>La liste des projets.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _projetRepo.Get();

            if (result is not null)
            {
                var user = result.Select(x => _projetRepo.ToModelDisplay(x));
                return Ok(result);
            }

            return NoContent();
        }

    }

}
