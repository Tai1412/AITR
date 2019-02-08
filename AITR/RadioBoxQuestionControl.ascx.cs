using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITR
{
    public partial class RadioBoxQuestionControl : System.Web.UI.UserControl
    {
        public Label QuestionLabel
        {
            set
            {
                questionLabel = value;
            }
            get
            {
                return questionLabel;
            }
        }
        public RadioButtonList QuestionRadioList
        {
            set
            {
                questionRadioList = value;
            }
            get
            {
                return questionRadioList;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}