using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefense.Source
{
    public interface IConfiguration
    {
        /// <summary>Distance between monster spawn location and tower.</summary>
        int DistanceToTower { get; }
        /// <summary>Height between ground and first to.</summary>
        int TowerBaseHeight { get; }
        /// <summary>Height of one of the tower blocks.</summary>
        int TowerBlockHeight { get; }
        
    }

    internal class Configuration : IConfiguration
    {
        public Configuration(int distanceToTower, int towerBaseHeight, int towerBlockHeight)
        {
            DistanceToTower = distanceToTower;
            TowerBaseHeight = towerBaseHeight;
            TowerBlockHeight = towerBlockHeight;
        }

        public int DistanceToTower { get; }
        public int TowerBaseHeight { get; }
        public int TowerBlockHeight { get; }
    }
}
