using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.UI
{
    public interface IMovable : IRenderable
    {
        void Move(long dTime);
    }
}
