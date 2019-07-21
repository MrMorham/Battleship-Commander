using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships_v2
{
    class Turret
    {
        public string Name;
        public int TurretBearing = 0;
        public int TurretTracking = 0;
        public int MinTurretDamage;
        public int MaxTurretDamage;
        public int LastDamageDone = 0;
        public int MaxAmmoCount;
        public int AmmoCount;
        
        public Random Rand = new Random();

        public Turret (string name, int MinDamage, int MaxDamage, int MaxAmmo)
        {
            this.Name = name;
            this.MinTurretDamage = MinDamage;
            this.MaxTurretDamage = MaxDamage;
            this.MaxAmmoCount = MaxAmmo;
            this.AmmoCount = MaxAmmo;
        }
        
        public int TurretDamage ()
        {
            int damage = Rand.Next(MinTurretDamage, MaxTurretDamage+1);
            LastDamageDone = damage;
            return damage;
        }

        public void Shoot ()
        {
            if(AmmoCount > 0)
            {
                AmmoCount = AmmoCount - 1;
            }
                        
        }

        public void Reload ()
        {
            AmmoCount = MaxAmmoCount;
        }
        

    } 
}
