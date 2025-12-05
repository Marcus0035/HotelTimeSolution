using Fujtajbl;
using Fujtajbl.Interfaces;
using Fujtajbl.Models;
using IPMasking;
using System.Net;

namespace UnitTests
{
    public class IPMaskingTests
    {
        private IPMaskingEngine engine = new();

        [Fact]
        public void IsInSameSubnet_ReturnsTrue_ForIPsInSameSubnet()
        {
            var ip1 = IPAddress.Parse("192.168.1.10");
            var ip2 = IPAddress.Parse("192.168.1.20");
            var mask = IPAddress.Parse("255.255.255.0");

            bool result = engine.IsInSameSubnet(ip1, ip2, mask);

            Assert.True(result);
        }

        [Fact]
        public void IsInSameSubnet_ReturnsFalse_ForIPsInDifferentSubnets()
        {
            var ip1 = IPAddress.Parse("192.168.1.10");
            var ip2 = IPAddress.Parse("192.168.2.10");
            var mask = IPAddress.Parse("255.255.255.0");

            bool result = engine.IsInSameSubnet(ip1, ip2, mask);

            Assert.False(result);
        }

        [Fact]
        public void IsInSameSubnet_WorksWithDifferentMask()
        {
            var ip1 = IPAddress.Parse("10.0.0.5");
            var ip2 = IPAddress.Parse("10.0.1.5");
            var mask = IPAddress.Parse("255.255.0.0");

            bool result = engine.IsInSameSubnet(ip1, ip2, mask);

            Assert.True(result);
        }

        [Fact]
        public void IsInSameSubnet_ReturnsFalse_WhenMaskDoesNotMatch()
        {
            var ip1 = IPAddress.Parse("10.0.0.5");
            var ip2 = IPAddress.Parse("10.1.0.5");
            var mask = IPAddress.Parse("255.255.255.0");

            bool result = engine.IsInSameSubnet(ip1, ip2, mask);

            Assert.False(result);
        }

        [Fact]
        public void IsInSameSubnet_ThrowsException_ForDifferentIPLength()
        {
            var ip1 = IPAddress.Parse("192.168.1.10"); // IPv4
            var ip2 = IPAddress.Parse("2001:db8::1");  // IPv6
            var mask = IPAddress.Parse("255.255.255.0");

            Assert.Throws<ArgumentException>(() => engine.IsInSameSubnet(ip1, ip2, mask));
        }

        [Fact]
        public void IsInSameSubnet_WorksWithIPv6()
        {
            var ip1 = IPAddress.Parse("2001:db8::1");
            var ip2 = IPAddress.Parse("2001:db8::2");
            var mask = IPAddress.Parse("ffff:ffff:ffff:ffff::");

            bool result = engine.IsInSameSubnet(ip1, ip2, mask);

            Assert.True(result);
        }

    }
}
