

using Application.Entities;
using Microsoft.AspNetCore.Identity;

namespace Web.Tools;

public class Hasher {
    
    public static string Hash(User user) {
        PasswordHasher<User> hasher = new ();
        return hasher.HashPassword(user, user.Password);
    }

    public static bool Verify(User user, string password) {
        PasswordHasher<User> hasher = new ();
        return hasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success;
    }

}