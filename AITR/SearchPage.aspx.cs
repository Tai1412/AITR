using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITR
{
    public partial class SearchPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["currentStaff"]!=null)
            {
                currentStaff.Text = "Welcome " + Session["currentStaff"].ToString();
            }
        }   

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session.Remove("currentStaff");
            Response.Redirect("~/Login");
        }
    }
}