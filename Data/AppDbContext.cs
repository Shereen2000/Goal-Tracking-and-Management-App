using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goal_Management_Web_App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Goal_Management_Web_App.Data
{
    public class AppDbContext: IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<ActionStep> Actions{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
         modelBuilder.Entity<Goal>()
            .ToTable("Goal")
            .HasOne(g => g.User)           
            .WithMany(u => u.Goals)        
            .HasForeignKey(g => g.UserId) 
            .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<ActionStep>().ToTable("ActionStep");
        }
    }
}