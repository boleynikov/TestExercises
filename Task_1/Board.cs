using System;
using System.Collections.Generic;

namespace TestTask
{
    internal class Board
    {
        private readonly int _size;
        private readonly ILogger _logger;
        private readonly List<List<int>> _matrix;

        public Board(int size, ILogger logger)
        {
            _logger = logger;
            _size = size;
            _matrix = new List<List<int>>();
            for (int i = 0; i < _size; i++)
            {
                _matrix.Add(new List<int>());
            }

            FillBoard();
        }

        public void ShowBoard(string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Console.Write($"{_matrix[i][j]} ");
                }

                Console.WriteLine();
            }

            _logger.WriteLog(message, _matrix);
        }

        public List<Node> LookForMatches()
        {
            var allMatches = new List<Node>();
            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    var match = GetHorizontalMatches(col, row);
                    if (match.Count > 2)
                    {
                        col += match.Count;
                        allMatches.AddRange(match);
                    }
                }
            }

            for (int col = 0; col < _size; col++)
            {
                for (int row = 0; row < _size; row++)
                {
                    var match = GetVerticalMatches(col, row);
                    if (match.Count > 2)
                    {
                        row += match.Count;
                        allMatches.AddRange(match);
                    }
                }
            }

            return allMatches;
        }

        public void FillByNodes(List<Node> allMatches)
        {
            foreach (var node in allMatches)
            {
                SwapValues(node);
            }
        }

        private void FillBoard()
        {
            Random rnd = new Random();
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _matrix[i].Add(rnd.Next(0, 4));
                }
            }
        }

        private void SwapValues(Node node)
        {
            if (node.Row == 0)
            {
                var rnd = new Random();
                _matrix[node.Row][node.Column] = rnd.Next(0, 4);
            }
            else
            {
                _matrix[node.Row][node.Column] = _matrix[--node.Row][node.Column];
                SwapValues(node);
            }
        }

        private List<Node> GetVerticalMatches(int col, int row)
        {
            var match = new List<Node>
            {
                new () { Column = col, Row = row }
            };
            for (var i = 1; row + i < _size; i++)
            {
                if (_matrix[row][col] == _matrix[row + i][col])
                {
                    match.Add(new Node() { Column = col, Row = row + i});
                }
                else
                {
                    return match;
                }
            }
            return match;
        }

        private List<Node> GetHorizontalMatches(int col, int row)
        {
            var match = new List<Node>
            {
                new () { Column = col, Row = row }
            };
            for (var i = 1; col + i < _size; i++)
            {
                if (_matrix[row][col] == _matrix[row][col + i])
                {
                    match.Add(new Node(){Column = col + i, Row = row});
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
