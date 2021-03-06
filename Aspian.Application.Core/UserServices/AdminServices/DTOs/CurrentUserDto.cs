namespace Aspian.Application.Core.UserServices.AdminServices.DTOs
{
    public class CurrentUserDto
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public PhotoDto Photo { get; set; }
    }
}