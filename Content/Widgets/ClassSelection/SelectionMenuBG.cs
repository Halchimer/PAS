using PAS.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.Content.Widgets.ClassSelection
{
    internal class SelectionMenuBG : Engine.Actor
    {
        public SelectionMenuBG() : base() {
            sprite = new SFML.Graphics.Sprite(AssetLoader.GetInstance().GetTexture("class_selection_bg"));
        }
    }
}
