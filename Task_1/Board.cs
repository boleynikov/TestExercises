using System;
using System.Collections.Generic;
using System.Threading;

namespace TestTask
{
    internal class Board
    {
        private readonly int _size;
        private readonly ILogger _logger;
        private readonly List<List<int>> _items;

        public Board(int size, ILogger logger)
        {
            _size = size;
            _logger = logger;
            _items = new List<List<int>>();
            for (int i = 0; i < _size; i++)
            {
                _items.Add(new List<int>());
            }

            FillBoard();
        }

        /// <summary>
        /// Show all items on console & log in file
        /// </summary>
        /// <param name="message">Display message</param>
        public void ShowBoard(string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Thread.Sleep(3);
                    Console.Write($"{_items[i][j]} ");
                }

                Console.WriteLine();
            }

            _logger.WriteLog(message, _items);
        }

        /// <summary>
        /// Search for matches at least 3 items
        /// </summary>
        /// <returns></returns>
        public List<Node> LookForMatches()
        {
            var allMatches = new List<Node>();
            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    var horizontalMatches = GetHorizontalMatches(col, row);
                    if (horizontalMatches.Count > 2)
                    {
                        col += horizontalMatches.Count;
                        allMatches.AddRange(horizontalMatches);
                    }
                }
            }

            for (int col = 0; col < _size; col++)
            {
                for (int row = 0; row < _size; row++)
                {
                    var verticalMatches = GetVerticalMatches(col, row);
                    if (verticalMatches.Count > 2)
                    {
                        row += verticalMatches.Count;
                        allMatches.AddRange(verticalMatches);
                    }
                }
            }

            return allMatches;
        }

        /// <summary>
        /// Replace items by match nodes
        /// </summary>
        /// <param name="allMatches">List of match nodes</param>
        public void FillByMatchNodes(List<Node> allMatches)
        {
            foreach (var node in allMatches)
            {
                SwapValues(node);
            }
        }

        /// <summary>
        /// Initial board filling
        /// </summary>
        private void FillBoard()
        {
            var rnd = new Random();
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _items[i].Add(rnd.Next(0, 4));
                }
            }
        }

        /// <summary>
        /// Swapping values until node in on top
        /// </summary>
        /// <param name="node">Match node</param>
        private void SwapValues(Node node)
        {
            if (node.Row == 0)
            {
                var rnd = new Random();
                _items[node.Row][node.Column] = rnd.Next(0, 4);
            }
            else
            {
                _items[node.Row][node.Column] = _items[--node.Row][node.Column];
                SwapValues(node);
            }
        }

        /// <summary>
        /// Search of vertical match in single column
        /// </summary>
        /// <param name="col">Start column</param>
        /// <param name="row">Start row</param>
        /// <returns></returns>
        private List<Node> GetVerticalMatches(int col, int row)
        {
            var match = new List<Node>
            {
                new () { Column = col, Row = row }
            };
            for (var i = 1; row + i < _size; i++)
            {
                if (_items[row][col] == _items[row + i][col])
                {
                    match.Add(new Node() { Column = col, Row = row + i });
                }
                else
                {
                    return match;
                }
            }

            return match;
        }

        /// <summary>
        /// Search of horizontal match in single row
        /// </summary>
        /// <param name="col">Start column</param>
        /// <param name="row">Start row</param>
        /// <returns></returns>
        private List<Node> GetHorizontalMatches(int col, int row)
        {
            var match = new List<Node>
            {
                new () { Column = col, Row = row }
            };
            for (var i = 1; col + i < _size; i++)
            {
                if (_items[row][col] == _items[row][col + i])
                {
                    match.Add(new Node(){ Column = col + i, Row = row });
                }
                else
                {
                    return match;
                }
            }

            return match;
        }
    }
}
