using System.Security.Claims;

namespace HRM.API.Utils.Constants
{
    public class MessageErrorConstants
    {
        public static string Default = "An error occurred please try again later";
        public static string NotFound = "Can not found";
        public static string Unauthorize = "You do not have access. Please log in again.";
        public static string WrongUserName = "Wrong user name";
        public static string WrongPassword = "Wrong password";
        public static string TokenExpired = "Tokens expired";
        public static string VerifyEmail = "Please verify email";

    }
    public class MessageSuccessConstants
    {
        public static string Default = "Success";
    

    }


}
