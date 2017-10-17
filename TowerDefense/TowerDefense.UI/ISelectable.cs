namespace TowerDefense.UI
{
    public interface ISelectable : IClickable
    {
        void OnDeselect();
    }
}
