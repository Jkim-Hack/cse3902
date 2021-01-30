/*
 * GitHub Test - Names
 * 
 * Pranav Koduri
 * Dev Patel
 * John Kim
 * Smera Palanivel
 * Alex Book
 * Andrew Fecher
 */

using System;

namespace cse3902
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
}
