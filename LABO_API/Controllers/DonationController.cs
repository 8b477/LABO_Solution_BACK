using LABO_DAL.DTO;
using LABO_DAL.Interfaces;
using LABO_Tools.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LABO_API.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CancellationFilter))]
    [Authorize("RequireRegisterRole")]
    [ApiController]
    public class DonationController : ControllerBase
    {

        #region Constructor

        #region Fields
        private readonly IProjetRepo _projetRepo;
        private readonly IDonationRepo _donationRepo;
        private readonly IParticipantRepo _participantRepo;
        #endregion

        public DonationController(IProjetRepo projetRepo, IDonationRepo donationRepo, IParticipantRepo participantRepo)
        {
            _projetRepo = projetRepo;
            _donationRepo = donationRepo;
            _participantRepo = participantRepo;
        }
        #endregion


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(string nameOfproject, decimal montant)
        {
            try
            {
                int id = HttpContext.Session.GetInt32("ID") ?? 0;

                IEnumerable<ProjetDTO>? projet = await _projetRepo.GetByString(nameOfproject);
                int idProjet = projet.SingleOrDefault().IDProjet;

                if (idProjet != 0 && id != 0 && montant > 4)
                {
                    DonationDTO donationDTO = new()
                    {
                        IDProjet = idProjet,
                        IDUtilisateur = id,
                        DateDonation = DateTime.Now,
                        Montant = montant
                    };

                    bool isCreate = await _donationRepo.Create(donationDTO);
                    if (isCreate)
                    {
                        bool addParticipant = await _participantRepo.Create(new ParticipantDTO()
                        { IDUtilisateur = id, IDContrepartie = idProjet, DateParticipation = DateTime.Now});

                        return CreatedAtAction(nameof(Post), donationDTO);
                    }
                }
                return Unauthorized("Veuillez vous logg");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la création d'un don. Source : " + ex.Source + " Message : " + ex.Message);
            }
        }
    }

}
