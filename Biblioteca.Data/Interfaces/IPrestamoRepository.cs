using Biblioteca.Data.Interfaces;
using Biblioteca.Entities.Models;

public interface IPrestamoRepository : IRepository<Prestamo>
{

    Task<int> GetActiveLoansCountAsync(int usuarioId);
    Task<IEnumerable<Prestamo>> GetActiveLoansByUserAsync(int usuarioId);
    Task<IEnumerable<Prestamo>> GetLoansByUserAsync(int usuarioId);
    Task<IEnumerable<Prestamo>> GetLoanHistoryByBookAsync(int libroId);
    Task<bool> HasActiveLoanAsync(int usuarioId, int libroId);
    Task<IEnumerable<Prestamo>> GetOverdueLoansAsync(int diasLimite);
}