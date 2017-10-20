using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading;
using NUnit.Framework;
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

        static void DemoMode(Action action, string designPaternName)
        {
            Console.WriteLine($"---------------{designPaternName}---------------");
            action.Invoke();
            Console.WriteLine();
        }

        static void DemoSingleton()
        {
            var loggerA = ConsoleLogger.GetLogger();
            var loggerB = ConsoleLogger.GetLogger();
            Console.WriteLine(ReferenceEquals(loggerA, loggerB) ? "loggerA == loggerB" : "loggerA != loggerB");
        }

        static void DemoFactories()
        {
            var guardianA = GuardianFactoryProducer.GetFactory(Archer).Value.CreateGuardian(Dark).Value;
            var guardianB = GuardianFactoryProducer.GetFactory(Wizard).Value.CreateGuardian(Fire).Value;

            var interactiveGuardianA = new ArcherLogger("Dark Archer", guardianA, ConsoleLogger.GetLogger());
            var interactiveGuardianB = new WizardLogger("Fire Wizard", guardianB, ConsoleLogger.GetLogger());

            interactiveGuardianA.Attack();
            interactiveGuardianA.ActivateChargeAttack();

            ConsoleLogger.GetLogger().Log(string.Empty);

            interactiveGuardianB.Attack();
            interactiveGuardianB.ActivateChargeAttack();
        }

        private static void DemoDecorator()
        {
            var guardianA = GuardianFactoryProducer.GetFactory(Archer).Value.CreateGuardian(Dark).Value;
            var guardianB = GuardianFactoryProducer.GetFactory(Wizard).Value.CreateGuardian(Fire).Value;

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
            var arrowPrototype = new Arrow { Location = new Vector2(0) };

            var arrowA = new ArrowLogger("Arrow A", (IProjectile)arrowPrototype.Spawn(), ConsoleLogger.GetLogger());
            var arrowB = new ArrowLogger("Arrow B", (IProjectile)arrowPrototype.Spawn(), ConsoleLogger.GetLogger());

            Console.WriteLine("#Moving arrow A:");

            arrowA.Move(new Vector2(100, 50));
            arrowA.Move(new Vector2(100, 50));

            Console.WriteLine();

            Console.WriteLine("#Moving arrow B:");

            arrowB.Move(new Vector2(100, 25));
            arrowB.Move(new Vector2(100, 25));

            Console.WriteLine();

            Console.WriteLine("#Moving arrow A:");

            arrowA.Move(new Vector2(100, 50));
            arrowA.Move(new Vector2(100, 50));

        }

        private static void DemoStrategy()
        {
            var arrowARaw = new Arrow { Location = new Vector2(0) };
            var arrowBRaw = new Arrow { Location = new Vector2(0), MoveType = new ArchMove() };

            var arrowA = new ArrowLogger("Arrow A", arrowARaw, ConsoleLogger.GetLogger());
            var arrowB = new ArrowLogger("Arrow B", arrowBRaw, ConsoleLogger.GetLogger());
            
            Console.WriteLine("#Moving arrow A in Line:");

            for (int i = 0; i <= 10; i++)
                arrowA.Move(new Vector2(100, 0));

            Console.WriteLine();

            Console.WriteLine("#Moving arrow B in Arch:");

            for(int i = 0; i <= 10; i++)
                arrowB.Move(new Vector2(100, 0));
        }
    }
}
