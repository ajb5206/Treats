using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PierreTreats.Models 
{
  public class PierreTreatsContext: IdentityDbContext <ApplicationUser>
  {
    public DbSet<Flavor> Flavors { get; set;}
    public DbSet<Treats> Treats {get; set;}
    public DbSet<FlavorTreat> FlavorTreat { get; set;}
    public PierreTreatsContext(DbContextOptions options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}