using System;
using System.Collections.Generic;

namespace AnimeDB.Data.Entities
{
    public partial class User
    {
        public User()
        {
            Personals = new HashSet<Personal>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string CreatedOn { get; set; }

        public virtual ICollection<Personal> Personals { get; set; }
    }
}
