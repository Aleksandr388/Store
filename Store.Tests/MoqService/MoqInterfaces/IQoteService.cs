﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.MoqService.MoqInterfaces
{
    public interface IQoteService
    {
        Task<string> GenerateQuote();
    }
}
