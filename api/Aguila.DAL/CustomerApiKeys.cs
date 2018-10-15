using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class CustomerApiKeys
    {
        public int Id { get; set; }
        public string SpireApiKey { get; set; }
        public string FitbitApiKey { get; set; }
    }
}
