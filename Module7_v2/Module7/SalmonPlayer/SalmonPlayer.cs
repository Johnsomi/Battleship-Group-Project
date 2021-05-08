using System;
using System.Collections.Generic;

namespace Module8
{
    internal class SalmonPlayer : IPlayer
    {
        private int _index;
        private static readonly Random Random = new Random();
        private int _gridSize;
        private BrainGrid brainGrid;

        public SalmonPlayer(string name)
        {
            Name = name;
        }

        public void StartNewGame(int playerIndex, int gridSize, Ships ships)
        {
            Random rand = new Random();
            _gridSize = gridSize;
            _index = playerIndex;

            brainGrid = new BrainGrid(gridSize);

            Direction d = (Direction)rand.Next(0, 1);
            Position pos = new Position(rand.Next(0, _gridSize), rand.Next(0, _gridSize));




            // currently this code leaves some of the ships without a position, its not going back to try again properly.

            Ships tempShips = new Ships();
            Ships finalShips = new Ships();

            foreach (var ship in ships._ships)
            {
                while (ship.Positions != null)// if this is true then the ship has a position
                {
                    d = (Direction)rand.Next(0, 1);
                    pos = new Position(rand.Next(0, _gridSize), rand.Next(0, _gridSize));
                    ship.Place(pos, d);



                    tempShips.Add(ship);
                    try
                    {
                        foreach (var spot in ship.Positions)
                        {
                            if (spot.X < 0 || spot.X > _gridSize || spot.Y < 0 || spot.Y >= _gridSize)
                            {
                                throw new ArgumentException("One of the ships is outside the grid");
                            }
                            else if (brainGrid._grid[spot.X, spot.Y] == BrainGrid.Thought.Me)
                            {
                                throw new ArgumentException("One of the players has an overlapping ship");
                            }
                            else
                            {

                            }

                        }
                    }
                    catch (Exception)
                    {
                        ship.Reset();// makes ship.Positions = null
                        continue;
                    }

                }
            }




        }

        public string Name { get; }
        public int Index => _index;

        public Position GetAttackPosition()
        {
            return new Position(0, 0);
        }

        public void SetAttackResults(List<AttackResult> results)
        {
            //Random player does nothing useful with these results, just keeps on making random guesses
        }

    }

}
