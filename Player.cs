using SplashKitSDK;

namespace RobotDodge
{
    public class Player
    {
        private Bitmap _PlayerBitmap;
        private SplashKitSDK.Timer _ScoreTimer;

        public double X
        {
            get;
            private set;
        }

        public double Y
        {
            get;
            private set;
        }

        public int Width
        {
            get
            {
                return _PlayerBitmap.Width;
            }
        }

        public int Height
        {
            get
            {
                return _PlayerBitmap.Height;
            }
        }

        public int Lives
        {
            get;
            private set;
        }

        public int Score
        {
            get
            {
                return (int)(_ScoreTimer.Ticks / 1000);
            }
        }

        public bool Quit
        {
            get;
            private set;
        }

        public Player(Window gameWindow)
        {
            _PlayerBitmap = new Bitmap(
                "Player",
                "Resources/images/Player.png"
            );

            X = (gameWindow.Width - Width) / 2;
            Y = (gameWindow.Height - Height) / 2;

            Lives = 5;
            Quit = false;

            _ScoreTimer = new SplashKitSDK.Timer(
                "Player Score Timer"
            );
            _ScoreTimer.Start();
        }

        public bool CollidedWith(Robot other)
        {
            return _PlayerBitmap.CircleCollision(
                X,
                Y,
                other.CollisionCircle
            );
        }

        public void LoseLife()
        {
            if (Lives > 0)
            {
                Lives = Lives - 1;
            }

            if (Lives <= 0)
            {
                Quit = true;
            }
        }

        public void HandleInput()
        {
            const int SPEED = 5;

            if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                X = X - SPEED;
            }

            if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                X = X + SPEED;
            }

            if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                Y = Y - SPEED;
            }

            if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                Y = Y + SPEED;
            }

            if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                Quit = true;
            }
        }

        public void StayOnWindow(Window gameWindow)
        {
            const int GAP = 10;

            if (X < GAP)
            {
                X = GAP;
            }

            if (X + Width > gameWindow.Width - GAP)
            {
                X = gameWindow.Width - Width - GAP;
            }

            if (Y < GAP)
            {
                Y = GAP;
            }

            if (Y + Height > gameWindow.Height - GAP)
            {
                Y = gameWindow.Height - Height - GAP;
            }
        }

        public void Draw()
        {
            _PlayerBitmap.Draw(X, Y);
        }

        public void DrawStatus()
        {
            for (int i = 0; i < Lives; i++)
            {
                SplashKit.FillCircle(
                    Color.Red,
                    20 + (i * 20),
                    20,
                    7
                );
            }

            SplashKit.DrawText(
                $"Score: {Score}",
                Color.Black,
                680,
                15
            );
        }
    }
}
