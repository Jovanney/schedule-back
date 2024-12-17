using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScheduleApp.Application.DTOs;
using ScheduleApp.Infrastructure.Data;

namespace ScheduleApp.Tests;

// -------------------------------------------------------------------------------------
// IMPORTANT: Resolving Multiple Database Providers Conflict in .NET 9 Tests
// -------------------------------------------------------------------------------------
// In .NET 9, when using WebApplicationFactory for integration tests, the production 
// database provider (e.g., UseSqlServer) configured in Program.cs can conflict with 
// the test-specific database provider (e.g., UseInMemoryDatabase). 
//
// This causes the following error:
// "System.InvalidOperationException: Services for database providers 
// 'Microsoft.EntityFrameworkCore.SqlServer', 'Microsoft.EntityFrameworkCore.InMemory' 
// have been registered in the service provider."
//

// GITHUB ISSUE REFERENCE: https://github.com/dotnet/efcore/issues/35126
// -------------------------------------------------------------------------------------

public class ContactsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ContactsControllerTests(WebApplicationFactory<Program> factory)
    {

        var customFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {

                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ScheduleAppDbContext>));
                    
                if (dbContextDescriptor != null)
                {
                    services.Remove(dbContextDescriptor);
                }

             
                var dbContextTypeDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(ScheduleAppDbContext));
                if (dbContextTypeDescriptor != null)
                {
                    services.Remove(dbContextTypeDescriptor);
                }

              
                services.AddDbContext<ScheduleAppDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));

                
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ScheduleAppDbContext>();
                dbContext.Database.EnsureCreated();
            });
        });

        _client = customFactory.CreateClient();
    }

    [Fact]
    public async Task GetAllContacts_ReturnsEmptyList_WhenNoData()
    {
        // Act
        var response = await _client.GetAsync("/api/contacts");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<ContactDto>>();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task CreateContact_ReturnsCreatedContact()
    {
        // Arrange
        var createContactDto = new CreateContactDto
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "123456789"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/contacts", createContactDto);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdContact = await response.Content.ReadFromJsonAsync<ContactDto>();

        createdContact.Should().NotBeNull();
        createdContact?.Name.Should().Be("John Doe");
        createdContact?.Email.Should().Be("john.doe@example.com");
    }
}
