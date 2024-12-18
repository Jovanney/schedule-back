using ScheduleApp.Domain.Entities;

namespace ScheduleApp.Application.Interfaces;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetAllAsync(string? search);
    Task<Contact?> GetByIdAsync(Guid id);
    Task AddAsync(Contact contact);
    Task UpdateAsync(Contact contact);
    Task DeleteAsync(Guid id);
}
