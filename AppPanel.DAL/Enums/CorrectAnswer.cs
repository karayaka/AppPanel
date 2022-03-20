using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.Enums
{
    public enum CorrectAnswer
    {
        [Display(Name = "Cevap A")]
        AnsvwerA = 0,

        [Display(Name = "Cevap B")]
        AnsvwerB = 1,

        [Display(Name = "Cevap C")]
        AnsvwerC = 2,

        [Display(Name = "Cevap D")]
        AnsvwerD = 3,
       

    }
}
