using ScheduleApp.Application.DTOs;

namespace ScheduleApp.Application.Interfaces;

public interface IContactService
{
    Task<IEnumerable<ContactDto>> GetAllContactsAsync();
    Task<ContactDto?> GetContactByIdAsync(Guid id);
    Task<ContactDto> CreateContactAsync(CreateContactDto contactDto);
    Task UpdateContactAsync(Guid id, UpdateContactDto contactDto);
    Task DeleteContactAsync(Guid id);
}
