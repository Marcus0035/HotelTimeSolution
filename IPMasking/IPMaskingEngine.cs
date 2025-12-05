using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPMasking
{
    public class IPMaskingEngine
    {
        public bool IsInSameSubnet(IPAddress ip1, IPAddress ip2, IPAddress mask)
        {
            var ip1Bytes = ip1.GetAddressBytes();
            var ip2Bytes = ip2.GetAddressBytes();
            var maskBytes = mask.GetAddressBytes();

            if (ip1Bytes.Length != ip2Bytes.Length || ip1Bytes.Length != maskBytes.Length)
                throw new ArgumentException("IP and mask lengths do not match.");

            for (var i = 0; i < ip1Bytes.Length; i++)
            {
                if ((ip1Bytes[i] & maskBytes[i]) != (ip2Bytes[i] & maskBytes[i]))
                    return false;
            }

            return true;
        }
    }
}
