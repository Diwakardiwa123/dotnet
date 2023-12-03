using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Passwords { get; set; }
        public string Email { get; set; }
    }

    public class UserProfileModel
    {
        public string Username { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string CurrentAddress { get; set; }
        public DateTime DOB { get; set; }
        public string Password { get; set; }
    }

    public class ProductListModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public string ImageURL { get; set; }
    }

    public class WishlistModel
    {
        public int WishlistID { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
    }

    public class OrderDetailsModel
    {
        public string OrderID { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}