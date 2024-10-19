using System.Text;
using BddAPI.Data;
using BddAPI.Enum;
using BddAPI.Exceptions;
using BddAPI.Mapping;
using BddAPI.Repositories;
using BddAPI.Services;
using BddAPI.Services.Auth;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JWT");
var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetLink API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Mayor", policy => policy.RequireRole(RoleType.Mayor.ToString()))
    .AddPolicy("Janitor", policy => policy.RequireRole(RoleType.Janitor.ToString()))
    .AddPolicy("User", policy => policy.RequireRole(RoleType.User.ToString()))
    .AddPolicy("CityOfficial", policy => policy.RequireRole(RoleType.CityOfficial.ToString()));

builder.Services.AddDbContext<BddDbContext>(options =>
{
    options.UseSqlServer("name=ConnectionStrings:DefaultConnection");
});

FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("firebaseServiceAcc.json")
});

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("AllowAll",
            policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
    });

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllers(options => { options.Filters.Add<BddExeptionFilter>(); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(FirebaseAuth.DefaultInstance);
builder.Services.AddScoped<ICommunityHomeService, CommunityHomeService>();
builder.Services.AddScoped<ICommunityHomeRepository, CommunityHomeRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IContractService, ContractService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();