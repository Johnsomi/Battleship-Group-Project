using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module8
{
    class BrainGrid
    {
        public enum Thought
        {
            Empty,
            Hit,
            Miss,
            Me
        }
        public Thought[,] _grid;
        public int _gridSize;
        public BrainGrid(int gridSize)
        {

            _gridSize = gridSize;
            _grid = new Thought[gridSize, gridSize];
            //Fill the grid with empty entries marked as not hit
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    _grid[x, y] = Thought.Empty;
                }
            }
        }
    }
}
