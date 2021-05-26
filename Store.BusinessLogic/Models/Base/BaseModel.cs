using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogic.Models.Base
{
    public class BaseModel
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsRemoved { get; set; }

        public BaseModel()
        {
            CreationDate = DateTime.Now;
        }
    }
}
