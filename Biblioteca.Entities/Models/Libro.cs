using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entities.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        public int Año { get; set; }
        public required string Categoria { get; set; }
        public bool Disponible { get; set; } = true;
    }
}
