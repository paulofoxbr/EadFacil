using System.Text;
using EadFacil.Api.Authorizations;
using EadFacil.Api.Configurations;
using EadFacil.Api.Data.DbContext;
using EadFacil.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddDbContext<LoginDbContext>(optionsAction: options =>
    {
        if (builder.Environment.IsDevelopment())
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
        }
        else 
        {   
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        }
    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LoginDbContext>();



#region Swagger Configuration

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MinhaApi", Version = "v1" });

    // Configuração do esquema de segurança Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

#endregion

#region JWT Authentication

var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettins>(jwtSettingsSection);
var jwtSettings = jwtSettingsSection.Get<JwtSettins>();
if (jwtSettings != null)
{
    var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidIssuer = jwtSettings.Issuer,
        };
    });
}

#endregion

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthorizationHandler, AlunoAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, FinanceiroAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ConteudoAuthorizationHandler>();
builder.Services.AddAuthorization(options => {
    options.AddPolicy("AdminPolicy", policy => policy.Requirements.Add(new FinanceiroAuthorizationRequirement()));
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
app.UseDbMigrationHelper();

app.Run();