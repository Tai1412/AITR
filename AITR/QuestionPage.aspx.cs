using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//extra namespaces
using System.Data.SqlClient;//sql
using System.Configuration; //webconfig

namespace AITR
{
    public partial class QuestionPage : System.Web.UI.Page
    {
        public static string SESSION_QUESTION_NUMBER = "questionNumber";
        public static string SESSION_EXTRA_QUESTIONS = "extraQuestions";
        protected void Page_Load(object sender, EventArgs e)
        {
             int currentQuestion = GetCurrentQuestionID();

            //2. Connect to DB
          

            using (SqlConnection connection = OpenSqlConnection())
            {

                //build sql command to get current question details
                SqlCommand command = new SqlCommand("SELECT * FROM SurveyQuestion,SurveyQuestionType WHERE SurveyQuestion.questionType = SurveyQuestionType.typeID AND SurveyQuestion.questionId=" + currentQuestion, connection);

                //run command and collect result in a reader
                SqlDataReader reader = command.ExecuteReader();

                //see if we have at least 1 result
                if (reader.Read())
                {
                    //read basic information about question
                    string questionText = (string)reader["qDescription"];
                    string questionType = reader["typeName"].ToString().ToLower();

                    //check what type of question it is and then loa up correct web user control
                    if (questionType.Equals("textbox"))
                    {
                        //load yp textbox question control template
                        TextBoxQuestionControl textBoxQuestion = (TextBoxQuestionControl)LoadControl("~/TextBoxQuestionControl.ascx");
                        //set ID
                        textBoxQuestion.ID = "textBoxQuestion";
                        //set question label to the current questions text from the DB
                        textBoxQuestion.QuestionLabel.Text = questionText;

                        //add to the page via place holder
                        QuestionPlaceHolder.Controls.Add(textBoxQuestion);

                    }
                    else if (questionType.Equals("checkbox"))
                    {
                        //load up control template
                        CheckBoxQuestionControl checkBoxQuestion = (CheckBoxQuestionControl)LoadControl("~/CheckBoxQuestionControl.ascx");

                        //we'll use this ID to find this check box question control again later to check which parts are selected
                        checkBoxQuestion.ID = "checkBoxQuestion";

                        checkBoxQuestion.QuestionLabel.Text = questionText;
                        //add items to the list

                        SqlCommand optionCommand = new SqlCommand("SELECT * FROM SurveyQuestionOption WHERE questionId=" + currentQuestion, connection);
                        //run command and collect results in reader
                        SqlDataReader optionReader = optionCommand.ExecuteReader();

                        //loop through results (Read 1 row at a time)
                        while (optionReader.Read())
                        {
                            //ListItem for every result params: display member(parts users see), value member(what its worth to us as developers, usually id from DB)
                            ListItem item = new ListItem(optionReader["oDescription"].ToString(), optionReader["optionId"].ToString());

                            checkBoxQuestion.QuestionCheckBoxList.Items.Add(item);
                        }

                        //add it to the webform via the placeholder
                        QuestionPlaceHolder.Controls.Add(checkBoxQuestion);
                    }
                    else if (questionType.Equals("radiobox"))
                    {
                        //load up control template
                        RadioBoxQuestionControl radioBoxQuestion = (RadioBoxQuestionControl)LoadControl("~/RadioBoxQuestionControl.ascx");

                        //we'll use this ID to find this radio box question control again later to check which parts are selected
                        radioBoxQuestion.ID = "radioBoxQuestion";

                        radioBoxQuestion.QuestionLabel.Text = questionText;
                        //add items to the list

                        SqlCommand optionCommand = new SqlCommand("SELECT * FROM SurveyQuestionOption WHERE questionId=" + currentQuestion, connection);
                        //run command and collect results in reader
                        SqlDataReader optionReader = optionCommand.ExecuteReader();

                        //loop through results (Read 1 row at a time)
                        while (optionReader.Read())
                        {
                            //ListItem for every result params: display member(parts users see), value member(what its worth to us as developers, usually id from DB)
                            ListItem item = new ListItem(optionReader["oDescription"].ToString(), optionReader["optionId"].ToString());

                            radioBoxQuestion.QuestionRadioList.Items.Add(item);
                        }

                        //add it to the webform via the placeholder
                        QuestionPlaceHolder.Controls.Add(radioBoxQuestion);
                    }
                }
                else
                {
                    //hopefully this situation does not happen, or assume end of survey and go to next page
                }
            }//connection.Close() using block calls Dispose method when done, dispose method for sqlConnection will close connection
        }
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["AITRConnection"].ConnectionString;
        }
        private int GetCurrentQuestionID()
        {
            using (SqlConnection connection = OpenSqlConnection())
            {
                //NOTE: dont default first question to 1, ask DB for min questionID in question table using SQL
                //1. get current question number client is up to
                SqlCommand currentQuestionCommand = new SqlCommand("SELECT MIN(questionId) FROM SurveyQuestion", connection);
                int currentQuestion = (int)currentQuestionCommand.ExecuteScalar();
                //if currentQuestion is previously stored in session, load it up
                if (HttpContext.Current.Session[SESSION_QUESTION_NUMBER] != null)
                    currentQuestion = (int)HttpContext.Current.Session[SESSION_QUESTION_NUMBER];
                else //not stored? store for first time
                    HttpContext.Current.Session[SESSION_QUESTION_NUMBER] = currentQuestion;
                return currentQuestion;
            }
        }
        private SqlConnection OpenSqlConnection()
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            return connection;
        }

        protected void NextButton_Click(object sender, EventArgs e)
        {
            int currentQuestion = GetCurrentQuestionID();
            string currentUserIpAddress = GetIPAddress();
            List<Answer> answers = getListOfAnswersFromSession();
            User u = new User();
            string anonymous = "Anonymous";
            u.UserIpAddress = currentUserIpAddress;
            u.GivenName = anonymous;

            //setup connection
            using (SqlConnection connection = OpenSqlConnection())
            {
                //Get extra questions list from session if its stored OR make a new one
                List<int> extraQuestions = new List<int>();
                if (HttpContext.Current.Session[SESSION_EXTRA_QUESTIONS] != null)
                    extraQuestions = (List<int>)HttpContext.Current.Session[SESSION_EXTRA_QUESTIONS];
                // Check what type of of question is loaded into the page and temporarily store answer in the session

                //try find textbox question stuff in page

                TextBoxQuestionControl textBoxQuestion = (TextBoxQuestionControl)QuestionPlaceHolder.FindControl("textBoxQuestion");//find looks for ID
                if (textBoxQuestion != null)
                {
                    string typeAnswer = textBoxQuestion.QuestionTextBox.Text;
                    Session["typeAnswer"]=textBoxQuestion.QuestionTextBox.Text;
                    Answer a = new Answer();
                    a.AnswerText = textBoxQuestion.QuestionTextBox.Text;
                    a.QuestionId = currentQuestion;
                    answers.Add(a);
                    HttpContext.Current.Session["answers"] = answers;
                        //TODO: store answer in session
                }
                //see if checkbox control on page
                CheckBoxQuestionControl checkBoxQuestion = (CheckBoxQuestionControl)QuestionPlaceHolder.FindControl("checkBoxQuestion");
                if (checkBoxQuestion != null)
                {
                    Boolean hasNextQuestion = false;
                    //loop through checkboxes
                    foreach (ListItem item in checkBoxQuestion.QuestionCheckBoxList.Items)
                    {
                        if (item.Selected)
                        {
                            int optionID = int.Parse(item.Value);
                            Session["optionID"]= int.Parse(item.Value);
                            Answer a = new Answer();
                            a.OptionId = int.Parse(item.Value);
                            a.QuestionId = currentQuestion;
                            answers.Add(a);
                            HttpContext.Current.Session["answers"] = answers;

                            SqlCommand optionCommand = new SqlCommand("SELECT nextQuestionId FROM SurveyQuestionOption WHERE optionId ="+ optionID, connection);
                            SqlDataReader optionReader = optionCommand.ExecuteReader();
                            while(optionReader.Read())
                            {
                                string nextQuestion = optionReader["nextQuestionId"].ToString();
                                if (nextQuestion != "" && hasNextQuestion==false)
                                {
                                    extraQuestions.Add(Int32.Parse(optionReader["nextQuestionId"].ToString()));
                                    hasNextQuestion = true;
                                }
                            }
                            //store item.Value(optionID) in session

                            //NOTE: some selected options lead to extra questions, here is where to do that
                            //if selected option leads onto extra question, then store it in the extraQuestions list 
                            //algorithm
                            //get item.Value from selected item, this is the optionID
                            //write sql command to get row from option table where optionID matches the selected items one
                            //if its next question id column is NOT null
                            //store the value in the next question id column into extra questions list
                        }
                    }
                }

                //see if checkbox control on page
                RadioBoxQuestionControl radioBoxQuestion = (RadioBoxQuestionControl)QuestionPlaceHolder.FindControl("radioBoxQuestion");
                if (radioBoxQuestion != null)
                {
                    //loop through checkboxes
                    foreach (ListItem item in radioBoxQuestion.QuestionRadioList.Items)
                    {
                        if (item.Selected)
                        {
                            int optionID = int.Parse(item.Value);
                            Answer a = new Answer();
                            a.OptionId = int.Parse(item.Value);
                            a.QuestionId = currentQuestion;
                            answers.Add(a);
                            HttpContext.Current.Session["answers"] = answers;

                            SqlCommand optionCommand = new SqlCommand("SELECT nextQuestionId FROM SurveyQuestionOption WHERE optionId =" + optionID, connection);
                            SqlDataReader optionReader = optionCommand.ExecuteReader();
                            while (optionReader.Read())
                            {
                                string nextQuestion = optionReader["nextQuestionId"].ToString();
                                if (nextQuestion != "")
                                {
                                    extraQuestions.Add(Int32.Parse(optionReader["nextQuestionId"].ToString()));
                                }
                            }
                            //store item.Value(optionID) in session

                            //NOTE: some selected options lead to extra questions, here is where to do that
                            //if selected option leads onto extra question, then store it in the extraQuestions list 
                            //algorithm
                            //get item.Value from selected item, this is the optionID
                            //write sql command to get row from option table where optionID matches the selected items one
                            //if its next question id column is NOT null
                            //store the value in the next question id column into extra questions list
                        }
                    }
                }

                //NOTE: this is an example. DONT DO IT THIS WAY. Hard coding extra question will lose like 10+ mark on
                //your assignment, add extra questions to list from checkbox and radiobutton selected options instead
                //if (currentQuestion == 1)
                //{
                //    extraQuestions.Add(2);
                 //   extraQuestions.Add(3);
                //}
                //Go to next question
                //===================
                //if we have extra questions, go to them first
                if (extraQuestions.Count > 0)
                {
                    //make the current question in session the first question from the extra question list
                    HttpContext.Current.Session[SESSION_QUESTION_NUMBER] = extraQuestions[0];
                    //remove first question from list
                    extraQuestions.RemoveAt(0);
                    //store the list of extra questions into the session
                    HttpContext.Current.Session[SESSION_EXTRA_QUESTIONS] = extraQuestions;

                    //reload the current page
                    Response.Redirect("QuestionPage.aspx");
                }
                else
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM SurveyQuestion WHERE questionId=" + currentQuestion, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    //read first row
                    if (reader.Read())
                    {
                        //get the column index from this sql table result based on column name
                        int nextQuestionColumnIndex = reader.GetOrdinal("nextQuestionId");//column ordinals start at 0
                                                                                                                  //check if this current row and this columns value is NULL or not
                        if (reader.IsDBNull(nextQuestionColumnIndex))
                        {
                            SqlCommand insertUserCommand;
                            insertUserCommand = new SqlCommand("INSERT INTO [User](givenName,userIpAddress,surveyRecordDate) VALUES('" + u.GivenName + "','" + u.UserIpAddress + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "');"+ " SELECT CAST(scope_identity() AS int);", connection);
                            int ? userId = (int?)insertUserCommand.ExecuteScalar();
                            HttpContext.Current.Session["userId"] = userId;
                            //if NULL, must be end of survey, so navigate to the thank you page
                            foreach (Answer a in answers)
                            {
                                SqlCommand insertCommand;
                                if(a.OptionId > 0)
                                    insertCommand= new SqlCommand("INSERT INTO SurveyAnswer(answerText,questionId,optionId,userId) VALUES('" + a.AnswerText + "','" + a.QuestionId + "','" + a.OptionId + "','"+userId+"')", connection);
                                else
                                    insertCommand = new SqlCommand("INSERT INTO SurveyAnswer(answerText,questionId,userId) VALUES('" + a.AnswerText + "','" + a.QuestionId + "','"+userId+"')", connection);

                                int rowsAffected = insertCommand.ExecuteNonQuery();//for insert, update,delete
                            }
                            HttpContext.Current.Session["answers"] = null;
                            Response.Redirect("QuestionCompletePage.aspx");//lead to thanks page
                        }
                        else
                        {
                            //If not null, make the value in that column of this row our new currentQuestion value
                            HttpContext.Current.Session[SESSION_QUESTION_NUMBER] = (int)reader["nextQuestionId"];
                            //reload page so it can generate the xext question into the page
                            Response.Redirect("QuestionPage.aspx");
                        }
                    }
                    else
                    {
                        //cant find this question in DB?!?!?!?!
                    }
                }
            }
        }

        protected void LogInButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        private static List<Answer> getListOfAnswersFromSession()
        {
            //make empty list
            List<Answer> answers = new List<Answer>();
            //if stored in session, replace empty list with the stored one
            if (HttpContext.Current.Session["answers"] != null)
                answers = (List<Answer>)HttpContext.Current.Session["answers"];

            return answers;
        }
        protected string GetIPAddress()
        {
            //get IP through PROXY
            //====================
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //should break ipAddress down, but here is what it looks like:
            // return ipAddress;
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] address = ipAddress.Split(',');
                if (address.Length != 0)
                {
                    return address[0];
                }
            }
            //if not proxy, get nice ip, give that back :(
            //ACROSS WEB HTTP REQUEST
            //=======================
            ipAddress = context.Request.UserHostAddress;//ServerVariables["REMOTE_ADDR"];

            if (ipAddress.Trim() == "::1")//ITS LOCAL(either lan or on same machine), CHECK LAN IP INSTEAD
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = System.Net.Dns.GetHostName();
                //Get Ip Host Entry
                System.Net.IPHostEntry ipHostEntries = System.Net.Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                System.Net.IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    ipAddress = arrIpAddress[1].ToString();
                }
                catch
                {
                    try
                    {
                        ipAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = System.Net.Dns.GetHostAddresses(stringHostName);
                            ipAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            ipAddress = "127.0.0.1";
                        }
                    }
                }
            }
            return ipAddress;
        }
    }
}