using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppOptativa.Models;
using Microsoft.EntityFrameworkCore;

namespace AppOptativa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }

       public DbSet<Rol> Roles { get; set; }
      
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Proveedor>().ToTable("proveedores");
            modelBuilder.Entity<Productos>().ToTable("productos");
            modelBuilder.Entity<Categorias>().ToTable("categorias");
            modelBuilder.Entity<Rol>().ToTable("rol");
        }
    }
}