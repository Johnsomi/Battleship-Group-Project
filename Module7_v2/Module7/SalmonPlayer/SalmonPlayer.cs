using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module8
{
    class SalmonPlayer : IPlayer
    {
        public string Name => throw new NotImplementedException();

        public int Index => throw new NotImplementedException();

        public Position GetAttackPosition()
        {
            throw new NotImplementedException();
        }

        public void SetAttackResults(List<AttackResult> results)
        {
            throw new NotImplementedException();
        }

        public void StartNewGame(int playerIndex, int gridSize, Ships ships)
        {
            throw new NotImplementedException();
        }
    }
}
