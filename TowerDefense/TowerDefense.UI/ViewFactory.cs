using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.UI
{
    public class ViewFactory
    {
        public static IView CreateView(ViewType type) // take game state as param?
        {
            switch (type)
            {
                case ViewType.NewGameView:
                    return new NewGameView(GameView.GetInstance());
                case ViewType.StartedGameView:
                    return new StartedGameView(GameView.GetInstance());
                default:
                    return null;
            }
        }
    }
}
