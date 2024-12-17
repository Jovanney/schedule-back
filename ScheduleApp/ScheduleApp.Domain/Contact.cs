namespace ScheduleApp.Domain.Entities;

public class Contact
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public Contact(string name, string email, string phoneNumber)
    {
        Id = Guid.NewGuid(); 
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public Contact() { }
}
