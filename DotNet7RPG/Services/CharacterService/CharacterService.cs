namespace DotNet7RPG.Services.CharacterService;

public class CharacterService : ICharacterService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    
    public CharacterService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var characters = await _context.Characters.ToListAsync();
        serviceResponse.Payload = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        var character = await _context.Characters.FindAsync(id);
        serviceResponse.Payload = _mapper.Map<GetCharacterDto>(character);
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto ch)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var character = _mapper.Map<Character>(ch);
        _context.Characters.Add(character);
        await _context.SaveChangesAsync();
        serviceResponse.Payload = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto ch)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        try
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c=>c.Id==ch.Id);
            if (character is null) throw new Exception($"Character with Id {ch.Id} not found");
            character.Name = ch.Name;
            await _context.SaveChangesAsync();
            serviceResponse.Payload = _mapper.Map<GetCharacterDto>(character);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        try
        {
            var character = await _context.Characters.FindAsync(id);
            if (character is null) throw new Exception($"Character with Id {id} not found");
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            serviceResponse.Payload =
                await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}