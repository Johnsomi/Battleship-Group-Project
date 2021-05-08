/*
 * Authors: Greg Marshall, Matthew Cassidy-Hanson, Jacob Mroz, Mitchell Johnson
 * Date Last Modified: 5/8/2021
 * Purpose: Battleship AI final Project-Team Salmon
 */

using System;
using System.Collections.Generic;

namespace CS3110_Module_8_Group
{ 


    internal class SalmonPlayer : IPlayer
    {
        private int _index;
        private static readonly Random Random = new Random();
        private int _gridSize;
        private BrainGrid brainGrid;
        private static readonly List<Position> Guesses = new List<Position>();

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

            if (Guesses.Count < _gridSize * _gridSize) // control for fallback firing
            {
                Guesses.Clear();
                for (int x = 0; x < _gridSize; x++)
                {
                    for (int y = 0; y < _gridSize; y++)
                    {
                        Guesses.Add(new Position(x, y));
                    }
                }
            }


            // currently this code leaves some of the ships without a position, its not going back to try again properly.

            Ships tempShips = new Ships();

            foreach (var ship in ships._ships)
            {
                while (ship.Positions == null)// if this is true then the ship has a position
                {
                    d = (Direction)rand.Next(0, 2);
                    pos = new Position(rand.Next(0, _gridSize), rand.Next(0, _gridSize));
                    int stopper = 0;
                    try
                    {
                        ship.Place(pos, d);
                        foreach (var spot in ship.Positions)
                        {
                            stopper = 0;
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
                                brainGrid._grid[spot.X, spot.Y] = BrainGrid.Thought.Me;
                            }
                            stopper++;
                        }
                    }
                    catch (Exception)
                    {

                        ship.Reset();// makes ship.Positions = null
                        for (int i = 0; i < stopper; i++)
                        {
                            Position spot = ship.Positions[i];
                            brainGrid._grid[spot.X, spot.Y] = BrainGrid.Thought.Empty;
                        }
                    }

                }
            }




        }

        public string Name { get; }
        public int Index => _index;

        public Position GetAttackPosition() // find a hit tile and fire adjacent
        {
            Random rand = new Random();
            Position pos = null;
            while (pos == null)
            {
                int brainX = rand.Next(0, _gridSize);
                int brainY = rand.Next(0, _gridSize);
                if (brainGrid._grid[brainX, brainY] == BrainGrid.Thought.Hit)
                {
                    int d = rand.Next(0, 4);
                    switch (d)
                    {
                        case 1:
                            if (brainX - 1 < 0)
                            {
                                goto case 2;
                            }
                            pos = new Position(brainX - 1, brainY);
                            break;
                        case 2:
                            if (brainY + 1 > _gridSize - 1)
                            {
                                goto case 3;
                            }
                            pos = new Position(brainX, brainY + 1);
                            break;
                        case 3:
                            if (brainX + 1 > _gridSize - 1)
                            {
                                goto case 0;
                            }
                            pos = new Position(brainX + 1, brainY);
                            break;
                        case 0:
                            if (brainY - 1 < 0)
                            {
                                goto case 1;
                            }
                            pos = new Position(brainX, brainY - 1);
                            break;
                        default:
                            pos = new Position(0, 0);
                            break;
                    }
                }
                else
                {
                    bool ValidBoard = false;
                    for (int x = 0; x < _gridSize; x++)
                    {
                        for (int y = 0; y < _gridSize; y++)
                        {
                            if (brainGrid._grid[x, y] == BrainGrid.Thought.Hit) // ensure there is a hti tile to fire on
                            {
                                ValidBoard = true;
                            }
                        }
                    }
                    if (!ValidBoard)
                    {
                        var guess = Guesses[Random.Next(Guesses.Count)]; // fire randomly if there are no hit tiles
                        Guesses.Remove(guess); //Don't use this one again
                        return guess;
                    }
                }


            }
            return pos;
        }

        public void SetAttackResults(List<AttackResult> results) // write the attack results to the BRAIN GRID
        {
            foreach (var item in results)
            {
                BrainGrid.Thought type = BrainGrid.Thought.Empty;

                if (item.ResultType == AttackResultType.Hit)
                {
                    type = BrainGrid.Thought.Hit;
                }
                else if (item.ResultType == AttackResultType.Miss)
                {
                    type = BrainGrid.Thought.Miss;
                }
                else if (item.ResultType == AttackResultType.Sank)
                {
                    type = BrainGrid.Thought.Sank;
                }

                brainGrid._grid[item.Position.X, item.Position.Y] = type;
            }
        }

    }

}
