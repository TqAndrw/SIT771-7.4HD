using SplashKitSDK;
using System.Collections.Generic;
using System.Diagnostics;

namespace RobotDodge
{
    public class RobotDodge
    {
        private Window _GameWindow;
        private Player _Player;
        private List<Robot> _Robots;
        private List<Func<Robot>> _RobotFactories;
        private List<Bullet> _Bullets;
        private Stopwatch _SpawnTimer;
        private int _NextSpawnTime;

        public bool Quit
        {
            get
            {
                return _Player.Quit;
            }
        }

        public RobotDodge(Window gameWindow)
        {
            _GameWindow = gameWindow;
            _Player = new Player(_GameWindow);
            _Robots = new List<Robot>();
            _Bullets = new List<Bullet>();

            _RobotFactories = new List<Func<Robot>>
            {
                () => new Boxy(_GameWindow, _Player),
                () => new Roundy(_GameWindow, _Player),
                () => new Cyclops(_GameWindow, _Player),
                () => new Hunter(_GameWindow, _Player)
            };

            _SpawnTimer = new Stopwatch();
            _SpawnTimer.Start();
            _NextSpawnTime = 1000 + SplashKit.Rnd(1000);
        }

        public void HandleInput()
        {
            _Player.HandleInput();
            _Player.StayOnWindow(_GameWindow);

            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                Point2D target = SplashKit.MousePosition();

                Point2D playerCentre = SplashKit.PointAt(
                    _Player.X + _Player.Width / 2.0,
                    _Player.Y + _Player.Height / 2.0
                );

                Vector2D shotDirection = SplashKit.VectorPointToPoint(
                    playerCentre,
                    target
                );

                if (shotDirection.X != 0 || shotDirection.Y != 0)
                {
                    _Bullets.Add(new Bullet(_Player, target));
                }
            }
        }

        public Robot RandomRobot()
        {
            int index = SplashKit.Rnd(_RobotFactories.Count);
            return _RobotFactories[index]();
        }

        private void CheckCollisions()
        {
            List<Robot> removedRobots = new List<Robot>();
            List<Bullet> removedBullets = new List<Bullet>();
            bool lifeLostThisUpdate = false;

            foreach (Robot robot in _Robots)
            {
                if (_Player.CollidedWith(robot))
                {
                    if (!lifeLostThisUpdate)
                    {
                        _Player.LoseLife();
                        lifeLostThisUpdate = true;
                    }

                    removedRobots.Add(robot);
                }
                else if (robot.IsOffscreen(_GameWindow))
                {
                    removedRobots.Add(robot);
                }
            }

            foreach (Bullet bullet in _Bullets)
            {
                if (bullet.IsOffscreen(_GameWindow))
                {
                    removedBullets.Add(bullet);
                    continue;
                }

                foreach (Robot robot in _Robots)
                {
                    if (removedRobots.Contains(robot))
                    {
                        continue;
                    }

                    if (bullet.CollidedWith(robot))
                    {
                        removedRobots.Add(robot);
                        removedBullets.Add(bullet);
                        break;
                    }
                }
            }

            foreach (Robot robot in removedRobots)
            {
                _Robots.Remove(robot);
            }

            foreach (Bullet bullet in removedBullets)
            {
                _Bullets.Remove(bullet);
            }
        }

        public void Update()
        {
            if (_SpawnTimer.ElapsedMilliseconds >= _NextSpawnTime)
            {
                _Robots.Add(RandomRobot());

                _SpawnTimer.Restart();
                _NextSpawnTime = 1000 + SplashKit.Rnd(1000);
            }

            foreach (Robot robot in _Robots)
            {
                robot.Update();
            }

            foreach (Bullet bullet in _Bullets)
            {
                bullet.Update();
            }

            CheckCollisions();
        }

        public void Draw()
        {
            _GameWindow.Clear(Color.White);

            foreach (Robot robot in _Robots)
            {
                robot.Draw();
            }

            foreach (Bullet bullet in _Bullets)
            {
                bullet.Draw();
            }

            _Player.Draw();
            _Player.DrawStatus();
            _GameWindow.Refresh(60);
        }
    }
}
