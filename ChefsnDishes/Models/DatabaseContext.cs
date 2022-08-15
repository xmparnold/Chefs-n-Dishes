#pragma warning disable CS8618
/* 
Disabled Warning: "Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable."
We can disable this safely because we know the framework will assign non-null values when it constructs this class for us.
*/
using Microsoft.EntityFrameworkCore;
namespace ChefsnDishes.Models;
// the MyContext class representing a session with our MySQL database, allowing us to query for or save data
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options) { }

    public DbSet<Chef> Chefs { get; set; }
    public DbSet<Dish> Dishes { get; set; }

    public List<Dish> CreatedDishes { get; set; } = new List<Dish>();
    
}