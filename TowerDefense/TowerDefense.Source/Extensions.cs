using System;

namespace TowerDefense.Source
{
    internal static class Extensions
    {
        internal static T If<T>(this T value, Func<T, bool> condition, Action action)
        {
            if(condition.Invoke(value))
                action.Invoke();
            return value;
        }
    }
}
