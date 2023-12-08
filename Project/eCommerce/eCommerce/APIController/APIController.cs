using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace eCommerce
{
    [Route("api/Login")]
    public class Login_APIController : ApiController
    {
        [HttpGet]
        [Route("api/Login/GetUser")]
        public bool GetUser(LoginModel loginModel)
        {
            var userController = new UserProfile_APIController()
            {
                Request = new HttpRequestMessage(HttpMethod.Post, Request.RequestUri.AbsoluteUri.Replace("https://localhost:44364/api/Login/GetUser", "https://localhost:44364/api/UserProfile"))
            };

            userController.Request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();
            var userModelList = userController.GetUsers(null) as List<UserProfileModel>;

            var userModel = userModelList.Where(x => x.Username.Equals(loginModel.Username) || x.Email.Equals(loginModel.Email))
                .Where(x => x.Password.Equals(loginModel.Passwords)).FirstOrDefault() as UserProfileModel;

            if (userModel != null && (loginModel.Username == userModel.Username || loginModel.Email == userModel.Email) && loginModel.Passwords == userModel.Password)
            {
                userController = null;
                return true;
            }
            else
            {
                userController = null;
                return false;
            }
        }
    }

    [Route("api/Products")]
    public class Product_APIController : ApiController
    {
        [HttpGet]
        [Route("api/Products/GetProduct")]
        public object GetProduct(int? productId = null) // 1001
        {
            List<ProductListModel> modelList = new List<ProductListModel>();
            using (var con = new SqlConnection(Global.dbConnectionString))
            {
                var command = new SqlCommand($"select * from ProductListing", con);
                con.Open();
                var dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var model = new ProductListModel();
                    model.ProductID = int.Parse(dataReader["ProductID"].ToString());
                    model.ProductName = dataReader["ProductName"].ToString();
                    model.ProductDescription = dataReader["ProductDescription"].ToString();
                    model.Price = decimal.Parse(dataReader["Price"].ToString());
                    model.StockQuantity = int.Parse(dataReader["StockQuantity"].ToString());
                    model.Category = dataReader["Category"].ToString();
                    model.Manufacturer = dataReader["Manufacturer"].ToString();
                    model.ImageURL = dataReader["ImageURL"].ToString();
                    model.IsNewCollection = Boolean.Parse(dataReader["IsNewCollection"].ToString());

                    if (productId != null && model.ProductID == productId)
                    {
                        con.Close();
                        return model;
                    }
                    else
                    {
                        modelList.Add(model);
                    }
                }

                con.Close();
                return modelList;
            }
        }

        [HttpPost]
        [Route("api/Products/ProductPost")]
        public void UserPost(ProductListModel product)
        {
            using (var con = new SqlConnection(Global.dbConnectionString))
            {
                var command = new SqlCommand("InsertProductProcedure", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ProductName ", product.ProductName);
                command.Parameters.AddWithValue("@ProductDescription ", product.ProductDescription);
                command.Parameters.AddWithValue("@Price ", product.Price);
                command.Parameters.AddWithValue("@StockQuantity ", product.StockQuantity);
                command.Parameters.AddWithValue("@Category ", product.Category);
                command.Parameters.AddWithValue("@Manufacturer ", product.Manufacturer);
                command.Parameters.AddWithValue("@ImageURL ", product.ImageURL);

                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
        }
    }

    [Route("api/UserProfile")]
    public class UserProfile_APIController : ApiController
    {
        [HttpGet]
        [Route("api/UserProfile/GetUsers")]
        public object GetUsers(string userID = null) // UserID = UID1001
        {
            List<UserProfileModel> modelList = new List<UserProfileModel>();
            using (var con = new SqlConnection(Global.dbConnectionString))
            {
                var command = new SqlCommand($"select * from UserProfile", con);
                con.Open();
                var dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var model = new UserProfileModel();
                    model.UserID = dataReader["UserID"].ToString();
                    model.Username = dataReader["Username"].ToString();
                    model.FirstName = dataReader["FirstName"].ToString();
                    model.LastName = dataReader["LastName"].ToString();
                    model.MobileNo = dataReader["MobileNo"].ToString();
                    model.Email = dataReader["Email"].ToString();
                    model.CurrentAddress = dataReader["CurrentAddress"].ToString();
                    model.DOB = DateTime.Parse(dataReader["DOB"].ToString());
                    model.Password = dataReader["Passwords"].ToString();

                    if (userID != null && model.UserID == userID)
                    {
                        con.Close();
                        return model;
                    }
                    else
                    {
                        modelList.Add(model);
                    }
                }

                con.Close();
                return modelList;
            }
        }

        [HttpPost]
        [Route("api/UserProfile/UserPost")]
        public void UserPost(object user)
        {
            UserProfileModel userProfile = new UserProfileModel();

            if (user.GetType().Name.Contains("Json"))
            {
                userProfile = new JsonSerializer().Deserialize<UserProfileModel>((JsonReader)user);
            }
            else if (user.GetType().Name.Contains("JObject"))
            {
                userProfile = (user as JObject).ToObject<UserProfileModel>();
            }
            else
            {
                userProfile = (UserProfileModel)user;
            }

            using (var con = new SqlConnection(Global.dbConnectionString))
            {
                var command = new SqlCommand("InsertUserProfileProcedure", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Username", userProfile.Username);
                command.Parameters.AddWithValue("@FirstName", userProfile.FirstName);
                command.Parameters.AddWithValue("@LastName", userProfile.LastName);
                command.Parameters.AddWithValue("@MobileNo", userProfile.MobileNo);
                command.Parameters.AddWithValue("@Email", userProfile.Email);
                command.Parameters.AddWithValue("@CurrentAddress", userProfile.CurrentAddress);
                command.Parameters.AddWithValue("@DOB", userProfile.DOB);
                command.Parameters.AddWithValue("@Passwords", userProfile.Password);

                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
        }
    }

    [Route("api/Admin")]
    public class Admin_APIController : ApiController
    {

    }
}
