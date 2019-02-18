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
            List<int> optionIdSelected = new List<int>();
            // list gender 
            foreach (ListItem item in genderRadioButtonList.Items)
            {
                if (item.Selected)
                {
                    int convertedInt = Int32.Parse(item.Value.ToString());
                    optionIdSelected.Add(convertedInt);
                }
            }
            // list banks services
            foreach (ListItem item in bankServiceCheckBoxList.Items)
            {
                if (item.Selected)
                {
                    int convertedInt = Int32.Parse(item.Value.ToString());
                    optionIdSelected.Add(convertedInt);
                }
            }
            //list section of newspaper read
            foreach (ListItem item in sectionRadioButtonList.Items)
            {
                if (item.Selected)
                {
                    int convertedInt = Int32.Parse(item.Value.ToString());
                    optionIdSelected.Add(convertedInt);
                }
            }
            //list travel
            foreach (ListItem item in travelCheckBoxList.Items)
            {
                if (item.Selected)
                {
                    int convertedInt = Int32.Parse(item.Value.ToString());
                    optionIdSelected.Add(convertedInt);
                }
            }
            if (optionIdSelected.Count > 0)
                {
                    string thisIds = String.Join(",", optionIdSelected);
                    try
                    {

                        using (SqlConnection connection = OpenSqlConnection())
                        {
                            //Create query 
                            SqlCommand optionCommand = new SqlCommand("SELECT * FROM [User] WHERE userId IN(SELECT userId FROM SurveyAnswer WHERE optionId IN(" + thisIds + "))", connection);
                            //collect result
                            SqlDataReader optionReader = optionCommand.ExecuteReader();

                            List<User> listUserSearch = new List<User>();
                            //Read row
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

                                tableBodySearch.Controls.Clear();//clear when it load the newone
                                foreach (User user in listUserSearch)
                                {
                                tableBodySearch.Controls.Add(new LiteralControl("<tr>"));
                                tableBodySearch.Controls.Add(new LiteralControl("<td>" + user.GivenName + "</td>"));
                                tableBodySearch.Controls.Add(new LiteralControl("<td>" + user.LastName + "</td>"));
                                tableBodySearch.Controls.Add(new LiteralControl("<td>" + user.PhoneNumber + "</td>"));
                                tableBodySearch.Controls.Add(new LiteralControl("<td>" + user.Age + "</td>"));
                                tableBodySearch.Controls.Add(new LiteralControl("<td>" + user.UserIpAddress + "</td>"));
                                tableBodySearch.Controls.Add(new LiteralControl("</tr>"));//when 1 row retreive, the new data will retrieve new row.
                                }
                            }
                        }
                    warningTextBox.Text = "";
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
            else
            {
                warningTextBox.Text = "You have to choose the option to search";
            }
            }
        }
    }