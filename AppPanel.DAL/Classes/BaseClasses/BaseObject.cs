using AppPanel.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.Classes.BaseClasses
{
    public class BaseObject
    {
        public BaseObject()
        {
            CreatedDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
            CreatedBy = -1;
            LastModifiedBy = -1;
            ObjectStatus = ObjectStatus.NonDeleted;
            Status = Status.Active;
        }

        [Key]
        public int ID { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public int LastModifiedBy { get; set; }

        public ObjectStatus ObjectStatus { get; set; }

        public Status Status { get; set; }

    }
}
