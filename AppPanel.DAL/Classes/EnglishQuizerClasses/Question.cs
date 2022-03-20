using AppPanel.DAL.Classes.BaseClasses;
using AppPanel.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.Classes.EnglishQuizerClasses
{
    public class Question:BaseObject
    {
        public int TestID { get; set; }
        public Test Test { get; set; }

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
