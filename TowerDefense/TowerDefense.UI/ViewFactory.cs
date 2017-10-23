using TowerDefense.GameEngine;

namespace TowerDefense.UI
{
    public class ViewFactory
    {
        public static IView CreateView(ViewType type, GameInfo gameInfo) // take game state as param?
        {
            switch (type)
            {
                case ViewType.NewGameView:
                    return new NewGameView(GameView.GetInstance(), gameInfo);
                case ViewType.StartedGameView:
                    return new StartedGameView(GameView.GetInstance());
                default:
                    return null;
            }
        }
    }
}
