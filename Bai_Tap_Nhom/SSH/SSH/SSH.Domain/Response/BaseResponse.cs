using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Domain.Response
{
    public class BaseResponse
    {
        public int Result { get; set; }
        public string Message { get; set; }
    }
}
