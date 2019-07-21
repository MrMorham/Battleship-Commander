using System;
using System.Collections.Generic;

namespace Battleships_v2
{
    class Program
    {
        static void Main (string[] args)
        {
            //Title
            Console.Title = "BATTLESHIP COMMANDER";
            //Set Console Size for Game
            Console.SetWindowSize(150, 31);
            Console.SetBufferSize(150, 31);
            Console.CursorSize = 100;
            //Win Condition
            int Open = 1;
            int Game = 0;
            //Player Name
            string PlayerName = "DEFAULT";
            Player PlayerShip = new Player();
            //Allows creation and destruction of Enemies
            EnemyManager Manager = new EnemyManager();

            //To be returned to Interpretorder()
            

            while (Open == 1)
            {

                switch (Game)
                {
                    //TITLE SCREEN
                    case 0:
                        while (Game == 0)
                        {
                            Console.Clear();
                            Console.CursorVisible = true;

                            void Centre (int length)
                            {
                                Console.CursorLeft = (Console.WindowWidth / 2) - length;
                            }

                            string TitleLine1 = "##### ##### ##### ##### ##    ##### ##### ## ## ##### #####";
                            string TitleLine2 = "##  # ## ## ##### ##### ##    ##    ##    ## ##  ###  ##  #";
                            string TitleLine3 = "##### #####  ###   ###  ##    ##### ##### #####  ###  #####";
                            string TitleLine4 = "##  # ## ##  ###   ###  ##    ##       ## ## ##  ###  ##   ";
                            string TitleLine5 = "##### ## ##  ###   ###  ##### ##### ##### ## ## ##### ##   ";
                            string TitleLine6 = "### ### ## ## ## ## ### ##  # ##  ### ###";
                            string TitleLine7 = "#   # # # # # # # # # # # # # # # ##  ## ";
                            string TitleLine8 = "### ### #   # #   # # # #  ## ##  ### # #";
                            string TitleLine9 = "====== NIGHT OF THE CELESTE ======";
                            string TitleLine10 = "<< PLEASE ENTER YOUR NAME COMMANDER >>";
                            //string TitleLine11 = "COMMANDER ";

                            Centre(TitleLine1.Length / 2);
                            Console.WriteLine(TitleLine1); Centre(TitleLine2.Length / 2);
                            Console.WriteLine(TitleLine2); Centre(TitleLine3.Length / 2);
                            Console.WriteLine(TitleLine3); Centre(TitleLine4.Length / 2);
                            Console.WriteLine(TitleLine4); Centre(TitleLine5.Length / 2);
                            Console.WriteLine(TitleLine5 + "\n"); Centre(TitleLine6.Length / 2);
                            Console.WriteLine(TitleLine6); Centre(TitleLine7.Length / 2);
                            Console.WriteLine(TitleLine7); Centre(TitleLine8.Length / 2);
                            Console.WriteLine(TitleLine8 + "\n"); Centre(TitleLine9.Length / 2);
                            Console.WriteLine(TitleLine9 + "\n"); Centre(TitleLine9.Length / 2);
                            Console.WriteLine(TitleLine10);
                            Centre(1);
                            //Centre((TitleLine11.Length / 2));
                            //Console.Write(TitleLine11);
                            PlayerName = Console.ReadLine();
                            PlayerName = PlayerName.ToUpper();
                            Game = 1;
                        }
                        break;

                    //MAIN GAME LOOP
                    case 1:

                        for (int i = 0; i < 10; i++)
                        {
                            Manager.NewEnemy();
                        }

                        while (Game == 1)
                        {
                            Manager.CollectDead();
                            PlayerShip.PlayerShield.RechargeShield();
                            PlayerShip.Building();

                            Console.Clear();
                            Console.WriteLine("{0,0} {1,-10}{2,-10}  {3,-10}\t{4,-10}{5,-10}{6,-10}", "       ", "SIGN", "CLASS", "STATUS", "BEARING", "DISTANCE", "VELOCITY");

                            RadarCallOut();
                            ShipReadOut();

                            Console.SetCursorPosition(0, 27);
                            Console.WriteLine(new string('-', Console.BufferWidth));
                            Console.Write($"YOUR ORDERS COMMANDER {PlayerName} >> ");
                            string inputAlpha = Console.ReadLine();
                            //Console.Beep();

                            InterpretOrder(inputAlpha.ToUpper());

                            PlayerShip.PlayerShield.ReduceShield(20);

                            Manager.DestroyDead();
                            PlayerShip.Track();
                            Manager.Move();
                            Manager.NewEnemy();
                        }
                        break;
                }
            }

            void RadarCallOut()
            {
                foreach (KeyValuePair<string, Enemy> instance in Manager.Enemies)
                {
                    Enemy Temp = instance.Value;
                    switch (Temp.Status)
                    {
                        case "DESTROYED":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("CONTACT {0,-10}{1,-10}  {2,-10}\t{3,-10}{4,-10}{5,-10}", instance.Key, Temp.Classification, Temp.Status, Temp.Bearing + " deg", Temp.Distance + " km", Temp.Velocity + " km/min");
                            break;
                        case "CRITICAL":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("CONTACT {0,-10}{1,-10}  {2,-10}\t{3,-10}{4,-10}{5,-10}", instance.Key, Temp.Classification, Temp.Status, Temp.Bearing + " deg", Temp.Distance + " km", Temp.Velocity + " km/min");
                            break;
                        case "DAMAGED":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("CONTACT {0,-10}{1,-10}  {2,-10}\t{3,-10}{4,-10}{5,-10}", instance.Key, Temp.Classification, Temp.Status, Temp.Bearing + " deg", Temp.Distance + " km", Temp.Velocity + " km/min");
                            break;
                        case "OPERATIONAL":
                            Console.ResetColor();
                            Console.WriteLine("CONTACT {0,-10}{1,-10}  {2,-10}\t{3,-10}{4,-10}{5,-10}", instance.Key, Temp.Classification, Temp.Status, Temp.Bearing + " deg", Temp.Distance + " km", Temp.Velocity + " km/min"); 
                            break;
                    }
                    Console.ResetColor();
                }
            }

            void ShipReadOut ()
            {
                string HealthTicks = new String('=', PlayerShip.Health/25);
                string HealthBuffer = new String(' ', (PlayerShip.Max_Health - PlayerShip.Health) / 25);
                string HealthBar = "[" + HealthTicks + HealthBuffer + $"] {(PlayerShip.Health/PlayerShip.Max_Health)*100}%";
                string ShieldTicks = new string('=', PlayerShip.PlayerShield.CurShield / 25);
                string ShieldBuffer = new string(' ', (PlayerShip.PlayerShield.MaxShield - PlayerShip.PlayerShield.CurShield) / 25);
                string ShieldBar = "[" + ShieldTicks + ShieldBuffer + $"] {(PlayerShip.PlayerShield.MaxShield/PlayerShip.PlayerShield.CurShield)*100}%";
                Console.SetCursorPosition(100, 0);
                Console.Write("SHIP REPORT");
                Console.SetCursorPosition(100,1);
                if(PlayerShip.Health <= PlayerShip.Max_Health && PlayerShip.Health >= PlayerShip.Max_Health*0.75)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(HealthBar);
                    Console.ResetColor();
                } else if (PlayerShip.Health < PlayerShip.Max_Health*0.75 && PlayerShip.Health >= PlayerShip.Max_Health*0.5)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(HealthBar);
                    Console.ResetColor();
                } else if (PlayerShip.Health < PlayerShip.Max_Health*0.5 && PlayerShip.Health >= PlayerShip.Max_Health*0.25)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(HealthBar);
                    Console.ResetColor();
                } else if (PlayerShip.Health < PlayerShip.Max_Health*0.25 && PlayerShip.Health >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(HealthBar);
                    Console.ResetColor();
                }
                Console.SetCursorPosition(100, 2);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(ShieldBar);
                Console.ResetColor();

                int weaponCount = 1;
                foreach(KeyValuePair<string, Turret> item in PlayerShip.weapons)
                {
                    Console.SetCursorPosition(100, weaponCount + 3);
                    Console.Write($"{PlayerShip.weapons[item.Key].Name} TURRET: BR {PlayerShip.weapons[item.Key].TurretBearing}° TR {PlayerShip.weapons[item.Key].TurretTracking}° AM [{PlayerShip.weapons[item.Key].AmmoCount}/{PlayerShip.weapons[item.Key].MaxAmmoCount}] <{PlayerShip.weapons[item.Key].LastDamageDone}>");
                    weaponCount++;
                }
                weaponCount = 1;

                Console.SetCursorPosition(100, 26);
                if (PlayerShip.building == true)
                {
                    Console.Write($"Construction Crews [Active] ETA {PlayerShip.buildTimer}");
                }
                else
                {
                    Console.Write("Construction Crews [Inactive] Awaiting Orders");
                }

            }

            
            void InterpretOrder (string order)
            {
                Array ToInterpret = order.Split(' ');
                List<string> Turrets = new List<string> { };
                string action = "";
                int number = 0;
                int velocity = 0;
                string type = "";

                foreach (string item in ToInterpret)
                {
                    if (PlayerShip.weapons.ContainsKey(item))
                    {
                        Turrets.Add(item);
                    }
                }

                foreach (string item in ToInterpret)
                {
                    switch (item)
                    {
                        case "BEARING":
                            action = item;
                            break;
                        case "TRACK":
                            action = item;
                            break;
                        case "FIRE":
                            action = item;
                            break;
                        case "RELOAD":
                            action = item;
                            break;
                        case "BUILD":
                            action = item;
                            break;
                        case "HELP":
                            action = item;
                            break;
                        default:
                            break;
                    }
                }

                foreach (string item in ToInterpret)
                {
                    if (Int32.TryParse(item, out int num) == true)
                    {
                        number = num;
                    }
                }

                foreach(string item in ToInterpret)
                {
                    switch (item)
                    {
                        case "HEAVY":
                            type = item;
                            break;
                        case "MEDIUM":
                            type = item;
                            break;
                        case "LIGHT":
                            type = item;
                            break;
                        default:
                            break;

                    }
                }

                if(Turrets.Count > 0)
                {
                    foreach (string turret in Turrets)
                    {
                        switch (action)
                        {
                            case "BEARING":
                                PlayerShip.weapons[turret].TurretBearing = number;
                                break;
                            case "TRACK":
                                PlayerShip.SetTrack(turret, number);
                                break;
                            case "FIRE":
                                foreach (KeyValuePair<string, Enemy> instance in Manager.Enemies)
                                {
                                    if (instance.Value.Bearing == PlayerShip.weapons[turret].TurretBearing && PlayerShip.weapons[turret].AmmoCount > 0)
                                    {
                                        instance.Value.ChangeHealth(PlayerShip.weapons[turret].TurretDamage());
                                    }
                                }
                                PlayerShip.weapons[turret].Shoot();
                                break;
                            case "RELOAD":
                                PlayerShip.weapons[turret].Reload();
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    switch (action)
                    {
                        case "FIRE":
                            foreach(KeyValuePair<string, Turret> turret in PlayerShip.weapons)
                            {
                                foreach (KeyValuePair<string, Enemy> instance in Manager.Enemies)
                                {
                                    if (instance.Value.Bearing == turret.Value.TurretBearing && turret.Value.AmmoCount > 0)
                                    {
                                        instance.Value.ChangeHealth(turret.Value.TurretDamage());
                                    }
                                }
                                turret.Value.Shoot();
                            }
                            break;
                        case "RELOAD":
                            foreach(KeyValuePair<string, Turret> turret in PlayerShip.weapons)
                            {
                                turret.Value.Reload();
                            }
                            break;
                        case "BUILD":
                            switch (type)
                            {
                                case "HEAVY":
                                    PlayerShip.BuildTurret(50, 100, 10);
                                    break;
                                case "MEDIUM":
                                    PlayerShip.BuildTurret(40, 70, 15);
                                    break;
                                case "LIGHT":
                                    PlayerShip.BuildTurret(30, 50, 30);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
                                
                Turrets.Clear();
                action = "";
                number = 0;
                type = "";
            }
        }
    }
}