#define SQL_NO
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecuConcesionario.Models;

public class ConcesionarioContext : DbContext
{
    public static string connString { get; private set; } 
#if SQL //SQLServer
     = $"Server=localhost;Database=ConcesionarioDB;User Id=sa;Password=Pa88word;MultipleActiveResultSets=true";
#else //Windows
     = $"Server=(localdb)\\mssqllocaldb;Database=ConcesionarioDB;Trusted_Connection=True;MultipleActiveResultSets=true";
#endif
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(connString);

    //public CryptoContext(DbContextOptions<CryptoContext> options) : base(options) { }
    //public CryptoContext() : base() { }
    
    public DbSet<RecuConcesionario.Models.Concesionario> Concesionario { get; set; }

    public DbSet<RecuConcesionario.Models.Modelo> Modelo { get; set; }

    public DbSet<RecuConcesionario.Models.Stock> Stock { get; set; }
}