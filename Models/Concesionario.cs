using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecuConcesionario.Models
{
    public class Concesionario{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConcesionarioId {get; set;}
        public string Marca {get; set;}
        public List<Stock> Smarka {get; } = new List<Stock>();
    }

    public class Modelo{
        [Key]
        public string ModeloId {get; set;}
        public int Importe {get; set;}
        public List<Stock> Mmarka {get; } = new List<Stock>();
    }

    public class Stock{
        [Key]
        public int id {get; set;}
        public int ConcesionarioId {get; set;}
        public string ModeloId {get; set;}
        public Concesionario Concesionario {get; set;}
        public Modelo Modelo {get; set;}
        public int Cantidad{get; set;}
    }
}