using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.Enums
{
    public enum ResuldStatus:byte
    {
        [Display(Name = "Başarılı")]
        succes = 2,
        [Display(Name = "Uyarı")]
        warning = 1,
        [Display(Name = "Hata")]
        danger = 0
    }
}
