using SFML.Graphics;
using SFML.System;

namespace PAS.Engine;

public class CustomFont
{
    private Texture _fontTexture;
    public int CharacterSize { get; private set; }
    public int CharacterSpacing { get; private set; }
    public CustomFont(string path, int characterSize, int characterSpacing = 1)
    {
        _fontTexture = new Texture(path);
        CharacterSize = characterSize;
        CharacterSpacing = characterSpacing;
    }

    public Sprite GetGlyph(int index)
    {
        int glyphRowNumber = (int)Math.Floor((float)(_fontTexture.Size.X / CharacterSize));
        Vector2i coordinates = new Vector2i(
            (index - glyphRowNumber*(int)Math.Floor((float)(index / glyphRowNumber)))*CharacterSize,
            (int)Math.Floor((float)(index / glyphRowNumber))*CharacterSize
        );
        Sprite res = new Sprite(_fontTexture);
        res.TextureRect = new IntRect(coordinates.X, coordinates.Y, CharacterSize, CharacterSize);
        return res;
    }
}