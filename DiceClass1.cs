using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._1
{
    class Dice
    {
        public int amount;
        public int sides;
        public Dice(int amount, int sides) 
        {
            this.amount = amount;
            this.sides = sides;
        }
        public int Roll()
        {
            Random random = new Random();
            int result = 0;
            for (int i = 0; i < amount; i++)
                result += random.Next(1, sides + 1);
            return result;
        }
    }
}
  

