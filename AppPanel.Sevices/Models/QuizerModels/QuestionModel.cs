using AppPanel.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPanel.Sevices.Models.QuizerModels
{
    public class QuestionModel
    {
        public int ID { get; set; }

        public int TestID { get; set; }

        public int QuestionNumber { get; set; }

        public string QuestionDesc { get; set; }

        public string AnsverA { get; set; }

        public string AnsverB { get; set; }

        public string AnsverC { get; set; }

        public string AnsverD { get; set; }

        public CorrectAnswer CorrectAnswer { get; set; }

        public string AnswerDesc { get; set; }
    }
}
