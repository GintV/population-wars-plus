namespace TowerDefense.UI.MockEngine
{
    public interface IGame
    {
        void ConstructEnemy();
        void ConstructProjectile();
        void DoStuff();
        void UpgradeTower();
        void GameLoop();
    }
}
