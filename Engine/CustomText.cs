using SFML.Graphics;
using SFML.System;

namespace PAS.Engine;

/// The CustomText class is designed to render text using a custom font
/// in an SFML application. The class converts text into a series of drawable
/// Sprite objects based on character glyphs from the provided CustomFont.
/// Upon initialization, it creates a RenderTexture that holds the entire
/// text as a single texture, allowing it to be efficiently rendered.
/// The CustomText class inherits from the SFML.Graphics.Sprite class,
/// allowing it to be used as any other sprite in the SFML framework.
internal class CustomText : Sprite
{
    /// <summary>
    /// A string containing characters supported by the CustomText class.
    /// </summary>
    private static string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz:.,0123456789 ";

    /// <summary>
    /// A dictionary that maps individual characters to their corresponding indices.
    /// This mapping is essential for rendering text using custom fonts,
    /// as it allows quick lookup of a character's glyph index.
    /// </summary>
    private static Dictionary<char, int> characterMap = new Dictionary<char, int>();

    /// CustomText class is responsible for rendering text using a specified CustomFont.
    /// It extends the Sprite class and manages the creation of a RenderTexture
    /// which renders each character of the input text as individual sprites.
    public CustomText(string text, CustomFont font) : base()
    {
        RenderTexture textRenderTexture = new RenderTexture(
            (uint)(text.Length * font.CharacterSize + font.CharacterSpacing * text.Length -1),
            (uint)((font.CharacterSize + 1) * (1+text.Count(t => t == '\n')))
            );
        uint counter = 0;
        uint line_counter = (uint)text.Count(t => t == '\n');
        Sprite spriteChar = new Sprite();
        foreach (char c in text)
        {
            if (c == '\n')
            {
                counter = 0;
                line_counter--;
                continue;
            }
            spriteChar = new Sprite(font.GetGlyph(characterMap[c]));
            spriteChar.Scale = new Vector2f(1, -1);
            spriteChar.Position = new Vector2f(counter*(font.CharacterSize + font.CharacterSpacing), (font.CharacterSize+1) *
                (1+line_counter));
            
            textRenderTexture.Draw(spriteChar);
            
            counter++;
        }

        Texture = new Texture(textRenderTexture.Texture);
        Color = Color.Black;
    }

    /// The GenerateCharacterMap method populates the characterMap dictionary.
    /// Each character in the characters array is mapped to its corresponding index.
    public static void GenerateCharacterMap()
    {
        characterMap = new Dictionary<char, int>();
        for (int i = 0; i < characters.Length; ++i)
        {
            characterMap.Add(characters[i], i);
        }
    }
}