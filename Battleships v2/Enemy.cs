namespace Battleships_v2
{
    public class Enemy
    {
        public int Health {get; set;}
        public int Velocity {get; set;}
        public int Bearing {get; set;}
        public int Distance { get; set; }
        public int Orbit_Distance { get; set; }
        public string Classification = "FRIGATE";
        public string Status = "OPERATIONAL";
        
        public Enemy(int health,int velocity,int bearing, int distance)
        {
            this.Health = health;
            this.Velocity = velocity;
            this.Bearing = bearing;
            this.Distance = distance;
        }
            
        public void ChangeHealth(int amount)
        {
            Health = Health + -amount;

            if (Health < 100 && Health >= 25)
            {
                Status = "DAMAGED";
            }
            if (Health < 25 && Health > 0)
            {
                Status = "CRITICAL";
            }
            if (Health <= 0)
            {
                Status = "DESTROYED";
            }
        }

        public void Classify(int health)
        {
            if(Health > 0 && Health <= 50)
            {
                Classification = "FRIGATE";
                Orbit_Distance = 0;
            }
            if(Health > 50 && Health <= 100)
            {
                Classification = "DESTROYER";
                Orbit_Distance = 75;
            }
            if(Health > 100 && Health <= 150)
            {
                Classification = "CRUISER";
                Orbit_Distance = 100;
            }
            if(Health > 150 && Health <= 200)
            {
                Classification = "BATTLESHIP";
                Orbit_Distance = 200;
            }
        }

        public void Move_Approach ()
        {
            Distance = Distance - Velocity;
        }

        public void Move_Orbit ()
        {
            if(Bearing + Velocity > 360)
            {
                Bearing = (Bearing + Velocity) - 360;
            }
            else
            {
                Bearing = Bearing + Velocity;
            }
        }
        
    }
}