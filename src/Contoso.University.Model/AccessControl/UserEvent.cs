using System;
using Zek.Model;

namespace Contoso.University.Model.AccessControl
{
    public class UserEvent : BaseEntity
    {
        public User User { get; set; }
        public string EventName { get; set; }
        public string Details { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Entity { get; set; }
        public int? EntityId { get; set; }
        public string CorrelationId { get; set; }
    }
}
