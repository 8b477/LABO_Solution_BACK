#region - Fixe - me viré la const et récup via .json
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;


//namespace LABO_Tools.Token
//{
//    /// <summary>
//    /// Création de notre clef secrete pour notre token.
//    /// </summary>
//    public class TokenHelper 
//    {

//        private readonly IConfiguration _config;

//        public TokenHelper(IConfiguration config)
//        {
//            _config = config;
//        }

//        public SymmetricSecurityKey GetSigningKey()
//        {
//            string secretKey = _config["Jwt:Key"];
//            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
//        }
//    }
//}

#endregion

using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace LABO_Tools.Token
{
    /// <summary>
    /// Création de notre clef secrete pour notre token.
    /// </summary>

    public class TokenHelper
    {
        private const string SECRET_KEY = "TQvgjeABMPOwCycOqah5EQu5yyVjpmVG";

        public static readonly SymmetricSecurityKey SIGNING_KEY = new
                               SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
    }
}