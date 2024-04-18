
using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.Data.Models;

public sealed class Users
{
        [Key]
        public long Id { get; set; }

        public string Username { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }
}