namespace JWTApp.Models
{
    public class UserConstants
    {
         public static List<UserModel> Users = new List<UserModel>()
         {
             new UserModel() { Username = "david_admin", Email = "david.admin@email.com", Password = "1234p@ssword", GivenName = "David", Surname = "Charles", Role = "Administrator"},
             new UserModel() { Username = "josh_seller", Email = "josh.seller@email.com", Password = "1234p@ssword", GivenName = "Josh", Surname = "Rich", Role = "Seller"},
         }
    }
}
