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
        public TimeSpan Duration { get; set; }
        public string CorrelationId { get; set; }

        public static UserOperation StartNew()
        {
            return new UserOperation
            {
                StartedAt = Current.Now
            };
        }

        public void Finish()
        {
            FinishedAt = Current.Now;
            Duration = FinishedAt - StartedAt;
        }

        public void FinishAsError()
        {
            Success = false;
            Finish();
        }

        public void FinishAsSuccess()
        {
            Success = true;
            Finish();
        }
    }
}