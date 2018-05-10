using System;

namespace RulesService.Domain.Core
{
    public class AuditMetadata
    {
        public AuditMetadata(DateTime now)
        {
            this.DateCreated = now;
            this.DateModified = now;
        }

        public DateTime DateCreated { get; private set; }

        public DateTime DateModified { get; private set; }

        public void StampAsModified(DateTime now)
        {
            if (now < this.DateModified || now < this.DateCreated)
            {
                throw new ArgumentException(
                    FormattableString.Invariant(
                        $"Specified date is inferior to {nameof(this.DateCreated)} ({this.DateCreated}) or {nameof(this.DateModified)} ({this.DateModified})."), nameof(now));
            }

            this.DateModified = now;
        }
    }
}