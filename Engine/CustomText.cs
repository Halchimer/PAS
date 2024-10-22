using SFML.Graphics;
using SFML.System;

namespace PAS.Engine;

internal class CustomText : Sprite
{
    private static string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz:.,0123456789 ";
    private static Dictionary<char, int> characterMap = new Dictionary<char, int>();
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

    public static void GenerateCharacterMap()
    {
        characterMap = new Dictionary<char, int>();
        for (int i = 0; i < characters.Length; ++i)
        {
            characterMap.Add(characters[i], i);
        }
    }
}