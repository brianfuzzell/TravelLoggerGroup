using TravelLoggerGroup.Models;
using TravelLoggerGroup.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfiles>());

builder.Services.AddCors();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<TravelLoggerDbContext>(builder.Configuration["TravelLoggerDbConnectionString"]);

var app = builder.Build();
// Comment out HTTPS redirection for now to simplify testing
// app.UseHttpsRedirection();

// Configure Swagger for all environments during development
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

// Add all endpoints here



//User Endpoints

//POST / api / users - Register a new user

//GET / api / users / signin /{ email}

//Sign in a user by email

//GET /api/users/{id} -Get user profile, with all their logs and recommendations

//PUT /api/users/{id} -Update user profile

//GET /api/cities/{cityId}/ users - List users by city (based on their most recent log)



//City Endpoints

//GET /api/cities - List all cities
app.MapGet("/api/cities", (TravelLoggerDbContext db, IMapper mapper) =>
{
    return db.Cities.ProjectTo<CityDTO>(mapper.ConfigurationProvider).ToList();
});


//GET /api/cities/{id} -Get city details, with logs, users there, and recommendations
app.MapGet("/api/cities/{id}", (TravelLoggerDbContext db, IMapper mapper, int id) =>
{
    var city = db.Cities
    .Where(c => c.Id == id)
    .Include(c => c.Logs)
    .Include(c => c.Recommendations)
        .ThenInclude(r => r.User)
    .ProjectTo<CityDTO>(mapper.ConfigurationProvider)
    .SingleOrDefault();

    return city != null ? Results.Ok(city) : Results.NotFound();
});


//Log Endpoints

//POST /api/logs - Create a new log

//PUT / api / logs /{ id}


//Update a log

//DELETE /api/logs/{id} -Delete a log

//GET /api/users/{userId}/ logs - List logs by user

//GET /api/cities/{cityId}/ logs - List logs by city



//Recommendation Endpoints

//POST /api/recommendations - Create a new recommendation

//PUT / api / recommendations /{ id}


//Update a recommendation

//DELETE /api/recommendations/{id} -Delete a recommendation

//GET /api/cities/{cityId}/ recommendations - List recommendations by city

//GET /api/recommendations/{id} -Get recommendation details, including total number of upvotes



//Upvote Endpoints

//POST /api/upvotes - Add an upvote to a recommendation

app.Run();