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

            // Factory
            Console.WriteLine("\n\n======== Bandomas Factory");
            DemoFactory();

            // Abstract Factory
            Console.WriteLine("\n\n======== Bandomas Abstract Factory");
            DemoAbstractFactory();

            // Strategy
            Console.WriteLine("\n\n======== Bandomas Strategy");
            DemoStrategy();

            // Strategy
            Console.WriteLine("\n\n======== Bandomas Observer");
            DemoObserver();

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

        static void DemoFactory()
        {
            var factory = WizardFactory.GetFactory();

            GuardianType fireWizardType = new GuardianType(GuardianClass.Wizard, GuardianSpecialization.Fire);
            GuardianType iceWizardType = new GuardianType(GuardianClass.Wizard, GuardianSpecialization.Ice);

            var fireWizard = factory.CreateGuardian(fireWizardType).Value;
            var iceWizard = factory.CreateGuardian(iceWizardType).Value;

            Assert.AreEqual(fireWizard.GetType(), typeof(FireWizard));
            Assert.AreEqual(iceWizard.GetType(), typeof(IceWizard));
            Console.WriteLine("Factory sukure du skirtingus objektus. Factory veikia.");

        }

        static void DemoAbstractFactory()
        {
            GuardianType fireWizardType = new GuardianType(GuardianClass.Wizard, GuardianSpecialization.Fire);
            GuardianType darkArcherType = new GuardianType(GuardianClass.Archer, GuardianSpecialization.Dark);

            var fireWizard = AbstractGuardianFactory.CreateGuardian(fireWizardType).Value;
            var darkArcher = AbstractGuardianFactory.CreateGuardian(darkArcherType).Value;

            Assert.AreEqual(fireWizard.GetType(), typeof(FireWizard));
            Assert.AreEqual(darkArcher.GetType(), typeof(DarkArcher));
            Console.WriteLine("Abstraktus factory sukure du skirtingus objektus is skirtingu objektu seimu. Abstract Factory veikia.");
        }

        static void DemoStrategy()
        {

        }

        static void DemoObserver()
        {

        }
    }
}
