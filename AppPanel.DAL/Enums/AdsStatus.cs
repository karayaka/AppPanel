using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.Enums
{
    public enum AdsStatus:byte
    {
        [Display(Name = "Reklamsız")]
        NoAds = 0,
        [Display(Name = "Getiç Reklamı")]
        InterstitialAd = 1,
        [Display(Name = "Ödüllü Reklam")]
        RewardedAd = 2
    }
}
