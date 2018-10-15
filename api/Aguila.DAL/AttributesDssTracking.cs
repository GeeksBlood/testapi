using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class AttributesDssTracking
    {
        public int Id { get; set; }
        public int? UpdateScopeLocalId { get; set; }
        public int? ScopeUpdatePeerKey { get; set; }
        public long? ScopeUpdatePeerTimestamp { get; set; }
        public int LocalUpdatePeerKey { get; set; }
        public byte[] LocalUpdatePeerTimestamp { get; set; }
        public int? CreateScopeLocalId { get; set; }
        public int? ScopeCreatePeerKey { get; set; }
        public long? ScopeCreatePeerTimestamp { get; set; }
        public int LocalCreatePeerKey { get; set; }
        public long LocalCreatePeerTimestamp { get; set; }
        public int SyncRowIsTombstone { get; set; }
        public DateTime? LastChangeDatetime { get; set; }
    }
}
