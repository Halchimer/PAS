using PAS.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets
{
    internal class MainMenuBackground : Actor
    {
        public MainMenuBackground(): base() 
        {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("menu_bg"));
        }

        public override void Start()
        {
            base.Start();
        }
    }
}
