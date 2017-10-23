using System.Collections.Generic;

namespace TowerDefense.Source.Utils
{
    public class Observable<TValue>
    {
        protected TValue m_value;
        private readonly List<Observer<TValue>> m_dependantObservers;

        public Observable()
        {
            m_dependantObservers = new List<Observer<TValue>>();
        }

        public virtual TValue Get(Observer<TValue> observer = null)
        {
            if (observer != null)
                m_dependantObservers.Add(observer);
            return m_value;
        }

        public void Set(TValue value)
        {
            var currentObservers = m_dependantObservers.ToArray();
            m_dependantObservers.Clear();
            foreach (var observer in currentObservers)
            {
                observer.Invalidate();
            }
            m_value = value;
        }
    }
}
