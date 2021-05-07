// Multiplayer Battleship Game with AI - Partial Solution

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module8
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IPlayer> players = new List<IPlayer>();
            players.Add(new DumbPlayer("Dumb 1"));
            players.Add(new SalmonPlayer("Salmon 2"));

            //Your code here
            //players.Add(new GroupNPlayer());

            MultiPlayerBattleShip game = new MultiPlayerBattleShip(players);
            game.Play(PlayMode.Pause);
        }
    }
}
