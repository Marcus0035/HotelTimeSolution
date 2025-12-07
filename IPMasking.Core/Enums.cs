using System.ComponentModel.DataAnnotations;

namespace IPMasking.Core
{
    public enum Mask : byte
    {
        [Display(Name = "/8")]
        Mask8 = 8,
        [Display(Name = "/16")]
        Mask16 = 16,
        [Display(Name = "/24")]
        Mask24 = 24
    }

}
