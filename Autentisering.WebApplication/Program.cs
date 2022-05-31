using Autentisering.WebApplication.Backend;
using Autentisering.WebApplication.IdentityAndAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddRefitClient<IWeatherForecastApi>()
        .ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri("https://localhost:7170/");
        }
    );


builder.Services.AddRefitClient<IdentityApi>()
        .ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri("https://localhost:7134/");
        }
    );



builder.Services.AddAuthentication(option=> 
   {
       option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
       option.DefaultChallengeScheme= CookieAuthenticationDefaults.AuthenticationScheme;
       option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
   }).     
    AddCookie(option =>
    {
        option.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();