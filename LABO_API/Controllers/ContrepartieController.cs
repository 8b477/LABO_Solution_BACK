using LABO_DAL.DTO;
using LABO_DAL.Interfaces;
using LABO_DAL.Repositories;
using LABO_Tools.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CancellationFilter))]
    [Authorize("RequireRegisterRole")]
    [ApiController]
    public class ContrepartieController : ControllerBase
    {

        private readonly IContrepartieRepo _repoContrepartie;

        public ContrepartieController(IContrepartieRepo repoContrepartie)
        {
            _repoContrepartie = repoContrepartie;
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
