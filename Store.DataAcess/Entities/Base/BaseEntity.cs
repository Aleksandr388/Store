using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAcess.Entities.Base
{
    public abstract class BaseEntity
    {
        public long? Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsRemoved { get; set; }

        public BaseEntity()
        {
            CreationDate = DateTime.Now;
        }
    }
}
