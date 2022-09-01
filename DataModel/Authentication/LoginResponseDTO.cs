namespace DataModel.Authentication;

public class LoginResponseDTO
{
    public bool IsAuthSuccessful { get; set; }
    public string ErrorMessage { get; set; }
    public string Token { get; set; }
    public UserInfoDTO UserInfoDTO { get; set; }
}
