using System.Reflection;
using SFML.Graphics;
using SFML.System;

namespace PAS.Engine;

/// <summary>
/// Represents a custom font loaded from a texture and contains information about character size and spacing.
/// </summary>
public class CustomFont
{
    /// <summary>
    /// Represents the texture used for rendering custom font glyphs.
    /// </summary>
    private Texture _fontTexture;

    /// <summary>
    /// Gets the size of the characters used in the font.
    /// </summary>
    /// <remarks>
    /// This property holds the size of the characters that are rendered using the font.
    /// The size is specified in pixels and determines the height and width of each character.
    /// </remarks>
    public int CharacterSize { get; private set; }

    /// Gets the spacing between characters in a custom font.
    /// This property determines the number of pixels to add between each character when rendering text.
    /// The default value is 1, but can be customized through the constructor.
    public int CharacterSpacing { get; private set; }

    /// Initializes a new instance of the CustomFont class.
    /// <param name="path">The embedded resource path to the font texture file.</param>
    /// <param name="characterSize">The size of each character in the font.</param>
    /// <param name="characterSpacing">The spacing between characters in the font. Default is 1.</param>
    public CustomFont(string path, int characterSize, int characterSpacing = 1)
    {
        Assembly assembly = typeof(Program).Assembly;
        _fontTexture = new Texture(assembly.GetManifestResourceStream(path));
        CharacterSize = characterSize;
        CharacterSpacing = characterSpacing;
    }

    /// Retrieves a glyph sprite from the font texture based on the given index.
    /// <param name="index">The index of the glyph to retrieve.</param>
    /// <return>Returns a Sprite object representing the glyph.</return>
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