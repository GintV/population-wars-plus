using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnit.Framework;
using TowerDefense.Source;
using TowerDefense.Source.Guardians;
using TowerDefense.Source.Guardians.Wizards;
using TowerDefense.Source.Guardians.Archers;

namespace TowerDefense.Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lab1 testai

            // Singleton
            Console.WriteLine("\n\n======== Bandomas Singleton");
            DemoSingleton();

            // Factory / Abstract Factory
            Console.WriteLine("\n\n======== Bandomas Abstract Factory");
            DemoAbstractFactory();

            Console.ReadLine();
        }
        
        static void DemoSingleton()
        {
            Thread t1 = new Thread(HelperDemoCreateFactoryInstance);
            Thread t2 = new Thread(HelperDemoCreateFactoryInstance);

            var f1 = WizardFactory.GetFactory();
            var f2 = WizardFactory.GetFactory();

            Assert.AreEqual(f1, f2);
            Console.WriteLine("Rodykles rodo i ta pati objekta. Singleton'as veikia.");
        }

        static void HelperDemoCreateFactoryInstance()
        {
            var factory = WizardFactory.GetFactory();
        }

        static void DemoAbstractFactory()
        {
            var factory = WizardFactory.GetFactory();

            GuardianType fireWizardType = new GuardianType(GuardianClass.Wizard, GuardianSpecialization.Fire);
            GuardianType iceWizardType = new GuardianType(GuardianClass.Wizard, GuardianSpecialization.Ice);

            var fireWizard = factory.CreateGuardian(fireWizardType).Value;
            var iceWizard = factory.CreateGuardian(iceWizardType).Value;

            Assert.AreEqual(fireWizard.GetType(), typeof(FireWizard));
            Assert.AreEqual(iceWizard.GetType(), typeof(IceWizard));
            Console.WriteLine("Abstraktus factory sukure du skirtingus objektus. Abstract Factory veikia.");

        }
    }
}
