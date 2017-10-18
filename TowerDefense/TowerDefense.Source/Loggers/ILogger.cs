using System;

namespace TowerDefense.Source.Loggers
{
    public interface ILogger
    {
        void Log(string message);
    }

    public class ConsoleLogger : ILogger
    {
        private static readonly Lazy<ConsoleLogger> Logger = new Lazy<ConsoleLogger>(() => new ConsoleLogger());

        private ConsoleLogger() { }

        public static ConsoleLogger GetLogger() => Logger.Value;

        public void Log(string message) => Console.WriteLine(message);
    }
}
