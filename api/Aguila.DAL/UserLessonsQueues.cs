using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class UserLessonsQueues
    {
        public int Id { get; set; }
        public string AspNetUsers_Id { get; set; }
        public int? LessonsAndRulesId { get; set; }
    }
}
