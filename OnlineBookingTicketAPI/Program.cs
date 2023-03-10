using BusinessObject;
using BusinessObject.Entities;
using DataAccess.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Repository.IRepository;
using Repository.Repository;
using System.Text;
using Repository.MovieRepository;

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Cinema>("Cinemas");
    builder.EntitySet<City>("Cities");
    builder.EntitySet<Movie>("Movies");
    builder.EntitySet<CinemaHall>("CinemaHalls");
    return builder.GetEdmModel();
}



var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

builder.Services.AddDbContext<OnlineBookingTicketDbContext>(options => {
    options.UseSqlServer(connectionString);
});
//AutoMapper
builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);
//Add Scoped repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICinemaRepository,CinemaRepository>();
builder.Services.AddScoped<ICityRepository,CityRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod());
});


builder.Services.AddIdentity<AppUsers, IdentityRole>()
    .AddEntityFrameworkStores<OnlineBookingTicketDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

builder.Services.AddControllers().AddOData(options => options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseODataBatching();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
