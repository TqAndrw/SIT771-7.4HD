using SplashKitSDK;

namespace RobotDodge
{
    public class Bullet
    {
        private const double RADIUS = 5;
        private const double SPEED = 8.0;

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
            private set;
        }

        public Circle CollisionCircle
        {
            get
            {
                return SplashKit.CircleAt(
                    X,
                    Y,
                    RADIUS
                );
            }
        }

        public Bullet(Player player, Point2D target)
        {
            X = player.X + player.Width / 2;
            Y = player.Y + player.Height / 2;

            Point2D bulletStart =
                SplashKit.PointAt(X, Y);

            Vector2D direction =
                SplashKit.VectorPointToPoint(
                    bulletStart,
                    target
                );

            direction =
                SplashKit.UnitVector(direction);

            Velocity =
                SplashKit.VectorMultiply(
                    direction,
                    SPEED
                );
        }

        public void Update()
        {
            X = X + Velocity.X;
            Y = Y + Velocity.Y;
        }

        public bool CollidedWith(Robot robot)
        {
            return SplashKit.CirclesIntersect(
                CollisionCircle,
                robot.CollisionCircle
            );
        }

        public bool IsOffscreen(Window gameWindow)
        {
            return
                X < -RADIUS ||
                X > gameWindow.Width + RADIUS ||
                Y < -RADIUS ||
                Y > gameWindow.Height + RADIUS;
        }

        public void Draw()
        {
            SplashKit.FillCircle(
                Color.Blue,
                X,
                Y,
                RADIUS
            );
        }
    }
}
