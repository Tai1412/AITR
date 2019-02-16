using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;//sql
using System.Configuration; //webconfig

namespace AITR
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["currentStaff"] != null)
            {
                Response.Redirect("~/SearchPage");
            }
        }

        protected void RegisterRedirect(object sender, EventArgs e)
        {
            Response.Redirect("~/Register");
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = OpenSqlConnection())
            {
                try
                {
                    string query = "SELECT count(*) FROM Staff WHERE staffId='" + staffIdTextBox.Text + "'AND staffPassword='" + staffPasswordTextBox.Text + "'";
                    SqlCommand command = new SqlCommand(query, connection);
                    string checkOutPut = command.ExecuteScalar().ToString();
                    if (checkOutPut == "1")
                    {
                        //session
                        Session["currentStaff"] = staffIdTextBox.Text;
                        Response.Redirect("~/SearchPage");
                    }
                    else
                    {
                        validate.Text = "StaffId & Password Is not correct Try again..!!";
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    validate.Text = ex.Message;
                }
            }
        }
        private SqlConnection OpenSqlConnection()
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            return connection;
        }
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["AITRConnection"].ConnectionString;
        }
    }
}