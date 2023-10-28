using LABO_DAL.DTO;
using LABO_DAL.Interfaces;
using LABO_Tools.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CancellationFilter))] // -> check si une contrepartie avec le meme nnom existe déjà en DB
    [Authorize("RequireRegisterRole")]
    [ApiController]
    public class ContrepartieController : ControllerBase
    {

        #region Constructor

        #region Fields

        private readonly IContrepartieRepo _repoContrepartie;

        #endregion


        public ContrepartieController(IContrepartieRepo repoContrepartie)
        {
            _repoContrepartie = repoContrepartie;
        }

        #endregion

        private int GetLoggedInUserId()
        {
            string? identifiant = HttpContext?.Items["identifiant"]?.ToString();

            if (int.TryParse(identifiant, out int id))
            {
                return id;
            }
            return 0;
        }


        [HttpPost]
        public async Task<IActionResult> Post(ContrepartiDTOCreate model)
        {
            int id = HttpContext.Session.GetInt32("ID") ?? 0; // je récup l'id du user connecter

            if(id == 0)
            {
                string? identifiant = HttpContext?.Items["identifiant"]?.ToString();
                id = int.Parse(identifiant);
            }

            if (id != 0)
            {
                ProjetDTO? projet = await _repoContrepartie.GetProjetByIdUser(id); //je récup le projet lié au user

                if(projet is not null && projet.EstValid == false)
                {
                    ContrepartieDTO contrepartie = new ()
                    {
                        IDProjet = projet.IDProjet,
                        Montant = model.Montant,
                        Description = model.Description
                    };
                    if(contrepartie != null)
                    {
                        if(await _repoContrepartie.Create(contrepartie))
                        return CreatedAtAction(nameof(Post),contrepartie);
                    }
                }
            }
            return Unauthorized();
        }
    }
}
