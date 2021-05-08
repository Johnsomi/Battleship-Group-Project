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

                            if (brainGrid._grid[spot.X, spot.Y] == BrainGrid.Thought.Me)
                            {
                                throw new ArgumentException("One of the players has an overlapping ship");
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


        /*

        int shipCounter = 0, trend = 0;
        static Random rnd = new Random();
        bool gameOver = false, playerTurn = false;
        int[] score = { 0, 0 };

        struct gameData
        {
            public bool occupied, hit, marked;
        }
        gameData[,,] data;



        public void computerMove()
        {
            Position target = seekTarget();

            try
            {
                if (data[1, target.X, target.Y].hit)
                    computerMove();
                else
                {
                    data[1, target.X, target.Y].hit = true;
                    if (data[1, target.X, target.Y].occupied)
                    {
                        //attacking = true;
                        score[0]++;
                        computerMove();
                    }
                }

                playerTurn = true;
            }
            catch (IndexOutOfRangeException)
            { computerMove(); }
        }

        public Position seekTarget()
        {
            Position origin = new Position(-1, -1);

            //find a Position that's been hit.
            int x = 0, y = 0;

            while (x < _gridSize && y < _gridSize)
            {
                if (data[1, x, y].hit && data[1, x, y].occupied && !data[1, x, y].marked)
                {
                    origin = new Position(x, y);
                    break;
                }
                x++;
                if (x == _gridSize && y != _gridSize)
                {
                    x = 0;
                    y++;
                }
            }

            return findTargets(origin);
        }

        public Position findTargets(Position origin)
        {
            Position[] lim = { origin, origin, origin, origin };
            Position[] possibleTargets = { origin, origin, origin, origin };

            //Find the edges.

            while (lim[0].X >= -1 && ((!data[1, lim[0].X, lim[0].Y].hit && !data[1, lim[0].X, lim[0].Y].occupied)
                || (data[1, lim[0].X, lim[0].Y].hit && data[1, lim[0].X, lim[0].Y].occupied)))
            {
                lim[0].X--;
                if (lim[0].X == -1)
                    break;
            }
            while (lim[1].Y >= -1 && ((!data[1, lim[0].X, lim[0].Y].hit && !data[1, lim[0].X, lim[0].Y].occupied)
                || (data[1, lim[0].X, lim[0].Y].hit && data[1, lim[0].X, lim[0].Y].occupied)))
            {
                lim[1].Y--;
                if (lim[1].Y == -1)
                    break;
            }
            while (lim[2].X <= _gridSize && ((!data[1, lim[0].X, lim[0].Y].hit && !data[1, lim[0].X, lim[0].Y].occupied)
                || (data[1, lim[0].X, lim[0].Y].hit && data[1, lim[0].X, lim[0].Y].occupied)))
            {
                lim[2].X++;
                if (lim[2].X == _gridSize)
                    break;
            }
            while (lim[3].Y <= _gridSize && ((!data[1, lim[0].X, lim[0].Y].hit && !data[1, lim[0].X, lim[0].Y].occupied)
                || (data[1, lim[0].X, lim[0].Y].hit && data[1, lim[0].X, lim[0].Y].occupied)))
            {
                lim[3].Y++;
                if (lim[3].Y == _gridSize)
                    break;
            }

            return new Position(rnd.Next(10), rnd.Next(10));
        }*/
    }

}
