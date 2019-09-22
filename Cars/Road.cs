

using System.Collections.Generic;
using System.Text;
using ShadowEngine;
using Tao.OpenGl;  

namespace CarRace
{
    class Road
    {
        int initList;

        public void Create()
        { 
            initList = Gl.glGenLists(1);
            Gl.glNewList(initList, Gl.GL_COMPILE);

            int texturaDelimitador = ContentManager.GetTextureByName("delimitador.jpg");

            //start line
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texturaDelimitador);
            Gl.glPushMatrix();
            Gl.glTranslatef(0, 0, 8);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(-0.5f, 0.1f, 0);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(-0.5f, 0.1f, 1);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(4f, 0.1f, 1);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(4f, 0.1f, 0);
            Gl.glEnd();
            Gl.glPopMatrix();

            //end line
            Gl.glPushMatrix();
            Gl.glTranslatef(0, 0, -50);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(-0.5f, 0.05f, 0);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(-0.5f, 0.05f, 1);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(3.5f, 0.05f, 1);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(3.5f, 0.05f, 0);
            Gl.glEnd();
            Gl.glPopMatrix();


            int texturaAsfalto = ContentManager.GetTextureByName("asfalto.jpg");
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texturaAsfalto);
            Gl.glPushMatrix();
            Gl.glTranslatef(0, 0, -100);

            int count = 0;
            for (int y = 0; y < 40; y++)// this for loop draws the road
            {
                Gl.glBegin(Gl.GL_QUADS);
                Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(-0.8f, 0, count);
                Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(-0.8f, 0, count + 10);
                Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(3.8f, 0, count + 10);
                Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(3.8f, 0, count);
                Gl.glEnd();
                count += 10; 
            }

            Gl.glPopMatrix();

            Gl.glEndList(); 
        }

        public void Draw()
        {
            Gl.glCallList(initList);  
        }
    }
}
