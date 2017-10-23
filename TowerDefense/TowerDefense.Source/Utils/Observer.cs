using System;

namespace TowerDefense.Source.Utils
{
    public class Observer<TValue> : Observable<TValue>
    {
        private readonly Observable<TValue> m_observable;
        private readonly Action m_onInvalidation;
        public bool IsValid { get; private set; }

        public Observer(Observable<TValue> observable, Action onInvalidation = null)
        {
            IsValid = false;
            m_observable = observable;
            m_onInvalidation = onInvalidation;
        }

        public override TValue Get(Observer<TValue> observer = null)
        {
            if (!IsValid)
            {
                Set(m_observable.Get(this));
                IsValid = true;
            }
            return base.Get(observer);
        }

        public void Invalidate()
        {
            IsValid = false;
            m_onInvalidation?.Invoke();
        }
    }
}
