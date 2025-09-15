using Biblioteca.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entities.Models;
using Biblioteca.Data.Interfaces;

namespace Biblioteca.Business.Services;
public class LibroService : ILibroService
{
    private readonly ILibroRepository _libroRepository;

    public LibroService(ILibroRepository libroRepository)
    {
        _libroRepository = libroRepository;
    }

    public async Task<Libro> RegistrarLibroAsync(string titulo, string autor, int año, string categoria)
    {
        if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(autor))
            throw new ArgumentException("Título y autor son obligatorios");

        if (año < 1000 || año > DateTime.Now.Year)
            throw new ArgumentException("Año no válido");

        var libro = new Libro
        {
            Titulo = titulo,
            Autor = autor,
            Año = año,
            Categoria = categoria,
            Disponible = true
        };

        await _libroRepository.AddAsync(libro);
        return libro;
    }

    public async Task<IEnumerable<Libro>> ObtenerTodosLibrosAsync()
    {
        return await _libroRepository.GetAllAsync();
    }

    public async Task<bool> ConsultarDisponibilidadAsync(int libroId)
    {
        return await _libroRepository.IsAvailableAsync(libroId);
    }

    public async Task<IEnumerable<Libro>> ObtenerLibrosPorCategoriaAsync(string categoria)
    {
        return await _libroRepository.GetByCategoriaAsync(categoria);
    }
}
