using System;
using System.Collections.Generic;
using System.Text;

namespace CarRace
{
    class RacePunter : PunterMain
    {

        /// <summary>
        /// overrides getwinning method to get winning status i.e. who won
        /// </summary>
        /// <param name="carNoBetted"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public override int getWinning(int[] carNoBetted, int no)
        {
            int result = -1;
            if (carNoBetted[0] == no)
                result = 0;
            if (carNoBetted[1] == no)
                result = 1;
            if (carNoBetted[2] == no)
                result = 2;
            return result;
        }
        /// <summary>
        /// overriding method to set better name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bettersTotalAmount"></param>
        public override void setPunterName(string name, int bettersTotalAmount)
        {
            betterName = name;
            better_total_amount = bettersTotalAmount;
        }
        /// <summary>
        /// override method to get punter name
        /// </summary>
        /// <param name="punterNumber"></param>
        /// <returns></returns>
        public override string getPunterName(int punterNumber)
        {
            string betterName = "";
            if (punterNumber == 0)
            {
                betterName = "Joe";
            }
            else if (punterNumber == 1)
            {
                betterName = "Bob";
            }
            else
            {
                betterName = "AI";
            }
            return betterName;
        }
        /// <summary>
        /// method to get total punter money
        /// </summary>
        /// <returns></returns>
        public int getTotalAmount()
        {
            return better_total_amount;
        }
    }
}
