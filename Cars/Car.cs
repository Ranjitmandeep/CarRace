

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ShadowEngine;
using Tao.OpenGl; 
using System.Windows.Forms;  
using ShadowEngine.ContentLoading; 

namespace CarRace
{
    class Car
    {
        float tireAngle;
        static int carColor = 1;
        Color color;
        Position pos;
        float speed;
        float traveledDistance = 0;
        static Random randomizer = new Random(); // to randomize the speed value
        int counter;
        ModelContainer m;
        
        List<Mesh> tires = new List<Mesh>();
        Mesh body; // this is the chassis of the car
        
        int texture;


        public float TraveledDistance
        {
            get { return traveledDistance; }
        }

        public Car()
        {
            if (carColor == 1)
            {
                color = Color.Blue;
                pos = new Position(-0.4f, 10);
            }
            if (carColor == 2)
            {
                color = Color.Red;
                pos = new Position(0.9f, 10);
            }
            if (carColor == 3)
            {
                color = Color.Green;
                pos = new Position(2.1f, 10);
            }
            if (carColor == 4)
            {
                color = Color.Violet;
                pos = new Position(3.4f, 10);
            }
            carColor++;
            speed = 0.2f + (float)randomizer.NextDouble() / 18f;
        }

        public void ResetRace()
        {
            traveledDistance = 0;
        }

        public void Create()
        {
            m = ContentManager.GetModelByName("carro.3DS");
            m.CreateDisplayList(); //optimice the model and load it in opengl display lists
            
            m.ScaleX = 0.1f;
            m.ScaleY = 0.1f;
            m.ScaleZ = 0.1f;

            foreach (var item in m.GetMeshes)
            {
                item.CalcCenterPoint();// calculate th epivot point
                switch (item.Name)
                {
                    case "tireA":
                    case "tireB":
                    case "tireC":
                    case "tire":
                    case "rimA":
                    case "rimB":
                    case "rimC":
                    case "rim":
                        tires.Add(item);
                        break; 
                    case "body":
                         body = item;
                         break;
                } 
            }

            if (color == Color.Blue)
            {
                texture = ContentManager.GetTextureByName("bodyBlue.jpg");   
            }
            if (color == Color.Red)
            {
                texture = ContentManager.GetTextureByName("bodyRed.jpg");
            }
            if (color == Color.Green)
            {
                texture = ContentManager.GetTextureByName("bodyGreen.jpg");
            }
            if (color == Color.Violet)
            {
                texture = ContentManager.GetTextureByName("bodyViolet.jpg");
                m.RemoveMeshesWithName("body");
                m.RemoveMeshesWithName("tire");
                m.RemoveMeshesWithName("rim");

            }
        }

        public void Draw()
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(pos.x, 0, pos.y);

            #region race logic

            if (Controller.StartedRace == true)
            {
                //move the object the traveled distance
                Gl.glTranslatef(0, 0, traveledDistance); 
                if (traveledDistance > -59)
                {
                    tireAngle -= 24;
                    traveledDistance -= speed;
                }
                else
                    if (Controller.FinishedRace == false)
                    {
                        Controller.FinishedRace = true;
                    }
                counter++;
                // if counter == 30 i change the speed
                if (counter == 30)
                {
                    counter = 0;
                    speed = 0.2f + (float)randomizer.NextDouble() / 20f;
                }
            }

            #endregion

            m.DrawWithTextures(); // draw the car accesories

            #region draw chasis

            Gl.glPushMatrix();
            Gl.glScalef(0.1f, 0.1f, 0.1f);

            Gl.glEnable(Gl.GL_TEXTURE_2D);// enable textures
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
            body.Draw();

            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix(); 

            #endregion

            #region draw rolling tires

            foreach (var item in tires)
            {
                Gl.glColor3f(0.5f, 0.5f, 0.5f);//especifico el color de la rueda
                Gl.glPushMatrix();
                Gl.glScalef(0.1f, 0.1f, 0.1f);
                Gl.glTranslatef(item.CenterPoint.X, item.CenterPoint.Y, item.CenterPoint.Z); //traslada a la posicion original 
                Gl.glRotatef(tireAngle, 1, 0, 0);// se rota 
                Gl.glTranslatef(-item.CenterPoint.X, -item.CenterPoint.Y, -item.CenterPoint.Z); // traslado al centro
                item.Draw();  // dibujo la rueda 
                Gl.glPopMatrix();
                Gl.glColor3f(1, 1, 1); 
            }

            #endregion

            Gl.glPopMatrix();
        }
    }
}
