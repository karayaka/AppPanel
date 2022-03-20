using AppPanel.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPanel.Sevices.Models.BaseModels
{
    public class ResultModel<T>
    {
        public ResultModel(
          T _Data,
          int _PageSize = 0,
          int _PageCount = 0,
          ResuldStatus _Type = ResuldStatus.succes,
          string _Message = null
          )
        {
            Type = _Type;
            Data = _Data;
            ResultDate = DateTime.Now;
            Message = _Message;
            PageSize = _PageSize;
            PageCount = _PageCount;
        }

        public DateTime ResultDate { get; set; }

        public T Data { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; set; }

        public ResuldStatus Type { get; set; }

        public string Message { get; set; }
    }
}
