using SFML.Graphics;
using SFML.System;

namespace PAS.Engine;

internal class CustomText : Sprite
{
    private static string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz:., ";
    private static Dictionary<char, int> characterMap = new Dictionary<char, int>();
    public CustomText(string text, CustomFont font) : base()
    {
        RenderTexture textRenderTexture = new RenderTexture((uint)(text.Length * font.CharacterSize + font.CharacterSpacing * text.Length -1), (uint)font.CharacterSize);
        uint counter = 0;
        Sprite spriteChar = new Sprite();
        foreach (char c in text)
        {
            spriteChar = new Sprite(font.GetGlyph(characterMap[c]));
            spriteChar.Scale = new Vector2f(1, -1);
            spriteChar.Position = new Vector2f(counter*(font.CharacterSize + font.CharacterSpacing), font.CharacterSize);
            
            textRenderTexture.Draw(spriteChar);

            counter++;
        }

        Texture = new Texture(textRenderTexture.Texture);
        Color = Color.Black;
    }

    public static void GenerateCharacterMap()
    {
        characterMap = new Dictionary<char, int>();
        for (int i = 0; i < characters.Length; ++i)
        {
            characterMap.Add(characters[i], i);
        }
    }
}