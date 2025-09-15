using Biblioteca.Data.Database;
using Biblioteca.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Entities.Models;

namespace Biblioteca.Data.Interfaces
{
    public interface ILibroRepository : IRepository<Libro>
    {
        Task<bool> IsAvailableAsync(int libroId);
        Task<IEnumerable<Libro>> GetByCategoriaAsync(string categoria);
    }
}
