using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using TowerDefense.Source;
using TowerDefense.Source.Guardians;

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
            
            Console.ReadLine();
        }

        static void DemoSingleton()
        {
            Thread t1 = new Thread(HelperDemoCreateFactoryInstance);
            Thread t2 = new Thread(HelperDemoCreateFactoryInstance);

            WizardFactory f1 = WizardFactory.GetFactory();
            WizardFactory f2 = WizardFactory.GetFactory();

            if(f1 == f2)
            {
                Console.WriteLine("Rodykles rodo i ta pati objekta. Singleton'as veikia.");
            } else
            {
                Console.WriteLine("Objektai nesutampa. Singleton'as neveikia.");
            }

            
        }

        static void HelperDemoCreateFactoryInstance()
        {
            WizardFactory factory = WizardFactory.GetFactory();
        }
    }
}
