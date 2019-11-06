using Parkare.Lince.AutoSync.Services.ObserverLibrary;
using System;

namespace Rayffer.PersonalPortfolio.Automator.ConsoleTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TestClass asd = new TestClass();
            while (true)
            {
                var consoleKey = Console.ReadKey();
                if (consoleKey.Key == ConsoleKey.Enter)
                    break;
            }
        }
    }
}