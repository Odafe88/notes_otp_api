namespace notes_api_app.app.DTOs;

public class VerifyOtpDto
{
    public string Email { get; set; } = string.Empty;
    public string OtpCode { get; set; } = string.Empty;
}