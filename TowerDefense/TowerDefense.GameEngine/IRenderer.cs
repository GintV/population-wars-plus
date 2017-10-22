using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Monsters;

namespace TowerDefense.GameEngine
{
    public interface IRenderer
    {
        void Render(Tower tower, IEnumerable<IMonster> monsters, IEnumerable<Projectile> projectiles);
    }
}
