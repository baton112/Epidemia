using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epidemia
{
    enum state { zdrowy, chory, wyzdrowial, martwy };
    struct vector {public int X; public int Y;};
 
    public class Osobnik
    {
        state condition;
        vector position;

        public Osobnik() { }
        public Osobnik(int x, int y)
        {
            this.condition = state.zdrowy;
            this.position.X = x;
            this.position.Y = y;
        }
        
        public Boolean isSick()
        {
            if (condition == state.chory)
            {
                return true;
            }
            else return false;
        }
        public Boolean canGetSic()
        {
            if (condition == state.zdrowy)
            {
                return true;
            }
            else return false;
        }


    }
}
