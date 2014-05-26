using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epidemia
{
    public class populacja
    {
        private List<Osobnik> curretPopulation;
        public int alive;

        public populacja(int size)
        {
            curretPopulation = new List<Osobnik>();
            this.alive = size;
            for (int i = 0; i < alive; i++)
            {
                curretPopulation.Add(new Osobnik(0, 0));
            }
        }

    }
}
