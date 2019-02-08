using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITR
{
    public class User
    {
        string givenName;
        string lastName;
        string userIpAddress;
        string phoneNumber;
        DateTime surveyRecordDate;
        int age;

        public string GivenName
        {
            get
            {
                return givenName;
            }
            set
            {
                givenName = value;
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }
        public string UserIpAddress
        {
            get
            {
                return userIpAddress;
            }
            set
            {
                userIpAddress = value;
            }
        }
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
            }
        }
        public DateTime SurveyRecordDate
        {
            get
            {
                return surveyRecordDate;
            }
            set
            {
                surveyRecordDate = value;
            }
        }
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }
    }
}