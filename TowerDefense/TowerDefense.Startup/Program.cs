using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using TowerDefense.Source;
using TowerDefense.Source.Guardians;
using TowerDefense.Source.Guardians.Wizards;
using TowerDefense.Source.Guardians.Archers;
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
    }
}
