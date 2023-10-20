using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace LABO_Tools.Token
{

    /// <summary>
    /// Classe d'assistance pour générer des jetons JWT (JSON Web Tokens) utilisés pour l'authentification.
    /// </summary>

    public static class GenerateTokenHandler
    {

        /// <summary>
        /// Génère un jeton JWT contenant les revendications (claims) spécifiées, une date d'expiration et une signature.
        /// </summary>
        /// <param name="name">Nom associé au jeton (généralement l'identifiant de l'utilisateur).</param>
        /// <returns>Une chaîne de caractères représentant le jeton JWT généré.</returns>
 
        public static string GenerateToken(string identifiant)
        {
            var token = new JwtSecurityToken(claims: new Claim[]
            {
                new Claim(ClaimTypes.Name, identifiant)
            },
            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
            signingCredentials: new SigningCredentials(TokenHelper.SIGNING_KEY, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}