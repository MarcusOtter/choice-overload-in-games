
[System.Serializable]
public class FullCustomCharacter
{
    // =============== General settings =============== //
    internal readonly string CharacterName;
    internal readonly int SkinTone; //  1: Brown  |  2: White  |  3: Red  |  4: Green
    internal readonly string CatchPhrase;

    // ==================== Clothes =================== //

    // ===================== Hair ===================== //

    // ==================== Weapon ==================== //

    internal FullCustomCharacter(string characterName, int skinTone, string catchPhrase)
    {
        CharacterName = characterName;
        SkinTone = skinTone;
        CatchPhrase = catchPhrase;
    }
}
