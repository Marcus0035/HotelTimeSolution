using System.Net;
using IPMasking;

namespace IPMaskingTests
{
    public class IpMaskingUtilsTests
    {
        [Theory]
        [InlineData("192.168.1.1/24")]
        [InlineData("0.0.0.0/0")]
        [InlineData("255.255.255.255/32")]
        [InlineData("10.10.10.10/8")]
        [InlineData("1.1.1.1/23")]
        public void IsValidIPAddress_ValidFormats_ReturnsTrue(string input)
        {
            Assert.True(IPMaskingUtils.IsValidIPAddress(input));
        }

        [Theory]
        [InlineData("256.0.0.1/24")]
        [InlineData("192.168.1/24")]
        [InlineData("192.168/24")]
        [InlineData("192.168.1.1")]
        [InlineData("1.1.1.1/33")]
        [InlineData("-1.1.1.1/24")]
        [InlineData("1.1.1.1/-1")]
        public void IsValidIPAddress_InvalidFormats_ReturnsFalse(string input)
        {
            Assert.False(IPMaskingUtils.IsValidIPAddress(input));
        }

        [Theory]
        [InlineData(24, "255.255.255.0")]
        [InlineData(23, "255.255.254.0")]
        [InlineData(16, "255.255.0.0")]
        [InlineData(8, "255.0.0.0")]
        [InlineData(32, "255.255.255.255")]
        [InlineData(0, "0.0.0.0")]
        public void GetMask_ValidPrefix_ReturnsCorrectMask(int prefix, string expectedMask)
        {
            var mask = IPMaskingUtils.GetMask(prefix);
            Assert.Equal(IPAddress.Parse(expectedMask), mask);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(33)]
        public void GetMask_InvalidPrefix_ThrowsException(int prefix)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => IPMaskingUtils.GetMask(prefix));
        }

        [Theory]
        [InlineData("192.168.1.10/24", "192.168.1.10")]
        [InlineData("10.0.0.1/8", "10.0.0.1")]
        public void SeparateIPAddress_ReturnsIPAddress(string input, string expected)
        {
            var output = IPMaskingUtils.SeparateIPAddress(input);
            Assert.Equal(IPAddress.Parse(expected), output);
        }

        [Theory]
        [InlineData("192.168.1.10/24", 24)]
        [InlineData("10.0.0.1/8", 8)]
        public void SeparatePrefix_ReturnsPrefix(string input, int expectedPrefix)
        {
            Assert.Equal(expectedPrefix, IPMaskingUtils.SeparatePrefix(input));
        }

        [Theory]
        [InlineData("192.168.1.10", "192.168.1.20", "255.255.255.0", true)]
        [InlineData("192.168.1.10", "192.168.2.10", "255.255.255.0", false)]
        [InlineData("10.0.0.1", "10.0.255.255", "255.255.0.0", true)]
        [InlineData("10.0.0.1", "10.1.0.1", "255.255.0.0", false)]
        public void IsInSameSubnet_ReturnsExpectedResult(string ip1, string ip2, string mask, bool expected)
        {
            var ipAddress1 = IPAddress.Parse(ip1);
            var ipAddress2 = IPAddress.Parse(ip2);
            var maskAddress = IPAddress.Parse(mask);

            var result = IPMaskingUtils.IsInSameSubnet(ipAddress1, ipAddress2, maskAddress);

            Assert.Equal(expected, result);
        }
    }
}
