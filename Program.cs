using System.Reflection;
using PAS.Engine;
namespace PAS
{
    internal class Program
    {
        static void Main(string[] args)
        {
             LoadAssets();
             
             Game gameInstance = Game.GetInstance(); // Gets instance of the Game singleton
             gameInstance.InitWindow(1920, 1080, "PAS", true); // Initialize the SFML RenderWindow for the game
             
             Scene mainMenu = new Content.Scenes.MainMenu(); // Creates a main menu
             
             gameInstance.SetScene(mainMenu);// Sets the current scene to a new Main Menu
             gameInstance.StartGame();

        }

        static void LoadAssets()
        {
            Assembly assembly = typeof(Program).Assembly;
            CustomText.GenerateCharacterMap();
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.logo.png"), "logo");
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.menu_bg.png"), "menu_bg");
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.mainmenu_sky_bg.png"), "sky_bg");
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.cloud_layer.png"), "cloud_layer");
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.main_menu_button.png"), "menu_button");
            
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.button.png"), "button");
            AssetLoader.GetInstance().LoadFont("PAS.Resources.font.png", "main", 5, 1);
            AssetLoader.GetInstance().LoadFont("PAS.Resources.mini_font.png", "mini", 4, 1);

            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.class_select_bg.png"), "class_selection_bg");
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.class_frame.png"), "class_frame");
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.class_selector_button.png"), "selector_button");
            
            
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.turn_indicator.png"), "turn_indicator");
            
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.you_died.png"), "youdied");
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.victory.png"), "victory");
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.combat_bg.png"), "combat_bg");
            
            
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.healer_sheet.png"), "healer");
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.tank_sheet.png"), "tank");
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.damager_sheet.png"), "damager");
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.wukong_beber_sheet.png"), "beber");
            
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.heart.png"), "heart");
            AssetLoader.GetInstance().GetTexture("heart").Repeated = true;
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.class_frame_stats_icon.png"), "stats_icons");
            AssetLoader.GetInstance().GetTexture("stats_icons").Repeated = true;
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.parry_indicator_effect.png"), "parry_effect");
            
            AssetLoader.GetInstance().LoadTextureFromStream(assembly.GetManifestResourceStream("PAS.Resources.confetti_particles.png"), "confetti");
        }
    }
}