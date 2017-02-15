using Flirsty.Domain.Entities.Enums;

namespace Flirsty.Domain.Entities.ValueObjects
{
    public class ExternalKey
    {
        public string Key { get; set; }

        public ExternalSystemType Type { get; set; }
    }
}