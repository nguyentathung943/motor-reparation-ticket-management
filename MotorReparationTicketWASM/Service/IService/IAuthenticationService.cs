
using DataModel.Authentication;

namespace MotorReparationTicketWASM.Service.IService
{
    public interface IAuthenticationService
    {
        Task<RegisterResponseDTO> RegisterUser(RegisterDTO registerDTO);

        Task<LoginResponseDTO> Login(LoginDTO loginDTO);

        Task LogOut();

    }
}
