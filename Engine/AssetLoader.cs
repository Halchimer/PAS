
using SFML.Graphics;
namespace PAS.Engine
{
    internal class AssetLoader
    {
        private Dictionary<string, Texture> texDict = new Dictionary<string, Texture>();
        private Dictionary<string, CustomFont> fontDict = new Dictionary<string, CustomFont>();
        //private Dictionary<string, Sound> soundDict = new Dictionary<string, Sound>();

        // Singleton Setup

        private AssetLoader() { }

        private static AssetLoader _instance;

        public static AssetLoader GetInstance() { 
            if(_instance == null )
                _instance = new AssetLoader();
            return _instance;
        }

        // Texture Loader

        public void LoadTexture( string path, string name )
        {
            texDict.Add(name, new Texture(path));
        }
        public void RemoveTexture(string name)
        {
            texDict[name].Dispose();
            texDict.Remove(name);
        }

        public void LoadFont(string path, string name, int characterSize, int characterSpacing=1 ) 
        {
            CustomFont font = new CustomFont(path, characterSize, characterSpacing);
            fontDict.Add(name, font);
        }
        public void RemoveFont(string name)
        {
            fontDict.Remove(name);
        }

        public Texture GetTexture(string name) {
            return texDict[name];
        }
        public CustomFont GetFont(string name)
        {
            return fontDict[name];
        }
    }
}
