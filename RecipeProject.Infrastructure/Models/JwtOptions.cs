namespace RecipeProject.Infrastructure.Models;

public class JwtOptions
{
    public string Issuer { get; set; } = null!;
    public string Token { get; set; } = null!;
    public int ExpiresHours { get; set; }
}