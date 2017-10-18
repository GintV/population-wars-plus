using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using TowerDefense.GameEngine;
using TowerDefense.Source;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Guardians.Archers;
using TowerDefense.Source.Guardians.Wizards;
using TowerDefense.Source.Monsters;
using TowerDefense.UI.Properties;

namespace TowerDefense.UI
{
    public class RendererToViewAdapter : IRenderer
    {
        private readonly Tower m_tower;
        public IView RenderingView { get; set; }

        public RendererToViewAdapter(IView renderingView = null)
        {
            m_tower = new Tower(new Vector2(300, 780), new Vector2(125, 125));
            RenderingView = renderingView;
        }

        public void Render(Source.Tower tower, IEnumerable<IMonster> monsters, IEnumerable<IProjectile> projectiles)
        {
            if (m_tower.Level != tower.GuardianSpace.Blocks)
                m_tower.Level = tower.GuardianSpace.Blocks;

            RenderingView?.Render(new List<IRenderable> { m_tower }.Concat(ProcessGuardians(tower.GuardianSpace.TowerBlocks))
                .Concat(ProcessMonsters(monsters)).Concat(ProcessProjectiles(projectiles)));
        }

        private static IEnumerable<IRenderable> ProcessGuardians(IEnumerable<TowerBlock> blocks)
        {
            return blocks.Select(b => Tuple.Create(b.Guardian, b.BlockNumber)).Where(t => t.Item1 != null).Select(t => new BasicRenderable
            {
                Image = ResolveImageByType(t.Item1.GetType()),
                Position = new Vector2(350, 60 + 125 * t.Item2),
                Size = new Vector2(50, 50)
            });
        }

        private static IEnumerable<IRenderable> ProcessMonsters(IEnumerable<IMonster> monsters) =>
            monsters.Select(m => new BasicRenderable
            {
                Image = ResolveImageByType(m.GetType()),
                Position = m.Location,
                Size = new Vector2(50, 50)
            });

        private static IEnumerable<IRenderable> ProcessProjectiles(IEnumerable<IProjectile> projectiles) =>
            projectiles.Select(p => new BasicRenderable
            {
                Image = ResolveImageByType(p.GetType()),
                Position = p.Location,
                Size = new Vector2(25, 25)
            });

        private static Image ResolveImageByType(Type type)
        {
            if (type == typeof(Bubble))
            {
                return Resources.bubble;
            }
            if (type == typeof(Skull))
            {
                return Resources.skull;
            }
            if (type == typeof(Arrow))
            {
                return Resources.proj;
            }
            if (type == typeof(FireWizard))
            {
                return Resources.enem;
            }
            if (type == typeof(IceWizard))
            {
                return Resources.enem;
            }
            if (type == typeof(DarkArcher))
            {
                return Resources.enem;
            }
            if (type == typeof(LightArcher))
            {
                return Resources.enem;
            }
            throw new Exception("Unsupported type for Image resolution.");
        }
    }
}
