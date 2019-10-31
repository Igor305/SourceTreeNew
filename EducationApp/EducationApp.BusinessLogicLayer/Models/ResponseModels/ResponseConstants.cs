namespace EducationApp.BusinessLogicLayer.Models.ResponseModels
{
    public class ResponseConstants
    {
        public string Successfully { get; } = "Successfully";
        public string Error { get; } = "Error";
        public string Null { get; } = "Not all values ​​are filled";
        public string WaringlessThanZero { get; } = "Value less than zero";
        public string ErrorDate { get; } = "Date vot valide";
        public string ErrorClone { get; } = "This value already exists";
        public string ErrorId { get; } = "No this Id";

    }
}
