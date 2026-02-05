using Application.Entities;
using Microsoft.AspNetCore.Identity;

namespace Web.Tools;

public class Hasher {
    public static string Hash(User user) {
        PasswordHasher<User> hasher = new PasswordHasher<User>();
        return hasher.HashPassword(user, user.Password);
    }

    public static PasswordVerificationResult Verify(User user, string hashedPassword) {
        PasswordHasher<User> hasher = new PasswordHasher<User>();
        return hasher.VerifyHashedPassword(user, hashedPassword, user.Password);
    }
}   