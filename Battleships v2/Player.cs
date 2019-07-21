using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships_v2
{
    class Player
    {
        public int Max_Health = 500;
        public int Health = 500;
        

        public SortedDictionary<string, Turret> weapons = new SortedDictionary<string, Turret> { };

        public Shield PlayerShield = new Shield(500);

        public bool building = false;
        public int buildTimer = 0;

        public Player ()
        {
            CreateTurret(30, 50, 30);
            CreateTurret(40, 70, 15);
        }

        int tempTurretMin = 0;
        int tempTurretMax = 0;
        int tempTurretClip = 0;

        public void BuildTurret (int min, int max, int clip)
        {
            if(weapons.Count < 6)
            {
                building = true;
                buildTimer = 30;
                tempTurretMin = min;
                tempTurretMax = max;
                tempTurretClip = clip;
            }
        }

        public void Building ()
        {
            if(building == true)
            {
                if(buildTimer > 0)
                {
                    buildTimer--;
                }
                else
                {
                    building = false;
                    buildTimer = 0;
                    CreateTurret(tempTurretMin, tempTurretMax,tempTurretClip);
                    tempTurretMin = 0;
                    tempTurretMax = 0;
                    tempTurretClip = 0;
                }
            }
        } 

        public void CreateTurret (int min, int max, int clip)
        {
            List<string> Designations = new List<string> { "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "FOXTROT", "GOLF", "HOTEL", "INDIA", "JULIETT", "KILO", "LIMA", "MIKE", "NOVEMBER", "OSCAR", "PAPA", "QUEBEC", "ROMEO", "SIERRA", "TANGO", "UNIFORM", "VICTOR", "WHISKEY", "XRAY", "YANKEE", "ZULU" };
            Turret turret = new Turret(Designations[weapons.Count], min, max, clip);
            weapons.Add(Designations[weapons.Count], turret);
        }

        public void SetTrack (string Key, int Track)
        {
            weapons[Key].TurretTracking = Track;
        }

        public void Track ()
        {
            foreach(KeyValuePair<string, Turret> weapon in weapons)
            {
                if (weapon.Value.TurretBearing + weapon.Value.TurretTracking > 360)
                {
                    weapon.Value.TurretBearing = (weapon.Value.TurretBearing + weapon.Value.TurretTracking) - 360;
                }
                else
                {
                    weapon.Value.TurretBearing = weapon.Value.TurretBearing + weapon.Value.TurretTracking;
                }
            }
        }
    }
}
