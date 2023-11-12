namespace fsmAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }  = "";
        public string LastName { get; set; }  = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string AvatarUrl { get; set; } = "";
        public string DateOfBirth { get; set; } = "";
        public string Gender { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Bio { get; set; } = "";
        public string ProfilePictureUrl { get; set; } = "";
        public string Video1Url { get; set; } = "";
        public string Video2Url { get; set; } = "";
        public string Video3Url { get; set; } = "";
        public string RegistrationDate { get; set; } = "";
        public string LastLoginDate { get; set; } = "";
        public string IsVerified { get; set; } = "";
        public int Role { get; set; }
    }
}
