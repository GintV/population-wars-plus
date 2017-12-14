using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Monsters;

namespace TowerDefense.Source.Mediator
{
    public class Mediator
    {
        private List<Projectile> _projectiles;
        private List<Monster> _monsters;

        public Mediator(List<Projectile> projectiles, List<Monster> monsters)
        {
            _projectiles = projectiles;
            _monsters = monsters;
        }

        public void Broadcast(Notifier sender, Vector2 location, int damage)
        {
            switch (sender)
            {
                case Projectile projectile:
                    _monsters.ToList().ForEach(monster => monster.Receive(location, damage));
                    return;
                case Monster monster:
                    _projectiles.ToList().ForEach(projectile => projectile.Receive(location, damage));
                    return;
            }
        }

        public void Remove(Notifier sender)
        {
            switch (sender)
            {
                case Projectile projectile:
                    _projectiles.Remove(projectile);
                    return;
                case Monster monster:
                    _monsters.Remove(monster);
                    return;
            }
        }
    }

    public abstract class Notifier
    {
        private Mediator _mediator;

        public void SetMediator(Mediator mediator) => _mediator = mediator;
        public void Send(Vector2 location, int damage) => _mediator.Broadcast(this, location, damage);
        public abstract void Receive(Vector2 location, int damage);
        public void Destroy() => _mediator.Remove(this);
    }
}
