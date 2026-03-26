namespace notes_api_app.app.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsEmailVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class UpdateUserDto
{
    public string Email { get; set; } = string.Empty;
    public bool IsEmailVerified { get; set; }
}
