using LABO_Tools.Middleware;
using LABO_Tools.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//********* AJOUT DE MES CLASS DE CONFIGURATION ***************************
SwaggerService.ConfigureSwagger(builder.Services);
AddAuthenticationService.ConfigureAuthentication(builder.Services);
AddAuthorizationService.ConfigureAuthorization(builder.Services);
//*************************************************************************



// ********************** INJECTION DE DEPENDANCE ******************************************************************
DependencyInjectionService.ConfigureDependencyInjection(builder.Services, builder.Configuration);
//******************************************************************************************************************



// ********************** CONFIG SERILOG ***************************************************
var configuration = new ConfigurationBuilder().AddJsonFile("serilogConfig.json").Build();

Log .Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Host.UseSerilog();
//******************************************************************************************


//******************ADD SESSION**************************
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "LABO.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.IsEssential = true;
});
//********************************************************

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
app.UseSession();
//*********************


app.MapControllers();
app.Run();