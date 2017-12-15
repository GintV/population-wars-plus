using System;
using System.Collections.Generic;
using System.Text;
using TowerDefense.Source;

namespace TowerDefense.GameEngine
{

    public abstract class InventoryInfo
    {
        protected readonly List<IIventoryInfoSubscriber> m_subscribers;

        public abstract IInventory Inventory { get; }

        protected InventoryInfo()
        {
            m_subscribers = new List<IIventoryInfoSubscriber>();
        }

        public virtual void Subscribe(IIventoryInfoSubscriber subscriber)
        {
            m_subscribers.Add(subscriber);
        }

        public virtual void Unsubscribe(IIventoryInfoSubscriber subscriber)
        {
            m_subscribers.Remove(subscriber);
        }

        public virtual void OnInventoryChanged()
        {
            foreach (var subscriber in m_subscribers)
            {
                subscriber.OnInventoryChange();
            }
        }
    }

    public class InventoryInfoProvider : InventoryInfo
    {
        public override IInventory Inventory { get; }

        public InventoryInfoProvider(IInventory inventory)
        {
            Inventory = inventory;
        }
    }

    public interface IIventoryInfoSubscriber
    {
        void OnInventoryChange();
    }
}
