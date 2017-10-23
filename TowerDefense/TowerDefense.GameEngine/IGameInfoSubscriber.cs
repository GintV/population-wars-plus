namespace TowerDefense.GameEngine
{
    public interface IGameInfoSubscriber
    {
        void OnTowerHealthPointsChanged();
        void OnTowerManaPointsChanged();
        void OnCoinsChanged();
    }
}