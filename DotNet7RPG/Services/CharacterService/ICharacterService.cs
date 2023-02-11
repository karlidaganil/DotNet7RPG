namespace DotNet7RPG.Services.CharacterService;

public interface ICharacterService
{
    Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId);
    Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
    Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto ch);
    Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto ch);
    Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);

    
}