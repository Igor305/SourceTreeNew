namespace EducationApp.BusinessLogicLayer.Models.ResponseModels
{
    public static class ResponseConstants
    {
        public static string Successfully { get; } = "Successfully";
        public static string Error { get; } = "Error";
        public static string Null { get; } = "Not all values ​​are filled";
        public static string WaringlessThanZero { get; } = "Value less than zero";
        public static string ErrorClone { get; } = "This value already exists";
        public static string ErrorId { get; } = "No this Id";
        public static string ErrorNotFound { get; } = "User is not found";
        public static string ErrorIncorrectData { get; } = "You entered incorrect data";
        public static string ConfirmEmail { get; } = "Confirm account from email";

    }
}
