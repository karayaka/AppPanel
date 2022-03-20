
using System.ComponentModel.DataAnnotations;

namespace AppPanel.DAL.Enums
{
    public enum TestStatus
    {
        [Display(Name = "Hazır")]
        Ready = 1,
        [Display(Name = "Hazırlanıyor")]
        GettingReady = 0
    }
}
