namespace TowerDefense.UI
{
    public class ViewFactory
    {
        public static IView CreateView(ViewType type, Game game) // take game state as param?
        {
            switch (type)
            {
                case ViewType.NewGameView:
                    return new NewGameView(GameView.GetInstance(), game);
                case ViewType.StartedGameView:
                    return new StartedGameView(GameView.GetInstance());
                default:
                    return null;
            }
        }
    }
}
