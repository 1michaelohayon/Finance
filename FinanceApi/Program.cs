using FinanceApi.Models;
using FinanceApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<FinanceDatabaseSettings>(
    builder.Configuration.GetSection("FinanceDatabase"));

builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<LiabilitiesService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//cors
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAllOrigins",
      builder =>
      {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
      });
});

builder.Services.AddAuthentication()
     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
     {
       options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
       options.Audience = builder.Configuration["Auth0:Audience"];
       options.TokenValidationParameters =
         new Microsoft.IdentityModel.Tokens.TokenValidationParameters
         {
           ValidAudience = builder.Configuration["Auth0:Audience"],
           ValidIssuer = $"{builder.Configuration["Auth0:Domain"]}",

         };
     });







var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.MapControllers();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.Run();



