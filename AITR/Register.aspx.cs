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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["userId"] == null)
            {
                Response.Redirect("~/QuestionPage");
            }
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = OpenSqlConnection())
            {
                try
                {
                    if(HttpContext.Current.Session["userId"] !=null)
                    {
                        string query = "UPDATE [User] SET givenName='" + giveNameTextBox.Text + "', lastName='" + lastNameTextBox.Text + "',age='" + ageTextBox.Text + "',phoneNumber='" + phoneNumberTextBox.Text
                                + "' where userId=" + HttpContext.Current.Session["userId"] +"" ;
                        SqlCommand command = new SqlCommand(query, connection);
                        int rowsAffected = command.ExecuteNonQuery();
                        Response.Redirect("~/RegisterCompletePage");
                    }
                    
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
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