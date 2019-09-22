using System;
using System.Collections.Generic;
using System.Text;

namespace CarRace
{
    abstract class PunterMain
    {
        public int better_total_amount; //punter total money
        public string betterName; //punter name
        public abstract int getWinning(int[] carnoBetted, int no); //get who won result
        public abstract void setPunterName(string name, int bettersTotalAmount); //set punter name using abstract method
        public abstract string getPunterName(int punterNumber); //get punter name using abstract method
    }
}
