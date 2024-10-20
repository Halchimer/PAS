using PAS.Engine;

namespace PAS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LoadAssets();

            Game gameInstance = Game.GetInstance();
            gameInstance.InitWindow(1920, 1080, "PAS", true);

            Scene mainMenu = new Content.Scenes.MainMenu();

            gameInstance.SetScene(mainMenu);
            gameInstance.StartGame();

        }

        static void LoadAssets()
        {
            CustomText.GenerateCharacterMap();
            AssetLoader.GetInstance().LoadTexture("logo.png", "logo");
            AssetLoader.GetInstance().LoadTexture("menu_bg.png", "menu_bg");
            AssetLoader.GetInstance().LoadTexture("mainmenu_sky_bg.png", "sky_bg");
            AssetLoader.GetInstance().LoadTexture("cloud_layer.png", "cloud_layer");
            AssetLoader.GetInstance().LoadTexture("main_menu_button.png", "menu_button");
            
            AssetLoader.GetInstance().LoadTexture("button.png", "button");
            AssetLoader.GetInstance().LoadFont("font.png", "main", 5, 1);
            AssetLoader.GetInstance().LoadFont("mini_font.png", "mini", 4, 1);

            AssetLoader.GetInstance().LoadTexture("class_select_bg.png", "class_selection_bg");
            AssetLoader.GetInstance().LoadTexture("class_frame.png", "class_frame");
            AssetLoader.GetInstance().LoadTexture("class_selector_button.png", "selector_button");
            
            AssetLoader.GetInstance().LoadTexture("you_died.png", "youdied");
            AssetLoader.GetInstance().LoadTexture("victory.png", "victory");
            AssetLoader.GetInstance().LoadTexture("combat_bg.png", "combat_bg");
            
            AssetLoader.GetInstance().LoadTexture("healer_sheet.png", "healer");
            AssetLoader.GetInstance().LoadTexture("tank_sheet.png", "tank");
            AssetLoader.GetInstance().LoadTexture("damager_sheet.png", "damager");
            
            AssetLoader.GetInstance().LoadTexture("heart.png", "heart");
            AssetLoader.GetInstance().GetTexture("heart").Repeated = true;
            AssetLoader.GetInstance().LoadTexture("parry_indicator_effect.png", "parry_effect");
            
            AssetLoader.GetInstance().LoadTexture("confetti_particles.png", "confetti");
        }
    }
}