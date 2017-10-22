using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefense.Source
{
    public interface IConfiguration
    {
        /// <summary>Base cost of all guardians creation.</summary>
        int BaseGuardianCreationCost { get; }
        /// <summary>Distance between monster spawn location and tower.</summary>
        int DistanceToTower { get; }
        /// <summary>Height from which guardian is shooting from block.</summary>
        int GuardianShootingHeightInBlock { get; }
        /// <summary>Height between ground and first tower block.</summary>
        int TowerBaseHeight { get; }
        /// <summary>Height of one of the tower blocks.</summary>
        int TowerBlockHeight { get; }
        /// <summary>Width of tower base and tower blocks.</summary>
        int TowerWidth { get; }
    }
}
