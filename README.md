# SIT771 7.4HD — RobotDodge Extension

This repository contains the extended RobotDodge project developed for SIT771 Object-Oriented Development.

## Key extensions

- Runtime polymorphism through `virtual` / `override` movement behaviour.
- `Boxy` uses straight movement.
- `Roundy` continuously tracks the player.
- `Cyclops` uses sinusoidal zigzag movement.
- `Hunter` tracks the player and accelerates over time.
- Delegate-based robot spawning using `List<Func<Robot>>` and lambda expressions.
- Zero-vector shooting guard.
- One-life-per-update collision policy.
- Time-based spawning using `Stopwatch` instead of frame-based probability checks.

## Project structure

- `Program.cs` — game entry point and main loop.
- `RobotDodge.cs` — game manager, spawning, updates and collision handling.
- `Robot.cs` — abstract robot base class and robot subtypes.
- `Player.cs` — player movement, lives and score.
- `Bullet.cs` — projectile movement and collision behaviour.
- `SplashKit.cs` — SplashKit C# bindings used by the project.
- `Resources/images/Player.png` — player image resource.

## Build

The project targets `.NET 10.0` and requires the SplashKit native runtime to be installed in the execution environment.

```bash
dotnet build
dotnet run
```

## SIT771 7.4H focus

The extension was designed to show abstraction, encapsulation, inheritance, runtime polymorphism, delegates/lambdas, debugging, iterative testing, design evaluation, and evidence-based justification of the final implementation.
