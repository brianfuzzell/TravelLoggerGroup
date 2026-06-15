using TravelLoggerGroup.Models;
using Microsoft.EntityFrameworkCore;


public class TravelLoggerDbContext : DbContext
{

    // Define tables here
    public DbSet<City> Cities {get;set;}
    public DbSet<Log> Logs {get;set;}
    public DbSet<Recommendation> Recommendations {get;set;}
    public DbSet<UpVote> UpVotes {get;set;}
    public DbSet<User> Users {get;set;}
    public TravelLoggerDbContext(DbContextOptions<TravelLoggerDbContext> context) : base(context)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data here
        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User { Id = 1, Email = "alex.nomad@email.com", Description = "Full-time traveler and remote developer.", ImageUrl = "https://i.pravatar.cc/150?img=1" },
            new User { Id = 2, Email = "sam.wanders@email.com", Description = "Coffee shop hunter and code writer.", ImageUrl = "https://i.pravatar.cc/150?img=2" },
            new User { Id = 3, Email = "jordan.roams@email.com", Description = "Mountains and meetups enthusiast.", ImageUrl = "https://i.pravatar.cc/150?img=3" },
            new User { Id = 4, Email = "riley.abroad@email.com", Description = "Chasing sunsets and stable WiFi.", ImageUrl = "https://i.pravatar.cc/150?img=4" },
            new User { Id = 5, Email = "morgan.fly@email.com", Description = "Design freelancer living out of a backpack.", ImageUrl = "https://i.pravatar.cc/150?img=5" },
            new User { Id = 6, Email = "casey.remote@email.com", Description = "SaaS founder. Currently in Southeast Asia.", ImageUrl = "https://i.pravatar.cc/150?img=6" },
            new User { Id = 7, Email = "taylor.globe@email.com", Description = "Writer and slow traveler.", ImageUrl = "https://i.pravatar.cc/150?img=7" },
            new User { Id = 8, Email = "drew.passport@email.com", Description = "Collecting visa stamps and bug reports.", ImageUrl = "https://i.pravatar.cc/150?img=8" },
        });

        modelBuilder.Entity<City>().HasData(new City[]
        {
            new City { Id = 1, Name = "Lisbon" },
            new City { Id = 2, Name = "Chiang Mai" },
            new City { Id = 3, Name = "Medellín" },
            new City { Id = 4, Name = "Bali" },
            new City { Id = 5, Name = "Budapest" },
            new City { Id = 6, Name = "Cape Town" },
            new City { Id = 7, Name = "Mexico City" },
            new City { Id = 8, Name = "Tbilisi" },
        });

        modelBuilder.Entity<Log>().HasData(new Log[]
        {
            new Log { Id = 1,  UserId = 1, CityId = 1, Comment = "Loved the tram rides and pastel de nata. Great coworking scene." },
            new Log { Id = 2,  UserId = 2, CityId = 2, Comment = "Super affordable and great expat community. Stayed three months." },
            new Log { Id = 3,  UserId = 3, CityId = 3, Comment = "The transformation of this city is remarkable. Very safe in El Poblado." },
            new Log { Id = 4,  UserId = 4, CityId = 4, Comment = "Canggu is chaotic but the vibes are unbeatable." },
            new Log { Id = 5,  UserId = 5, CityId = 5, Comment = "Ruin bars and thermal baths. Cheapest euro capital I've found." },
            new Log { Id = 6,  UserId = 6, CityId = 6, Comment = "Worked from a coffee shop overlooking Table Mountain. Surreal." },
            new Log { Id = 7,  UserId = 7, CityId = 7, Comment = "The food alone is worth the trip. Incredible city energy." },
            new Log { Id = 8,  UserId = 8, CityId = 8, Comment = "Totally underrated. Visa-free for 365 days for many passports." },
            new Log { Id = 9,  UserId = 1, CityId = 5, Comment = "Second time here. The coworking spaces got even better." },
            new Log { Id = 10, UserId = 3, CityId = 7, Comment = "Used it as a base for a month while working on a client project." },
        });

        modelBuilder.Entity<Recommendation>().HasData(new Recommendation[]
        {
            new Recommendation { Id = 1,  CityId = 1, UserId = 1, Name = "LX Factory",            Description = "Cool market and creative hub in a repurposed industrial space. Great on Sundays." },
            new Recommendation { Id = 2,  CityId = 1, UserId = 2, Name = "Second Home Lisbon",     Description = "Beautiful plant-filled coworking space. Day passes available." },
            new Recommendation { Id = 3,  CityId = 2, UserId = 2, Name = "CAMP",                   Description = "Best coworking hub in Chiang Mai. Fast internet and great community events." },
            new Recommendation { Id = 4,  CityId = 2, UserId = 3, Name = "Nimman Area",            Description = "Walkable neighborhood packed with cafes, gyms, and restaurants." },
            new Recommendation { Id = 5,  CityId = 3, UserId = 4, Name = "Selina Medellín",        Description = "Coliving and coworking in one place. Good for meeting other nomads." },
            new Recommendation { Id = 6,  CityId = 4, UserId = 5, Name = "Dojo Bali",              Description = "The go-to coworking spot in Canggu. Community vibe is excellent." },
            new Recommendation { Id = 7,  CityId = 5, UserId = 6, Name = "Széchenyi Thermal Bath", Description = "A must-do. Go on a weekday morning to avoid crowds." },
            new Recommendation { Id = 8,  CityId = 6, UserId = 7, Name = "Woodstock Exchange",     Description = "Creative hub with cafes and coworking. Great atmosphere for focused work." },
            new Recommendation { Id = 9,  CityId = 7, UserId = 8, Name = "Mercado de Coyoacán",    Description = "Authentic market. Try the tlayudas and fresh juice." },
            new Recommendation { Id = 10, CityId = 8, UserId = 1, Name = "Fabrika",                Description = "Soviet-era factory turned into a hip market, hostel, and creative space." },
        });

        modelBuilder.Entity<UpVote>().HasData(new UpVote[]
        {
            new UpVote { Id = 1, UserId = 2, RecommendationId = 1 },
            new UpVote { Id = 2, UserId = 3, RecommendationId = 1 },
            new UpVote { Id = 3, UserId = 4, RecommendationId = 3 },
            new UpVote { Id = 4, UserId = 1, RecommendationId = 6 },
            new UpVote { Id = 5, UserId = 5, RecommendationId = 7 },
            new UpVote { Id = 6, UserId = 6, RecommendationId = 9 },
            new UpVote { Id = 7, UserId = 7, RecommendationId = 2 },
            new UpVote { Id = 8, UserId = 8, RecommendationId = 10 },
        });
    }
}