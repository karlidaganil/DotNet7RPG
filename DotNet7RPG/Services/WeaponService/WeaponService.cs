using System.Security.Claims;
using DotNet7RPG.Dtos.Weapon;

namespace DotNet7RPG.Services.WeaponService;

public class WeaponService : IWeaponService
{
    private readonly DataContext _contenxt;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public WeaponService(DataContext contenxt, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _contenxt = contenxt;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    private int GetUserId() =>
        int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
    {
        var response = new ServiceResponse<GetCharacterDto>();
        try
        {
            var character =
                await _contenxt.Characters.FirstOrDefaultAsync(c =>
                    c.Id == newWeapon.CharacterId && c.User!.Id == GetUserId());
            if (character is null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }
            // var vpn = _mapper.Map<Weapon>(newWeapon);

            var weapon = new Weapon
            {
                Name = newWeapon.Name,
                Damage = newWeapon.Damage,
                Character = character
            };
            
            _contenxt.Weapons.Add(weapon);
            await _contenxt.SaveChangesAsync();
            response.Payload = _mapper.Map<GetCharacterDto>(character);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }
}