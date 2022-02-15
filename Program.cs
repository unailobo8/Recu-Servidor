using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static System.Console;
using RecuConcesionario.Models;

namespace RecuConcesionario
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CrearBD();
            InsertarDatos();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
        static void CrearBD()
        {
            using (var db = new ConcesionarioContext())
            {
                bool deleted = db.Database.EnsureDeleted();
                WriteLine($"Database deleted: {deleted}");
                bool created = db.Database.EnsureCreated();
                WriteLine($"Database created: {created}");
            }
        }

        static void InsertarDatos()
        {
            using (var db = new ConcesionarioContext())
            {
                db.Concesionario.RemoveRange(db.Concesionario);
                db.Modelo.RemoveRange(db.Modelo);
                db.Stock.RemoveRange(db.Stock);
                db.SaveChanges();

                db.Concesionario.AddRange(
                    new Concesionario {ConcesionarioId = 1 , Marca = "Audi"},
                    new Concesionario {ConcesionarioId = 2 ,Marca = "Audi"},
                    new Concesionario {ConcesionarioId = 3 ,Marca = "Seat"},
                    new Concesionario {ConcesionarioId = 4 ,Marca = "Kia"},
                    new Concesionario {ConcesionarioId = 5 ,Marca = "Toyota"},
                    new Concesionario {ConcesionarioId = 6 ,Marca = "Toyota"}
                );
                db.SaveChanges();

                db.Modelo.AddRange(
                    new Modelo {ModeloId = "A8", Importe = 15000},
                    new Modelo {ModeloId = "A7", Importe = 12000},
                    new Modelo {ModeloId = "A5", Importe = 10000},
                    new Modelo {ModeloId = "Sportage", Importe = 11000},
                    new Modelo {ModeloId = "Sorento", Importe = 9000},
                    new Modelo {ModeloId = "Panda", Importe = 8000},
                    new Modelo {ModeloId = "Leon", Importe = 10000},
                    new Modelo {ModeloId = "Corolla", Importe = 16000},
                    new Modelo {ModeloId = "Yaris", Importe = 15000}
                );
                db.SaveChanges();

                db.Stock.AddRange(
                    new Stock {ConcesionarioId = 1 , ModeloId = "A8", Cantidad = 3},
                    new Stock {ConcesionarioId = 1 , ModeloId = "A7", Cantidad = 2},
                    new Stock {ConcesionarioId = 1 , ModeloId = "A5", Cantidad = 2},
                    new Stock {ConcesionarioId = 2 , ModeloId = "A8", Cantidad = 2},
                    new Stock {ConcesionarioId = 2 , ModeloId = "A7", Cantidad = 3},
                    new Stock {ConcesionarioId = 2 , ModeloId = "A5", Cantidad = 1},
                    new Stock {ConcesionarioId = 3 , ModeloId = "Panda", Cantidad = 2},
                    new Stock {ConcesionarioId = 3 , ModeloId = "Leon", Cantidad = 4},
                    new Stock {ConcesionarioId = 4 , ModeloId = "Sportage", Cantidad = 2},
                    new Stock {ConcesionarioId = 4 , ModeloId = "Sorento", Cantidad = 1},
                    new Stock {ConcesionarioId = 5 , ModeloId = "Corolla", Cantidad = 2},
                    new Stock {ConcesionarioId = 5 , ModeloId = "Yaris", Cantidad = 1},
                    new Stock {ConcesionarioId = 6 , ModeloId = "Corolla", Cantidad = 3},
                    new Stock {ConcesionarioId = 6 , ModeloId = "Yaris", Cantidad = 2}
                );
                db.SaveChanges();
            }
        }
    }
}
