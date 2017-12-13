using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TowerDefense.Source.Guardians;

namespace TowerDefense.Source.Monsters
{
    public interface IMonsterFactory
    {
        Maybe<IMonster> CreateMonster(MonsterType monsterType);
    }

    internal class MonsterFactory : IMonsterFactory
    {
        public Maybe<IMonster> CreateMonster(MonsterType monsterType) =>
            monsterType == MonsterType.Bubble ? (Monster)new Bubble(1, new Vector2(0, 0), 1) :
            monsterType == MonsterType.Skull ? new Skull() : null;
    }
}
