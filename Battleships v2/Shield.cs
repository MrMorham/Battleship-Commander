using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships_v2
{
    class Shield
    {
        public int MaxShield;
        public int CurShield;

        public Shield(int points)
        {
            this.MaxShield = points;
            this.CurShield = MaxShield;
        }

        public void ReduceShield(int amount)
        {
            if(CurShield - amount <= 0)
            {
                CurShield = 1;
            } else
            {
                CurShield = CurShield - amount;
            }
        }

        public void RechargeShield ()
        {
            if (CurShield > 0)
            {
                CurShield = CurShield + 10;
            }
            
            if (CurShield > MaxShield)
            {
                CurShield = CurShield - (CurShield - MaxShield);
            }
        }
    }
}
