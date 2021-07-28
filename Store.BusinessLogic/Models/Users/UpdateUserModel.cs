using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Users
{
    public class UpdateUserModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Jwt { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
