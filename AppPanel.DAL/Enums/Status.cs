using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.Enums
{
    public enum Status:byte
    {
        [Display(Name = "Aktif")]
        Active = 1,

        [Display(Name = "Pasif")]
        Pasive = 0,
    }
}
