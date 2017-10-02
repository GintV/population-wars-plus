using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public interface IMovable : IRenderable
    {
        void Move(long dTime);
    }
}
