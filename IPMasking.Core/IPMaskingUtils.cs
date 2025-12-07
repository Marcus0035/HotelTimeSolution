using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace IPMasking
{
    public static class IPMaskingUtils
    {
        private const string _ipAddressMask = @"^((25[0-5]|2[0-4]\d|1?\d{1,2})\.){3}(25[0-5]|2[0-4]\d|1?\d{1,2})/(3[0-2]|[12]?\d)$";
        private const int _byteCount = 4;
        private static readonly Regex _ipRegex = new Regex(_ipAddressMask);

        public static bool IsValidIPAddress(string input) => _ipRegex.IsMatch(input);
        public static IPAddress SeparateIPAddress(string input) => IPAddress.Parse(input.Split('/')[0]);
        public static int SeparatePrefix(string input) => int.Parse(input.Split('/')[1]);

        public static bool IsInSameSubnet(IPAddress ip1, IPAddress ip2, IPAddress mask)
        {
            var ip1Bytes = ip1.GetAddressBytes();
            var ip2Bytes = ip2.GetAddressBytes();
            var maskBytes = mask.GetAddressBytes();

            for (var i = 0; i < _byteCount; i++)
            {
                if ((ip1Bytes[i] & maskBytes[i]) != (ip2Bytes[i] & maskBytes[i]))
                    return false;
            }

            return true;
        }
        public static IPAddress GetMask(int prefix)
        {
            if (prefix < 0 || prefix > 32)
                throw new ArgumentOutOfRangeException(nameof(prefix));

            var maskValue = prefix == 0 ? 0 : uint.MaxValue << (32 - prefix);

            var bytes = BitConverter.GetBytes(maskValue).Reverse().ToArray();
            return new IPAddress(bytes);
        }
    }
}