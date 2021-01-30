using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentSystems.FrameWork
{
     public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message)
        {
        }

        public BusinessException(string format, params object[] args)
            : this(string.Format(format, args))
        {

        }
    }
}