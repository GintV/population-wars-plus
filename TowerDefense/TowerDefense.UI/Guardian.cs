using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.UI
{
    public class Guardian : IRenderable
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Image Image { get; set; }
        public Type ReprestentedType { get; }
        public int Index { get; set; }

        public int UpgradeCost { get; set; }
        public int PromoteCost { get; set; }
        public int ChargedShotCost { get; set; }
        public int Level { get; set; }
        public int PromoteLevel { get; set; }

        public Guardian(Type representedGuardianType, int upgradeCost, int promoteCost, int chargedShotCost, int level, int promoteLevel)
        {
            ReprestentedType = representedGuardianType;
            UpgradeCost = upgradeCost;
            PromoteCost = promoteCost;
            ChargedShotCost = chargedShotCost;
            Level = level;
            PromoteLevel = promoteLevel;
        }

        protected bool Equals(Guardian other)
        {
            return Equals(ReprestentedType, other.ReprestentedType) && UpgradeCost == other.UpgradeCost &&
                   PromoteCost == other.PromoteCost && ChargedShotCost == other.ChargedShotCost &&
                   Level == other.Level && PromoteLevel == other.PromoteLevel;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Guardian)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ReprestentedType != null ? ReprestentedType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ UpgradeCost;
                hashCode = (hashCode * 397) ^ PromoteCost;
                hashCode = (hashCode * 397) ^ ChargedShotCost;
                hashCode = (hashCode * 397) ^ Level;
                hashCode = (hashCode * 397) ^ PromoteLevel;
                return hashCode;
            }
        }
    }
}
