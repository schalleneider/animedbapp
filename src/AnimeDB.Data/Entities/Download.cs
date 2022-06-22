using System;
using System.Collections.Generic;

namespace AnimeDB.Data.Entities
{
    public partial class Download
    {
        public long Id { get; set; }
        public long? KeyId { get; set; }
        public string Address { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedOn { get; set; }

        public virtual Media Key { get; set; }
    }
}
