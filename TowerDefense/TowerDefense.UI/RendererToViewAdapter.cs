using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TowerDefense.GameEngine;
using TowerDefense.Source;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Monsters;

namespace TowerDefense.UI
{
    public class RendererToViewAdapter : IRenderer
    {
        private readonly Tower m_tower;
        private readonly ImageRepository m_imageRepository = ImageRepository.GetInstance();

        public IView RenderingView { get; set; }

        public RendererToViewAdapter(IView renderingView = null)
        {
            m_tower = new Tower(new Vector2(300, 780), new Vector2(125, 125));
            RenderingView = renderingView;
        }

        public void Render(Source.ITower tower, IEnumerable<IMonster> monsters, IEnumerable<Projectile> projectiles)
        {
            if (m_tower.Level != tower.GuardianSpace.Blocks)
                m_tower.Level = tower.GuardianSpace.Blocks;

            RenderingView?.Render(new List<IRenderable> { m_tower }.Concat(ProcessGuardians(tower.GuardianSpace.TowerBlocks))
                .Concat(ProcessMonsters(monsters)).Concat(ProcessProjectiles(projectiles)));
        }

        private IEnumerable<IRenderable> ProcessGuardians(IEnumerable<TowerBlock> blocks)
        {
            return blocks.Select(b => Tuple.Create(b.Guardian, b.BlockNumber)).Where(t => t.Item1 != null).Select(
                t => new Guardian(t.Item1.GetType(), t.Item1.UpgradeCost, t.Item1.PromoteCost, t.Item1.ChargeAttackCost, t.Item1.Level, t.Item1.PromoteLevel)
            {
                Image = m_imageRepository.GetImage(t.Item1.GetType()),
                //Position = new Vector2(350, 60 + 125 * t.Item2),
                Position = Vector2.Zero,
                Size = new Vector2(50, 50),
                Index = t.Item2 - 1
            });
            //while (m_guardianSlots.Count != blocks.Count)
            //{
            //    m_guardianSlots.Add(new GuardianSlot());
            //}
            //foreach (var towerBlock in blocks)
            //{
            //    if (!GuardiansEqual(towerBlock.Guardian, m_guardianSlots[towerBlock.BlockNumber].Guardian))
            //    {
            //        m_guardianSlots[towerBlock.BlockNumber].Guardian = new Guardian(towerBlock.Guardian.GetType())
            //        {
            //            Image = m_imageRepository.GetImage(towerBlock.Guardian.GetType()),
            //            Position = Vector2.Zero,
            //            Size = new Vector2(100, 100)
            //        };
            //    }
            //}
            //return m_guardianSlots;
        }

        private IEnumerable<IRenderable> ProcessMonsters(IEnumerable<IMonster> monsters) =>
            monsters.Select(m => new BasicRenderable
            {
                Image = m_imageRepository.GetImage(m.GetType()),
                Position = m.Location,
                Size = new Vector2(50, 50)
            });

        private IEnumerable<IRenderable> ProcessProjectiles(IEnumerable<Projectile> projectiles) =>
            projectiles.Select(p => new BasicRenderable
            {
                Image = m_imageRepository.GetImage(p.GetType()),
                Position = p.Location,
                Size = new Vector2(40, 40)
            });
    }
}
