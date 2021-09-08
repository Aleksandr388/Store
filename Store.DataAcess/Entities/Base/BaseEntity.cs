using Dapper.Contrib.Extensions;
using System;

namespace Store.DataAcess.Entities.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsRemoved { get; set; }

        public BaseEntity()
        {
            CreationDate = DateTime.Now;
        }
    }
}
