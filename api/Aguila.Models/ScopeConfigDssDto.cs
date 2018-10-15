using System;

namespace Aguila.Models
{
    public partial class ScopeConfigDssDto
    {
        public Guid ConfigId { get; set; }
        public string ConfigData { get; set; }
        public string ScopeStatus { get; set; }
    }
}
