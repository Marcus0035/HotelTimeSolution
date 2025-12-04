using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMasking
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
