# SIT771 7.4HD — RobotDodge Extension

This repository contains the extended RobotDodge project developed for SIT771 Object-Oriented Development.

## Key extensions

- Runtime polymorphism through `virtual` / `override` movement behaviour.
- `Boxy` uses straight movement.
- `Roundy` continuously tracks the player.
- `Cyclops` uses sinusoidal zigzag movement.
- `Hunter` tracks the player and accelerates over time.
- Delegate-based robot spawning using `List<Func<Robot>>` and lambda expressions.
- Zero-vector shooting guard before a bullet is created.
- One-life-per-update collision policy.
- Time-based spawning using `Stopwatch` instead of frame-based probability checks.

## Project structure

- `Program.cs` — game entry point and main loop.
- `RobotDodge.cs` — game manager, spawning, updates and collision handling.
- `Robot.cs` — abstract robot base class and robot subtypes.
- `Player.cs` — player movement, lives and score.
- `Bullet.cs` — projectile movement and collision behaviour.
- `Resources/images/Player.png` — player image resource.

## SplashKit prerequisite

The large `SplashKit.cs` C# binding used in the local Deakin/SplashKit project is external/generated support code rather than part of the 7.4H implementation, so it is not committed here. To build this repository in the same environment, place the corresponding generated `SplashKit.cs` binding in the project root and ensure the SplashKit native runtime is installed.

## Build

The project targets `.NET 10.0`.

```bash
dotnet build
dotnet run
```

## SIT771 7.4H focus

The extension was designed to show abstraction, encapsulation, inheritance, runtime polymorphism, delegates/lambdas, debugging, iterative testing, design evaluation, and evidence-based justification of the final implementation.
