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
        private Action<Monster> _monsterDestroyCallback;

        public Mediator(List<Projectile> projectiles, List<Monster> monsters, Action<Monster> monsterDestroyCallback)
        {
            _projectiles = projectiles;
            _monsters = monsters;
            _monsterDestroyCallback = monsterDestroyCallback;
        }

        public void Broadcast(Notifier sender, Vector2 location, int damage)
        {
            switch (sender)
            {
                case Projectile projectile:
                    _monsters.ToList().ForEach(monster => monster.Receive(location, damage, sender));
                    return;
                case Monster monster:
                    _projectiles.ToList().ForEach(projectile => projectile.Receive(location, damage, sender));
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
                    _monsterDestroyCallback(monster);
                    return;
            }
        }
    }

    public abstract class Notifier
    {
        private Mediator _mediator;

        public void SetMediator(Mediator mediator) => _mediator = mediator;
        public void Send(Vector2 location, int damage) => _mediator.Broadcast(this, location, damage);
        public abstract void Receive(Vector2 location, int damage, Notifier sender);
        public void Destroy() => _mediator.Remove(this);
    }
}
