using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading;
using NUnit.Framework;
using TowerDefense.GameEngine;
using TowerDefense.Source;
using TowerDefense.Source.Guardians;
using TowerDefense.Source.Guardians.Wizards;
using TowerDefense.Source.Guardians.Archers;
using TowerDefense.Source.Attacks;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Attacks.Projectiles.MoveTypes;
using TowerDefense.Source.Loggers;
using static TowerDefense.Source.Flags.GuardianClass;
using static TowerDefense.Source.Flags.GuardianType;

namespace TowerDefense.Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoMode(DemoSingleton, "Singleton");
            DemoMode(DemoFactories, "Factories");
            DemoMode(DemoDecorator, "Decorator");
            DemoMode(DemoPrototype, "Prototype");
            DemoMode(DemoStrategy, "Strategy");

            Console.ReadLine();
        }

        static void DemoMode(Action action, string designPatternName)
        {
            Console.WriteLine($"---------------{designPatternName}---------------");
            action.Invoke();
            Console.WriteLine();
        }

        static void DemoSingleton()
        {
            var gameHandlerA = GameHandler.GetHandler();
            var gameHandlerB = GameHandler.GetHandler();
            Console.WriteLine(ReferenceEquals(gameHandlerA, gameHandlerB) ? "gameHandlerA == gameHandlerB" : "gameHandlerA != gameHandlerB");
        }

        static void DemoFactories()
        {
            var guardianA = GuardianFactoryProvider.GetFactory(Archer).Value.CreateGuardian(Dark).Value;
            var guardianB = GuardianFactoryProvider.GetFactory(Wizard).Value.CreateGuardian(Fire).Value;

            var interactiveGuardianA = new ArcherLogger("Dark Archer", guardianA, ConsoleLogger.GetLogger());
            var interactiveGuardianB = new WizardLogger("Fire Wizard", guardianB, ConsoleLogger.GetLogger());

            interactiveGuardianA.Attack(Vector2.Zero, 0, 0);
            interactiveGuardianA.ActivateChargeAttack();

            ConsoleLogger.GetLogger().Log(string.Empty);

            interactiveGuardianB.Attack(Vector2.Zero, 0, 0);
            interactiveGuardianB.ActivateChargeAttack();
        }

        private static void DemoDecorator()
        {
            var guardianA = GuardianFactoryProvider.GetFactory(Archer).Value.CreateGuardian(Dark).Value;
            var guardianB = GuardianFactoryProvider.GetFactory(Wizard).Value.CreateGuardian(Fire).Value;

            var interactiveGuardianA = new ArcherLogger("Dark Archer", guardianA, ConsoleLogger.GetLogger());
            var interactiveGuardianB = new WizardLogger("Fire Wizard", guardianB, ConsoleLogger.GetLogger());

            Console.WriteLine("#Simple Guardians:");

            guardianA.Promote();
            guardianA.Upgrade();

            guardianB.Promote();
            guardianB.Upgrade();

            Console.WriteLine();

            Console.WriteLine("#Interactive Guardians:");

            interactiveGuardianA.Promote();
            interactiveGuardianA.Upgrade();

            ConsoleLogger.GetLogger().Log(string.Empty);

            interactiveGuardianB.Promote();
            interactiveGuardianB.Upgrade();
        }

        private static void DemoPrototype()
        {
            // Archer has this phantom arrow spawner
            var arrowPrototype = new Arrow();
            arrowPrototype.MoveType.Initialize(new Vector2(0, 0), arrowPrototype.Speed, new Vector2(101, 0), 5);

            var arrowA = new ArrowLogger("Arrow A", (Projectile)arrowPrototype.Clone(), ConsoleLogger.GetLogger());
            var arrowB = new ArrowLogger("Arrow B", (Projectile)arrowPrototype.Clone(), ConsoleLogger.GetLogger());
            

            Console.WriteLine("#Moving arrow A:");

            arrowA.Move();
            arrowA.Move();

            Console.WriteLine();

            Console.WriteLine("#Moving arrow B:");

            arrowB.Move();
            arrowB.Move();

            Console.WriteLine();

            Console.WriteLine("#Moving arrow A:");

            arrowA.Move();
            arrowA.Move();
            arrowA.Move();

        }

        private static void DemoStrategy()
        {
            var arrowARaw = new Arrow { Location = new Vector2(0) };
            arrowARaw.MoveType.Initialize(new Vector2(0, 0), arrowARaw.Speed, new Vector2(101, 0), 5);

            var arrowBRaw = new Arrow { Location = new Vector2(0), MoveType = new LineMove() };
            arrowBRaw.MoveType.Initialize(new Vector2(0, 0), arrowBRaw.Speed, new Vector2(101, 0), 5);

            var arrowA = new ArrowLogger("Arrow A", arrowARaw, ConsoleLogger.GetLogger());
            var arrowB = new ArrowLogger("Arrow B", arrowBRaw, ConsoleLogger.GetLogger());
            
            Console.WriteLine("#Moving arrow A in Arch:");

            for (int i = 0; i <= 10; i++)
                arrowA.Move();

            Console.WriteLine();

            Console.WriteLine("#Moving arrow B in Line:");

            for(int i = 0; i <= 10; i++)
                arrowB.Move();
        }
    }
}
