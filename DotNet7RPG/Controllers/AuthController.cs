using DotNet7RPG.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace DotNet7RPG.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepo;
    private readonly IMapper _mapper;

    public AuthController(IAuthRepository authRepo, IMapper mapper)
    {
        _authRepo = authRepo;
        _mapper = mapper;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
    {
        var response = await _authRepo.Register(new User { Username = request.Username},request.Password);
        if (response.Success) return Ok(response);
        return BadRequest(response);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
    {
        var response = await _authRepo.Login(request.Username,request.Password);
        if (response.Success) return Ok(response);
        return BadRequest(response);
    }
}