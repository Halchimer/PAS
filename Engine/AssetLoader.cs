
using SFML.Graphics;
namespace PAS.Engine
{
    /// <summary>
    /// The AssetLoader class is responsible for loading and managing game assets such as textures and fonts.
    /// It follows the Singleton pattern to ensure only one instance of the AssetLoader exists.
    /// </summary>
    internal class AssetLoader
    {
        /// <summary>
        /// A dictionary that holds textures, indexed by their name.
        /// </summary>
        /// <remarks>
        /// This dictionary is intended to manage textures that are loaded from various sources,
        /// such as file paths or streams, and stored by a string key for easy retrieval and management.
        /// The keys are unique identifiers for each texture, facilitating access and manipulation of textures
        /// within the asset loader.
        /// </remarks>
        private Dictionary<string, Texture> texDict = new Dictionary<string, Texture>();

        /// <summary>
        /// A dictionary that holds the loaded custom fonts, keyed by their string names.
        /// </summary>
        /// <remarks>
        /// This variable is used to manage and access custom font assets within the application.
        /// Fonts can be added to this dictionary using the LoadFont method and retrieved using the GetFont method.
        /// </remarks>
        private Dictionary<string, CustomFont> fontDict = new Dictionary<string, CustomFont>();
        //private Dictionary<string, Sound> soundDict = new Dictionary<string, Sound>();

        // Singleton Setup

        /// <summary>
        /// AssetLoader is a singleton class designed to manage assets such as textures and custom fonts for the PAS Engine.
        /// </summary>
        private AssetLoader() { }

        /// <summary>
        /// Holds the singleton instance of the AssetLoader class.
        /// </summary>
        private static AssetLoader _instance;

        /// <summary>
        /// Returns the singleton instance of the AssetLoader class.
        /// </summary>
        /// <returns>The single instance of AssetLoader.</returns>
        public static AssetLoader GetInstance() { 
            if(_instance == null )
                _instance = new AssetLoader();
            return _instance;
        }
        
        /// <summary>
        /// Loads a texture from the specified file path and assigns it a name for retrieval.
        /// </summary>
        /// <param name="path">The file path to the texture.</param>
        /// <param name="name">The name to associate with the loaded texture.</param>
        public void LoadTexture( string path, string name )
        {
            texDict.Add(name, new Texture(path));
        }

        /// <summary>
        /// Loads a texture from a given stream and stores it in the texture dictionary
        /// with the specified name.
        /// </summary>
        /// <param name="stream">The stream from which the texture is loaded.</param>
        /// <param name="name">The name to associate with the loaded texture.</param>
        public void LoadTextureFromStream(Stream stream, string name)
        {
            texDict.Add(name, new Texture(stream));
        }

        /// <summary>
        /// Removes a texture from the texture dictionary and disposes of it.
        /// </summary>
        /// <param name="name">The name associated with the texture to be removed.</param>
        public void RemoveTexture(string name)
        {
            texDict[name].Dispose();
            texDict.Remove(name);
        }

        /// Loads a font from the specified file path, assigns it a name, and sets its character size and spacing.
        /// <param name="path">The file path where the font is located.</param>
        /// <param name="name">The name to assign to the loaded font.</param>
        /// <param name="characterSize">The size of the characters in the font.</param>
        /// <param name="characterSpacing">The spacing between characters in the font. Default is 1.</param>
        public void LoadFont(string path, string name, int characterSize, int characterSpacing=1 ) 
        {
            CustomFont font = new CustomFont(path, characterSize, characterSpacing);
            fontDict.Add(name, font);
        }

        /// <summary>
        /// Removes a font from the AssetLoader's font dictionary.
        /// </summary>
        /// <param name="name">The name of the font to be removed.</param>
        public void RemoveFont(string name)
        {
            fontDict.Remove(name);
        }

        /// <summary>
        /// Retrieves a texture by its name.
        /// </summary>
        /// <param name="name">The name of the texture to retrieve.</param>
        /// <returns>The texture associated with the given name.</returns>
        public Texture GetTexture(string name) {
            return texDict[name];
        }

        /// <summary>
        /// Retrieves the font associated with the specified name from the asset loader's font dictionary.
        /// </summary>
        /// <param name="name">The name of the font to retrieve.</param>
        /// <returns>The CustomFont object associated with the specified name.</returns>
        public CustomFont GetFont(string name)
        {
            return fontDict[name];
        }
    }
}
