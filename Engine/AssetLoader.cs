using SFML.Audio;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Engine
{
    internal class AssetLoader
    {
        private Dictionary<string, Texture> texDict = new Dictionary<string, Texture>();
        private Dictionary<string, Font> fontDict = new Dictionary<string, Font>();
        private Dictionary<string, Sound> soundDict = new Dictionary<string, Sound>();

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

        public void LoadFont(string path, string name ) 
        {
            Font font = new Font(path);
            font.SetSmooth(false);
            fontDict.Add(name, font);
        }
        public void RemoveFont(string name)
        {
            fontDict[name].Dispose();
            fontDict.Remove(name);
        }

        public Texture GetTexture(string name) {
            return texDict[name];
        }
        public Font GetFont(string name)
        {
            return fontDict[name];
        }
    }
}
