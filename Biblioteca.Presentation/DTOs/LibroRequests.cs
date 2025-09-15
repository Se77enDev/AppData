namespace Biblioteca.Presentation.DTOs;
public record LibroRequest(
    string Titulo,
    string Autor,
    int Año,
    string Categoria);

public record PrestamoRequest(
    int UsuarioId,
    int LibroId);

public record UsuarioRequest(
    string Nombre,
    string Tipo);