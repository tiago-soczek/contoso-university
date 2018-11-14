using System;
using Zek.Model;

namespace Contoso.University.Model.AccessControl
{
    public class UserOperation : BaseEntity
    {
        public User User { get; set; }
        public string OperationName { get; set; }
        public string Request { get; set; }
        public bool Success { get; set; }
        public DateTimeOffset StartedAt { get; set; }
        public DateTimeOffset FinishedAt { get; set; }
        public long Duration { get; set; }
        public string CorrelationId { get; set; }
    }
}