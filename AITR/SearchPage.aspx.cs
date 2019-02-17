using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient; //sql
using System.Configuration; //webconfig

namespace AITR
{
    public partial class SearchPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["currentStaff"] != null)
                {
                    currentStaff.Text = "Welcome " + Session["currentStaff"].ToString();
                }
                else
                {
                    Response.Redirect("~/Login");
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
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

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session.Remove("currentStaff");
            Response.Redirect("~/Login");
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            List<int> selectedOptionIds = new List<int>();
            // list banks services
            foreach (ListItem item in bankServiceCheckBoxList.Items)
            {
                if (item.Selected)
                {
                    int convertedInt = Int32.Parse(item.Value.ToString());
                    selectedOptionIds.Add(convertedInt);
                }
            }
                if (selectedOptionIds.Count > 0)
                {
                    string thisIds = String.Join(",", selectedOptionIds);
                    try
                    {

                        using (SqlConnection connection = OpenSqlConnection())
                        {
                            //Create query to get all options relating to current question
                            SqlCommand optionCommand = new SqlCommand("SELECT * FROM [User] WHERE userId IN(SELECT userId FROM SurveyAnswer WHERE optionId IN(" + thisIds + "))", connection);
                            //run command and collect results in reader
                            SqlDataReader optionReader = optionCommand.ExecuteReader();

                            List<User> listUserSearch = new List<User>();
                            //loop through results (Read 1 row at a time)
                            while (optionReader.Read())
                            {
                                User user = new User();
                            user.GivenName = optionReader["givenName"].ToString();
                            user.LastName = optionReader["lastName"].ToString();
                            user.PhoneNumber = optionReader["phoneNumber"].ToString();
                            user.Age = optionReader["age"].ToString();
                            user.UserIpAddress = optionReader["userIpAddress"].ToString();

                            listUserSearch.Add(user);
                            }

                            if (listUserSearch.Count > 0)
                            {

                                tableBodySearch.Controls.Clear();
                                foreach (User user in listUserSearch)
                                {
                                tableBodySearch.Controls.Add(new LiteralControl("<tr>"));
                                    string givenName = "Anomyous";
                                    if (user.GivenName != "")
                                    {
                                        givenName = user.GivenName;
                                    }
                                tableBodySearch.Controls.Add(new LiteralControl("<td>" + givenName + "</td>"));
                                tableBodySearch.Controls.Add(new LiteralControl("<td>" + user.LastName + "</td>"));
                                tableBodySearch.Controls.Add(new LiteralControl("<td>" + user.PhoneNumber + "</td>"));
                                tableBodySearch.Controls.Add(new LiteralControl("<td>" + user.Age + "</td>"));
                                tableBodySearch.Controls.Add(new LiteralControl("<td>" + user.UserIpAddress + "</td>"));
                                tableBodySearch.Controls.Add(new LiteralControl("</tr>"));
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }