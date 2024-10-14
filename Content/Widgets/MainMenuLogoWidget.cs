using PAS.Engine;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets
{
    internal class MainMenuLogoWidget : Actor
    {
        Vector2f defaultLocation;

        public MainMenuLogoWidget() : base() 
        { 
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("logo")); 
        }

        public override void Start()
        {
            defaultLocation = new SFML.System.Vector2f(
                192 / 2 - sprite.GetLocalBounds().Width / 2,
                108.0f / 15
            );

            MoveToLocationOverTime(defaultLocation, 5);

            base.Start();
        }

        public override void Tick()
        {

            base.Tick();
        }
    }
}
