using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPMasking.Extensions
{
    public class MaskingExtension
    {
        private const string _notImplementedExceptionMessage = "Not Implemented Mask";
        public static IPAddress GetMask(Mask mask)
        {
            switch (mask)
            {
                case Mask.Mask8:
                    return IPAddress.Parse("255.0.0.0");
                case Mask.Mask16:
                    return IPAddress.Parse("255.255.0.0");
                case Mask.Mask24:
                    return IPAddress.Parse("255.255.255.0");
                default:
                    throw new NotImplementedException(_notImplementedExceptionMessage);
            }
        }

    }
}
