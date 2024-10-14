using PAS.Engine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets
{
    internal class PlayButton : Engine.Button
    {
        public PlayButton() : base() { }

        public override void Start()
        {

            AssetLoader.GetInstance().LoadFont("upheavtt.ttf", "main_font");
            AddText("PLAY", AssetLoader.GetInstance().GetFont("main_font"), 10 ,new SFML.System.Vector2f(19.0f, -2.0f)) ;
            base.Start();
        }

        public override void Tick()
        {
            base.Tick();
        }

        public override void OnClick(RenderWindow window)
        {
            Game.GetInstance().SetScene(new Scenes.CombatScene(Game.GetInstance()));

            base.OnClick(window);
        }
    }
}
