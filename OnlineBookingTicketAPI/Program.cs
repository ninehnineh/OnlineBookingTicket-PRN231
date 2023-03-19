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
using Repository.ShowSeatRepository;
using Repository.CinemaHallRepository;
using Repository.CinemaSeatRepository;
using Repository.BookingRepository;
using Repository.MovieShowRepository;
using Microsoft.OpenApi.Models;
using Repository.AuthenticationRepository;
using DataAccess;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;





var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));


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
builder.Services.AddScoped<IShowSeatRepository, ShowSeatRepository>();
builder.Services.AddScoped<ICinemaHallRepository, CinemaHallRepository>();
builder.Services.AddScoped<ICinemaSeatRepository, CinemaSeatRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IMovieShowRepository, MovieShowRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

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


builder.Services.AddControllers().AddOData(options =>
options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100)
.AddRouteComponents("odata", GetEdmModel()));

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Cinema>("Cinemas");
    builder.EntitySet<City>("Cities");
    builder.EntitySet<Movie>("Movies");
    builder.EntitySet<ShowSeat>("ShowSeats");
    builder.EntitySet<CinemaHall>("CinemaHalls");
    builder.EntitySet<CinemaSeat>("CinemaSeats");
    builder.EntitySet<Booking>("Bookings");
    builder.EntitySet<MovieShow>("MovieShows");
    return builder.GetEdmModel();
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                Enter 'Bearer' [space] and then your token in the text input below.
                \r\n\r\nExample: 'Bearer 12345avcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Final Project Api",
    });
});

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

builder.Services.AddAuthorization(op =>
{
    op.AddPolicy("RequireAdminRole", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});

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
