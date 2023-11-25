using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eCommerce
{
    [Route("api/Login")]
    public class Login_APIController : ApiController
    {

    }

    [Route("api/UserProfile")]
    public class UserProfile_APIController : ApiController
    {
        [HttpGet]
        [Route("api/UserProfile/Get")]
        public string Get()
        {
            return "Sample";
        }

        [HttpPost]
        [Route("api/UserProfile/UserPost")]
        public void UserPost(UserProfileModel userProfile)
        {
            // Initialize Model
            var model = new UserProfileModel()
            {
                FirstName = "Diwakar",
                LastName = "V",
                CurrentAddress = "XYZ",
                DOB = DateTime.Today,
                Email = "email",
                MobileNo = "1213123",
                Username = "Diwa",
                UserID = "UID001"
            };

            // Open SQL Connection
            var con = new SqlConnection(Global.dbConnectionString);
            con.Open();


            // Call SP for inserting item in UserProfile DB table

            // Status check

            // Close SQL connection
        }
    }


}
