using AutoMapper;
using ScheduleApp.Application.DTOs;
using ScheduleApp.Application.Interfaces;
using ScheduleApp.Domain.Entities;

namespace ScheduleApp.Application.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _repository;
    private readonly IMapper _mapper;

    public ContactService(IContactRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

   public async Task<IEnumerable<ContactDto>> GetAllContactsAsync(string? search)
{
    var contacts = await _repository.GetAllAsync(search);

    if (!string.IsNullOrEmpty(search))
    {
        contacts = contacts.Where(c =>
            c.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
            c.Email.Contains(search, StringComparison.OrdinalIgnoreCase) ||
            c.PhoneNumber.Contains(search, StringComparison.OrdinalIgnoreCase));
    }

    return _mapper.Map<IEnumerable<ContactDto>>(contacts);
}


    public async Task<ContactDto?> GetContactByIdAsync(Guid id)
    {
        var contact = await _repository.GetByIdAsync(id);
        return _mapper.Map<ContactDto?>(contact);
    }

    public async Task<ContactDto> CreateContactAsync(CreateContactDto contactDto)
    {
        var contact = _mapper.Map<Contact>(contactDto);
        await _repository.AddAsync(contact);
        return _mapper.Map<ContactDto>(contact);
    }

    public async Task UpdateContactAsync(Guid id, UpdateContactDto contactDto)
    {
        var contact = await _repository.GetByIdAsync(id);
        if (contact == null) throw new Exception("Contact not found.");

        _mapper.Map(contactDto, contact);
        await _repository.UpdateAsync(contact);
    }

    public async Task DeleteContactAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
