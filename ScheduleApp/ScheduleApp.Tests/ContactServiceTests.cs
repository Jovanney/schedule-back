using Moq;
using FluentAssertions;
using ScheduleApp.Application.Interfaces;
using ScheduleApp.Application.Services;
using ScheduleApp.Application.DTOs;
using ScheduleApp.Domain.Entities;
using AutoMapper;

namespace ScheduleApp.Tests;

public class ContactServiceTests
{
    private readonly Mock<IContactRepository> _mockRepository;
    private readonly IMapper _mapper;
    private readonly ContactService _service;

    public ContactServiceTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Contact, ContactDto>();
            cfg.CreateMap<CreateContactDto, Contact>();
            cfg.CreateMap<UpdateContactDto, Contact>();
        });
        _mapper = configuration.CreateMapper();

        _mockRepository = new Mock<IContactRepository>();

        _service = new ContactService(_mockRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetAllContactsAsync_ShouldReturnListOfContacts()
    {
        var contacts = new List<Contact>
        {
            new Contact { Id = Guid.NewGuid(), Name = "John Doe", Email = "john@example.com", PhoneNumber = "12345" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(contacts);

        var result = await _service.GetAllContactsAsync();

        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(1);
        result.First().Name.Should().Be("John Doe");
    }

    [Fact]
    public async Task GetContactByIdAsync_ShouldReturnContact_WhenContactExists()
    {
        var contact = new Contact { Id = Guid.NewGuid(), Name = "Jane Doe", Email = "jane@example.com", PhoneNumber = "67890" };
        _mockRepository.Setup(repo => repo.GetByIdAsync(contact.Id)).ReturnsAsync(contact);

        var result = await _service.GetContactByIdAsync(contact.Id);

        result.Should().NotBeNull();
        result?.Name.Should().Be("Jane Doe");
    }

    [Fact]
    public async Task CreateContactAsync_ShouldAddContact()
    {
        var createContactDto = new CreateContactDto
        {
            Name = "Test User",
            Email = "test@example.com",
            PhoneNumber = "123456789"
        };

        var contact = _mapper.Map<Contact>(createContactDto);

        _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Contact>())).Returns(Task.CompletedTask);

        var result = await _service.CreateContactAsync(createContactDto);

        result.Should().NotBeNull();
        result.Name.Should().Be("Test User");
        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Contact>()), Times.Once);
    }

    [Fact]
    public async Task DeleteContactAsync_ShouldCallDelete()
    {
        var contactId = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.DeleteAsync(contactId)).Returns(Task.CompletedTask);

        await _service.DeleteContactAsync(contactId);

        _mockRepository.Verify(repo => repo.DeleteAsync(contactId), Times.Once);
    }
}
