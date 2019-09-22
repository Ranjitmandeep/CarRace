

using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl; 

namespace CarRace
{
    public class Camara
    {
        public void SelectCamara(int camara)
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            switch (camara)
            {
                case 0:
                    {
                        Glu.gluLookAt(2, 3, 14, 2, 0, 5, 0, 1, 0);
                        break;
                    }
                case 1:
                    {
                        Glu.gluLookAt(-2, 7, 10, 1, 3, 1, 0, 1, 0);
                        break;
                    }
                case 2:
                    {
                        Glu.gluLookAt(0, 35, -15, 1, 0, -16, 0, 1, 0);
                        break;
                    }
                case 3:
                    {
                        Glu.gluLookAt(3, 3, -47, 1, 0, 1, 0, 1, 0);
                        break;
                    }
            }
        }

        public void SetPerspective()
        {
            //select the projection matrix
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            //reset it
            Gl.glLoadIdentity();
            //55 = vision angle
            //1  = aspect ratio
            //0.1f = minimum draw distance
            //1000 = maximum draw distance
            Glu.gluPerspective(55, 1, 0.1f, 1000);
            SelectCamara(1);
        }
    }
}
