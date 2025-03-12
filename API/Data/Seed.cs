using API.Enitites;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext dataContext)
        {
            if(await dataContext.Users.AnyAsync())
            {
                return;
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "UserSeedData.json");

            // ✅ Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found: " + filePath);
                return;
            }

            // ✅ Read the file content
            var userdata = await File.ReadAllTextAsync(filePath);
            Console.WriteLine("File Content: " + userdata);  // Debugging Output

            // ✅ Deserialize JSON into list of users
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<AppUser>>(userdata, options);

            // ✅ Check if users are successfully loaded
            if (users == null || users.Count == 0)
            {
                Console.WriteLine("Deserialization failed or no users found in JSON.");
                return;
            }

            // ✅ Insert users into database
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                Console.WriteLine($"Adding User: {user.UserName}");  // Debugging Output
                dataContext.Users.Add(user);
            }

            await dataContext.SaveChangesAsync();
            Console.WriteLine("Users added successfully!");
        }
    }
}
