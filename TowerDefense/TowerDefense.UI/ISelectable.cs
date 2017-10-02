using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public interface ISelectable
    {
        void OnSelect();
        void OnDeselect();
    }
}
