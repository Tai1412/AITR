using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITR
{
    public partial class CheckBoxQuestionControl : System.Web.UI.UserControl
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
        public CheckBoxList QuestionCheckBoxList
        {
            set
            {
                questionCheckBoxList = value;
            }
            get
            {
                return questionCheckBoxList;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}