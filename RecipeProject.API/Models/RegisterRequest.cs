using RecipeProject.Domain.Enums;

namespace RecipeProject.API.Models;

public record RegisterRequest(string Username, string Password, string Email, List<RoleEnum> roles);