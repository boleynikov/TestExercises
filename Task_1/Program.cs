using System;

namespace TestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger("Results.txt");
            var board = new Board(9, logger);

            board.ShowBoard("Initial state:");
            var matches = board.LookForMatches();
            while (matches.Count != 0)
            {
                Console.WriteLine($"Items to remove: {matches.Count}");

                board.FillByMatchNodes(matches);
                Console.WriteLine();
                board.ShowBoard($"Deleted {matches.Count} match items:");
                matches = board.LookForMatches();
            }
        }
    }
}
