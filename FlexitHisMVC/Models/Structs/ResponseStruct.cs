﻿using System;
using System.Collections.Generic;

namespace FlexitHis_API.Models.Structs
{
    public class ResponseStruct<T>
    {
        public string requestToken { get; set; }
        public int status { get; set; }
        public List<T> data { get; set; }
    }
}
