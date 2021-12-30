using BitstampApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyMusic.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitstampApp.Data
{
    public class ApplicationContext : DbContext
    {
 
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
 
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            //builder.Entity<User>().HasData(new User { Id = 123, Username = "sa", Password = "123", Status = true, CreationDate = DateTime.Now, UpdateDate = DateTime.Now });

            base.OnModelCreating(builder);

            builder
                .ApplyConfiguration(new UserConfiguration());


        }

        
    }
}
