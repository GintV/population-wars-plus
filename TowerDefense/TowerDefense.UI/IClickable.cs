using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.UI
{
    public interface IClickable : IRenderable
    {
        void OnClick(Vector2 clickPosition);
    }
}
