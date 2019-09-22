
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRace
{
    public struct Position
    {
        public float x;
        public float y;
        public Position(float x, float y)
        {
            this.x = x;
            this.y = y; 
        }
    }

    public class Controller
    {
        Car[] cars = new Car[4];
        World world = new World(); 
        Camara camara = new Camara();
        Road road = new Road();
        static bool startedRace;
        static int winner = -1;

        public static int Winner
        {
            get { return Controller.winner; }
            set { Controller.winner = value; }
        }

        static bool finishedRace;

        public static bool FinishedRace
        {
            get { return Controller.finishedRace; }
            set { Controller.finishedRace = value; }
        }

        public static bool StartedRace
        {
            get { return Controller.startedRace; }
            set { Controller.startedRace = value; }
        }

        public Controller()
        {
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i] = new Car();   
            } 
        }

        public Camara Camara
        {
            get { return camara; }
        }

        public int GetFirstPlace()
        {
            float menor = cars[0].TraveledDistance;
            int lugar = 0;
            for (int i = 1; i < cars.Length; i++)
            {
                if (menor > cars[i].TraveledDistance)
                {
                    lugar = i;
                    menor = cars[i].TraveledDistance;   
                }
            }
            return lugar; 
        }

        public int GetSecondPlace()
        {
            int primero = GetFirstPlace();

            switch (primero)
            {
                case 0:
                    {
                        if (cars[1].TraveledDistance > cars[2].TraveledDistance)
                        {
                            if (cars[1].TraveledDistance > cars[3].TraveledDistance)
                                return 1;
                            else
                                return 3;
                        }
                        else
                        {
                            if (cars[2].TraveledDistance > cars[3].TraveledDistance)
                                return 2;
                            else
                                return 3;
                        }

                    }
                case 1:
                    {
                        if (cars[0].TraveledDistance > cars[2].TraveledDistance)
                        {
                            if (cars[0].TraveledDistance > cars[3].TraveledDistance)
                                return 0;
                            else
                                return 3;
                        }
                        else
                        {
                            if (cars[2].TraveledDistance > cars[3].TraveledDistance)
                                return 2;
                            else
                                return 3;
                        }
                    }
                case 2:
                    {
                        if (cars[0].TraveledDistance > cars[1].TraveledDistance)
                        {
                            if (cars[0].TraveledDistance > cars[3].TraveledDistance)
                                return 0;
                            else
                                return 3;
                        }
                        else
                        {
                            if (cars[1].TraveledDistance > cars[3].TraveledDistance)
                                return 1;
                            else
                                return 3;
                        }
                    }
                case 3:
                    {
                        if (cars[0].TraveledDistance > cars[1].TraveledDistance)
                        {
                            if (cars[0].TraveledDistance > cars[2].TraveledDistance)
                                return 0;
                            else
                                return 2;
                        }
                        else
                        {
                            if (cars[1].TraveledDistance > cars[2].TraveledDistance)
                                return 1;
                            else
                                return 2;
                        }
                    }

                default:
                    break;

            }
            return -1;
        }

        public int GetThirdPlace()
        {
            for (int i = 0; i < cars.Length; i++)
            {
                if (i != GetFirstPlace() || i != GetSecondPlace())
                {
                    return i;
                }
            }
            return -1;
        }

        public float GetDistanceInMeters(int carro)
        {
            return cars[carro].TraveledDistance * -10; 
        }

        public void ResetRace()
        {
            startedRace = false;
            finishedRace = false; 
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i].ResetRace();
                
            }
        }

        public void CreateObjects()
        {
            
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i].Create();  
            }
            road.Create();  
        }

        public void DrawScene()
        {
            //draw the world (sky  & terrain)
            world.Draw();
            //draw the road
            road.Draw();
  
            //draw cars

            for (int i = 0; i < cars.Length; i++)
            {
                cars[i].Draw();
            }
        }
    }
}
