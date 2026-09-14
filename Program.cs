using SplashKitSDK;

namespace RobotDodge
{
    public class Program
    {
        public static void Main()
        {
            Window gameWindow =
                new Window(
                    "Robot Dodge",
                    800,
                    600
                );

            RobotDodge game =
                new RobotDodge(
                    gameWindow
                );

            while (!game.Quit &&
                   !gameWindow.CloseRequested)
            {
                SplashKit.ProcessEvents();

                game.HandleInput();
                game.Update();
                game.Draw();
            }

            gameWindow.Close();
        }
    }
}
