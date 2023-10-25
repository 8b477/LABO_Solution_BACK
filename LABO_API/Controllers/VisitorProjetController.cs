using LABO_DAL.Services.Interfaces;
using LABO_Tools.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [ServiceFilter(typeof(CancellationFilter))]
    public class VisitorProjetController : ControllerBase
    {

        #region Dependancy injection

        #region Fields

        private readonly IProjetService _projetService;

        #endregion

        public VisitorProjetController(IProjetService projetService)
        {
            _projetService = projetService;
        }

        #endregion



        /// <summary>
        /// Récupère la liste des projets.
        /// </summary>
        /// <returns>La liste des projets.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
        {
            var projetsDetails = await _projetService.GetAllProjetDetails(page,pageSize);

            if (projetsDetails.Any())
                return Ok(projetsDetails);

            return NoContent();
        }



        [HttpGet("{nameOfProject}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(string nameOfProject)
        {
            var projets = await _projetService.GetProjetDetails(nameOfProject);

            if (projets.Any())
                return Ok(projets);
            
            return NoContent();
        }


    }

}
