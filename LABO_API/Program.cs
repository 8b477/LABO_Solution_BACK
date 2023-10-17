using LABO_DAL.Repositories;
using LABO_Tools.Middleware;
using LABO_Tools.Services;
using System.Data.SqlClient;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// *** AJOUT DE MES CLASS DE CONFIGURATION *******************************
SwaggerService.ConfigureSwagger(builder.Services);
AddAuthenticationService.ConfigureAuthentication(builder.Services);
AddAuthorizationService.ConfigureAuthorization(builder.Services);

// ********************** INJECTION DE DEPENDANCE ******************************
DependencyInjectionService.ConfigureDependencyInjection(builder.Services, builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//******************* MIDDLEWATR ******************** 
app.UseMiddleware<AuthorizeAllEndpointsMiddleware>();
//***************************************************

//******  ADD   ******* 
app.UseAuthentication();
app.UseAuthorization();
//*********************

app.MapControllers();
app.Run();




















//// ***************** AJOUT D UN TOKEN POUR L'AUTHENTICATION **************************
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddJwtBearer(options =>
//        {
//            options.TokenValidationParameters = new()
//            {
//                IssuerSigningKey = TokenHelper.SIGNING_KEY,
//                ClockSkew = TimeSpan.FromMinutes(5)
//            };
//        });
//// ***********************************************************************************


//// Ajoutez la stratégie d'autorisation qui exige un jeton JWT valide
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireToken", policy =>
//    {
//        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
//        policy.RequireAuthenticatedUser();
//    });
//});



//string connectionString = builder.Configuration.GetConnectionString("dev"); // récup valeur dans appsettings.json

//// Injecter la connexion par default, à chaque utilisation celui-ci sera connecter
//builder.Services.AddScoped<UserRepo>(provider => new UserRepo(new SqlConnection(connectionString)));
//builder.Services.AddScoped<ProjetRepo>(provider => new ProjetRepo(new SqlConnection(connectionString)));
////***************************************************************************************************