using System;
using Zek.Model;

namespace Contoso.University.Model.AccessControl
{
    public class User : BaseEntity
    {
        public static User System = new User
        {
            Id = 1,
            Username = "system",
            RegisteredAt = Current.Now
        };

        public string Username { get; set; }
        public DateTimeOffset RegisteredAt { get; set; }
    }
}
