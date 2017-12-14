using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefense.Source
{
    public static class Constants
    {
        public static class GameEngineSettings
        {
            public static int GameCyclesPerSecond = 1;
            public static double GameCycleInSeconds => 1.0 / GameCyclesPerSecond;
        }

        public static class ConfigurationSettings
        {
            public static int BaseGuardianCreationCost = 100;
            public static int DistanceToTower = 100;
            public static int GuardianShootingHeightInBlock = 5;
            public static int TowerBaseHeight = 20;
            public static int TowerBlockHeight = 10;
            public static int TowerWidth = 30;
        }

        public static class AttackSpeedMultiplier
        {
            public const double SingleArrow = 1.05;
            public const double DoubleArrow = 1.06;
            public const double TripleArrow = 1.07;
            public const double SingleMageBall = 1.02;
            public const double DoubleMageBall = 1.03;
            public const double TripleMageBall = 1.04;
            public const double TripleScatteredMageBall = 1.05;
            public const double MageBallDoom = 1.05;
        }

        public static class AttackSpeedBase
        {
            public const double SingleArrow = 1;
            public const double DoubleArrow = 0.7;
            public const double TripleArrow = 0.5;
            public const double SingleMageBall = 0.6;
            public const double DoubleMageBall = 0.5;
            public const double TripleMageBall = 0.4;
            public const double TripleScatteredMageBall = 0.3;
            public const double MageBallDoom = 0.2;
        }

        public static class ChargeAttackCostBase
        {
            public const int DarkArcher = 20;
            public const int LightArcher = 25;
            public const int IceWizard = 10;
            public const int FireWizard = 15;
        }

        public static class ChargeAttackTimerBase
        {
            public const double DarkArcher = 10;
            public const double LightArcher = 15;
            public const double IceWizard = 5;
            public const double FireWizard = 10;
        }

        public static class ChargeAttackTimerMultiplier
        {
            public const double DarkArcher = 1.02;
            public const double LightArcher = 1.03;
            public const double IceWizard = 1.04;
            public const double FireWizard = 1.05;
        }

        public static class GuardianUpgradeCostBase
        {
            public const int DarkArcher = 50;
            public const int LightArcher = 100;
            public const int IceWizard = 150;
            public const int FireWizard = 200;
        }

        public static class GuardianUpgradeCostMultiplier
        {
            public const double DarkArcher = 1.40;
            public const double LightArcher = 1.35;
            public const double IceWizard = 1.30;
            public const double FireWizard = 1.25;
        }

        public static class GuardianPromoteCostBase
        {
            public const int DarkArcher = 2500;
            public const int LightArcher = 2500;
            public const int IceWizard = 2000;
            public const int FireWizard = 2000;
        }

        public static class GuardianPromoteCostMultiplier
        {
            public const double DarkArcher = 3.5;
            public const double LightArcher = 2.5;
            public const double IceWizard = 2.0;
            public const double FireWizard = 3.0;
        }

        public static class GuardianPromotionsCount
        {
            public const int DarkArcher = 2;
            public const int LightArcher = 2;
            public const int IceWizard = 4;
            public const int FireWizard = 4;
        }

        public static class GuardianPromotionLevels
        {
            public static readonly int[] DarkArcher = { 10, 20 };
            public static readonly int[] LightArcher = { 10, 20 };
            public static readonly int[] IceWizard = { 15, 25, 35, 45 };
            public static readonly int[] FireWizard = { 15, 25, 35, 45 };
        }

        public static class ProjectileDamageBase
        {
            public const int Arrow = 30;
            public const int SuperArrow = 50;
            public const int MageBall = 20;
            public const int MegaMageBall = 40;
            public const int UltimateMageBall = 100;
        }

        public static class ProjectileDamageMultiplier
        {
            public const double Arrow = 1.1;
            public const double SuperArrow = 1.15;
            public const double MageBall = 1.06;
            public const double MegaMageBall = 1.1;
            public const double UltimateMegaBall = 1.2;
        }

        public static class ProjectileSpeedBase
        {
            public const double Arrow = 10;
            public const double SuperArrow = 15;
            public const double MageBall = 4;
            public const double MegaMageBall = 6;
            public const double UltimateMageBall = 8;
        }

        public static class ProjectileSpeedMultiplier
        {
            public const double Arrow = 1.05;
            public const double SuperArrow = 1.06;
            public const double MageBall = 1.04;
            public const double MegaMageBall = 1.03;
            public const double UltimateMegaBall = 1.02;
        }
    }
}
