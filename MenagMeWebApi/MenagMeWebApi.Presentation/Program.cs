using MenagMeWebApi.Application;
using MenagMeWebApi.Infrastructure;
using MenagMeWebApi.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
      .AllowAnyHeader()
      .AllowAnyMethod();
    });
});

//Services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MenagMe API", Version = "v1" });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});



builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseCors("Allow");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.InitialiseDatabaseAsync();
}

app.UseHttpsRedirection();


app.UseAuthentication();  
app.UseAuthorization();  

app.MapControllers();

app.Run();
