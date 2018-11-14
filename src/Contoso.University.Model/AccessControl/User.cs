using System;
using Zek.Model;

namespace Contoso.University.Model.AccessControl
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public DateTimeOffset RegisteredAt { get; set; }
    }
}
