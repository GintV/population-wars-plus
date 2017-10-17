using System;

namespace TowerDefense.Source
{
    /// <summary>Monad for storing value.</summary>
    /// <typeparam name="T"></typeparam>
    public struct Maybe<T>
    {
        private readonly T m_value;

        /// <summary>The value stored in the monad.</summary>
        /// <value>Gets the value stored in the monad.</value>
        /// <exception cref="InvalidOperationException"> if no value is present.</exception>
        public T Value => HasValue ? m_value : throw new InvalidOperationException();

        /// <summary>The flag of value presence.</summary>
        /// <value>Gets the flag of value presence.</value>
        public bool HasValue { get; }

        /// <summary>Implicit constructor.</summary>
        /// <param name="value">Value to store in monad.</param>
        /// <returns><see cref="Maybe{T}"/> of <paramref name="value"/>.</returns>
        public static implicit operator Maybe<T>(T value) => new Maybe<T>(value);

        /// <summary>Constructor.</summary>
        /// <param name="value">Value to store in monad.</param>
        public Maybe(T value)
        {
            m_value = value;
            HasValue = value != null;
        }
    }
}
