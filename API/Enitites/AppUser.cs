﻿using API.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enitites
{
    public class AppUser
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[0];
        public  byte[] PasswordSalt { get; set; } = new byte[0];
        [NotMapped]
        public DateOnly DateOfBirth { get; set; }
        public required  string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public required string Gender { get; set; } 
        public string? Introduction { get; set; } 
        public string? Interests { get; set; } 
        public string? LookingFor { get; set; } 
        public required string City { get; set; } 
        public required string Country { get; set; }

        public List<Photo> photos { get; set; } 

        public int GetAge()
        {
            return DateOfBirth.CaluculateAge();
        }
        
    }
}
