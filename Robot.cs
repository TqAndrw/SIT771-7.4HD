using SplashKitSDK;

namespace RobotDodge
{
    public abstract class Robot
    {
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

        public Vector2D Velocity
        {
            get;
            protected set;
        }

        public Color MainColor
        {
            get;
            private set;
        }

        public int Width
        {
            get
            {
                return 50;
            }
        }

        public int Height
        {
            get
            {
                return 50;
            }
        }

        public Circle CollisionCircle
        {
            get
            {
                return SplashKit.CircleAt(
                    X + Width / 2,
                    Y + Height / 2,
                    20
                );
            }
        }

        public Robot(Window gameWindow, Player player)
        {
            switch (SplashKit.Rnd(4))
            {
                case 0:
                    X = -Width;
                    Y = SplashKit.Rnd(gameWindow.Height - Height);
                    break;

                case 1:
                    X = gameWindow.Width;
                    Y = SplashKit.Rnd(gameWindow.Height - Height);
                    break;

                case 2:
                    X = SplashKit.Rnd(gameWindow.Width - Width);
                    Y = -Height;
                    break;

                case 3:
                    X = SplashKit.Rnd(gameWindow.Width - Width);
                    Y = gameWindow.Height;
                    break;
            }

            MainColor = Color.RandomRGB(200);

            Point2D robotCentre = SplashKit.PointAt(
                X + Width / 2.0,
                Y + Height / 2.0
            );

            Point2D playerCentre = SplashKit.PointAt(
                player.X + player.Width / 2.0,
                player.Y + player.Height / 2.0
            );

            Vector2D direction = SplashKit.VectorPointToPoint(
                robotCentre,
                playerCentre
            );

            direction = SplashKit.UnitVector(direction);

            Velocity = SplashKit.VectorMultiply(
                direction,
                2.0
            );
        }

        public virtual void Update()
        {
            X = X + Velocity.X;
            Y = Y + Velocity.Y;
        }

        public bool IsOffscreen(Window screen)
        {
            return
                X < -Width ||
                X > screen.Width ||
                Y < -Height ||
                Y > screen.Height;
        }

        public abstract void Draw();
    }

    public class Boxy : Robot
    {
        public Boxy(Window gameWindow, Player player)
            : base(gameWindow, player)
        {
        }

        public override void Draw()
        {
            double leftX = X + 12;
            double rightX = X + 27;
            double eyeY = Y + 10;
            double mouthY = Y + 30;

            SplashKit.FillRectangle(Color.Gray, X, Y, Width, Height);
            SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10);
            SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);
            SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
            SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
        }
    }

    public class Roundy : Robot
    {
        private readonly Player _target;

        public Roundy(Window gameWindow, Player player)
            : base(gameWindow, player)
        {
            _target = player;
        }

        public override void Update()
        {
            Point2D robotCentre = SplashKit.PointAt(
                X + Width / 2.0,
                Y + Height / 2.0
            );

            Point2D playerCentre = SplashKit.PointAt(
                _target.X + _target.Width / 2.0,
                _target.Y + _target.Height / 2.0
            );

            Vector2D direction = SplashKit.VectorPointToPoint(
                robotCentre,
                playerCentre
            );

            if (direction.X == 0 && direction.Y == 0)
            {
                return;
            }

            Velocity = SplashKit.VectorMultiply(
                SplashKit.UnitVector(direction),
                2.0
            );

            base.Update();
        }

        public override void Draw()
        {
            double leftX = X + 17;
            double midX = X + 25;
            double rightX = X + 33;
            double midY = Y + 25;
            double eyeY = Y + 20;
            double mouthY = Y + 35;

            SplashKit.FillCircle(Color.White, midX, midY, 25);
            SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
            SplashKit.FillCircle(MainColor, leftX, eyeY, 5);
            SplashKit.FillCircle(MainColor, rightX, eyeY, 5);
            SplashKit.FillEllipse(Color.Gray, X, eyeY, 50, 30);
            SplashKit.DrawLine(Color.Black, X, mouthY, X + 50, mouthY);
        }
    }

    public class Cyclops : Robot
    {
        private Vector2D _forwardVelocity;
        private double _zigzagPhase;

        public Cyclops(Window gameWindow, Player player)
            : base(gameWindow, player)
        {
            _forwardVelocity = Velocity;
            _zigzagPhase = 0;
        }

        public override void Update()
        {
            _zigzagPhase += 0.15;

            Vector2D perpendicular;
            perpendicular.X = -_forwardVelocity.Y;
            perpendicular.Y = _forwardVelocity.X;

            Vector2D sideVelocity = SplashKit.VectorMultiply(
                perpendicular,
                Math.Sin(_zigzagPhase) * 0.75
            );

            Velocity = SplashKit.VectorAdd(
                _forwardVelocity,
                sideVelocity
            );

            base.Update();
        }

        public override void Draw()
        {
            double midX = X + 25;
            double midY = Y + 25;

            SplashKit.FillCircle(MainColor, midX, midY, 25);
            SplashKit.FillCircle(Color.White, midX, Y + 18, 10);
            SplashKit.FillCircle(Color.Black, midX, Y + 18, 5);
            SplashKit.DrawLine(Color.Black, X + 15, Y + 38, X + 35, Y + 38);
        }
    }

    public class Hunter : Robot
    {
        private readonly Player _target;
        private double _speed;

        public Hunter(Window gameWindow, Player player)
            : base(gameWindow, player)
        {
            _target = player;
            _speed = 1.5;
        }

        public override void Update()
        {
            Point2D hunterCentre = SplashKit.PointAt(
                X + Width / 2.0,
                Y + Height / 2.0
            );

            Point2D playerCentre = SplashKit.PointAt(
                _target.X + _target.Width / 2.0,
                _target.Y + _target.Height / 2.0
            );

            Vector2D direction = SplashKit.VectorPointToPoint(
                hunterCentre,
                playerCentre
            );

            if (direction.X == 0 && direction.Y == 0)
            {
                return;
            }

            if (_speed < 4.0)
            {
                _speed += 0.005;
            }

            Velocity = SplashKit.VectorMultiply(
                SplashKit.UnitVector(direction),
                _speed
            );

            base.Update();
        }

        public override void Draw()
        {
            double midX = X + Width / 2.0;
            double midY = Y + Height / 2.0;

            SplashKit.FillCircle(Color.Red, midX, midY, 25);
            SplashKit.FillCircle(Color.Black, midX, midY, 8);
        }
    }
}
