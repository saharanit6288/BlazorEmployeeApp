using BlazorEmployeeManagementApp2.Shared.AuthModels;

namespace BlazorEmployeeManagementApp2.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel registerModel);
        Task<UserDetails> GetUserInfo();
        Task<ChangePasswordResult> ChangePassword(ChangePasswordModel model);
        Task<ResetPasswordResult> ResetPassword(ResetPasswordModel model);
    }
}
