using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITR
{
    public class Answer
    {
        string answerText;
        string ipAddress;
        int questionId;
        int optionId;

        public string AnswerText {
            get
            {
                return answerText;
            }
            set
            {
                answerText = value;
            }
        }
        public string IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
            }
        }
        public int QuestionId
        {
            get
            {
                return questionId;
            }
            set
            {
                questionId = value;
            }
        }
        public int OptionId
        {
            get
            {
                return optionId;
            }
            set
            {
                optionId = value;
            }
        }
    }
}