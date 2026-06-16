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

//POST / api / users 
//Register a new user
app.MapPost("/api/users", (TravelLoggerDbContext db, UserDTO userDTO, IMapper mapper) =>
{
    var newUser = mapper.Map<User>(userDTO);
    db.Users.Add(newUser);
    db.SaveChanges();
    return Results.Created($"/api/users/{newUser.Id}", mapper.Map<UserDTO>(newUser));
});
// note: automap converts userDTO into a user model, gets added to db and saves. returns 201 when the user is mapped back to the DTO so it confirms it saved the new user information


//GET / api / users / signin /{ email}
//Sign in a user by email
app.MapGet("/api/users/signin/{email}", (TravelLoggerDbContext db, string email, IMapper mapper) =>
{
    var user = db.Users
    .Where(u => u.Email == email)
    .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
    .SingleOrDefault();
    return user != null ? Results.Ok(user) : Results.NotFound();
});
// note: searches users for one that has an email that matches, then it maps it to the UserDTO (it does this with ProjectTo method) then returns it. but returns 404 if theres no match


//GET /api/users/{id} 
//Get user profile, with all their logs and recommendations
app.MapGet("/api/users/{id}", (TravelLoggerDbContext db, int id, IMapper mapper) =>
{
    var user = db.Users
    .Where(u => u.Id == id)
    .Include(u => u.Logs)
    .Include(u => u.Recommendations)
    .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
    .SingleOrDefault();
    return user != null ? Results.Ok(user) : Results.NotFound();
});
// note: essentially the same concept as the /signin/{email} getter, but it locates the id and checks to find a match. uses .Include to also grab the users logs + recommendations associated with them in the database. .ProjectTo maps all of that info(including the nested collections) to the userDTO


//PUT /api/users/{id}
//Update user profile
app.MapPut("/api/users/{id}", (TravelLoggerDbContext db, int id, UserDTO userDTO, IMapper mapper) =>
{
    var user = db.Users
    .SingleOrDefault(u => u.Id == id);

    if (user == null)
    {
        return Results.NotFound();
    }

    user.Description = userDTO.Description;
    user.ImageUrl = userDTO.ImageUrl;
    db.SaveChanges();
    return Results.NoContent();
});
// note: finds existing user, or returns 404. ONLY directly updates the description + imageURL. cannot update email due to it being the login identifier. saves it. returns .NoContent 204 to say it worked


//GET /api/cities/{cityId}/ users
//List users by city (based on their most recent log)
app.MapGet("/api/cities/{cityId}/users", (TravelLoggerDbContext db, int cityId, IMapper mapper) =>
{
    return db.Users
        .Where(u => u.Logs
            .OrderByDescending(l => l.DateLogged)
            .FirstOrDefault().CityId == cityId)
        .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
        .ToList();
});
// note: finds users who have a log for at least 1 city. .Any checks the users logs to see if any have a log w a cityId that matches. returns that list. orderbydescending checks datelogged on all logs, so it can go by the most recent log for whatever user it brings back.


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
app.MapPost("/api/logs", (TravelLoggerDbContext db, IMapper mapper, LogDTO logDTO) =>
{
    Log log = mapper.Map<Log>(logDTO);
    db.Logs.Add(log);
    db.SaveChanges();

    Log created = db.Logs
        .Include(l => l.User)
        .Single(l => l.Id == log.Id);

    return Results.Created($"/api/logs/{created.Id}", mapper.Map<LogDTO>(created));
});

//PUT /api/logs/{id} - Update a log
app.MapPut("/api/logs/{id}", (TravelLoggerDbContext db, IMapper mapper, int id, LogDTO logDTO) =>
{
    Log log = db.Logs.SingleOrDefault(l => l.Id == id);
    if (log == null)
    {
        return Results.NotFound();
    }

    mapper.Map(logDTO, log);
    db.SaveChanges();
    return Results.NoContent();
});

//DELETE /api/logs/{id} - Delete a log
app.MapDelete("/api/logs/{id}", (TravelLoggerDbContext db, int id) =>
{
    Log log = db.Logs.SingleOrDefault(l => l.Id == id);
    if (log == null)
    {
        return Results.NotFound();
    }

    db.Logs.Remove(log);
    db.SaveChanges();
    return Results.NoContent();
});
//GET /api/users/{userId}/logs - List logs by user
app.MapGet("/api/users/{userId}/logs", (TravelLoggerDbContext db, IMapper mapper, int userId) =>
{
    return db.Logs
        .Where(l => l.UserId == userId)
        .ProjectTo<LogDTO>(mapper.ConfigurationProvider)
        .ToList();
});

//GET /api/cities/{cityId}/logs - List logs by city
app.MapGet("/api/cities/{cityId}/logs", (TravelLoggerDbContext db, IMapper mapper, int cityId) =>
{
    return db.Logs
        .Where(l => l.CityId == cityId)
        .ProjectTo<LogDTO>(mapper.ConfigurationProvider)
        .ToList();
});



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