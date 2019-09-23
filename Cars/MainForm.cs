using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ShadowEngine;
using Tao.OpenGl;
using ShadowEngine.OpenGL;
using ShadowEngine.ContentLoading;

namespace CarRace
{
    public partial class MainForm : Form
    {
        uint hdc;
        int selectedCamara = 1;  //set camera angle
        int count, bustCount = 0;
        Controller control = new Controller();
        int mostrado = 0;
        int moving;
        //initialize total amount of punters
        int joe_total = 50, bob_total = 50, ai_total = 50;
        //by default no one wins 
        bool joe_won = false, bob_won = false, ai_won = false; //initially no one wins 
        int[] carno = new int[3]; //betno, the number of punter
        int[] betValue = new int[3]; //betvalue, the amount placed as bet by punter
        public MainForm()
        {
            InitializeComponent();
            hdc = (uint)this.Handle;
            string error = "";
            OpenGLControl.OpenGLInit(ref hdc, this.Width, this.Height, ref error);

            control.Camara.SetPerspective();
            if (error != "")
            {
                MessageBox.Show("Error Occurred"); //show error if something goes wrong
                this.Close();
            }


            //float[] lightAmbient = { 0.15F, 0.15F, 0.15F, 0.0F };

            //Lighting.LightAmbient = lightAmbient; 

            Lighting.SetupLighting();

            ContentManager.SetTextureList("texturas\\");
            ContentManager.LoadTextures();
            ContentManager.SetModelList("modelos\\");
            ContentManager.LoadModels();
            control.CreateObjects();

            //Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);   
        }
        /// <summary>
        /// initialize values for unit test cases for project testmybet 
        /// </summary>
        /// <param name="joewon"></param>
        /// <param name="bobwon"></param>
        /// <param name="aiwon"></param>
        /// <param name="totalMoney"></param>
        public MainForm(bool joewon, bool bobwon, bool aiwon, int totalMoney)
        {
            joe_won = joewon;
            bob_won = bobwon;
            ai_won = aiwon;
            joe_total = totalMoney;
            bob_total = totalMoney + 10;
            ai_total = totalMoney + 20;

        }
        //update the road, who won , money won and lost 
        public void UpdateLogic()
        {
            if (moving == 1)
            {
                Gl.glTranslatef(0, 0, 0.35f);
            }
            else
                if (moving == -1)
            {
                Gl.glTranslatef(0, 0, -0.35f);
            }
            count++;
            if (Controller.FinishedRace == true && mostrado == 0)
            {
                int result;
                RacePunter obj = new RacePunter();
                mostrado = 1;
                moving = 0;
                MessageBox.Show("The winner was the: " + lblPrimero.Text + " (car no: " + lblbettingno.Text + ")"); //display the actual result of race
                if (lblPrimero.Text == "Blue car")  //call overriden abstract metod to get and set winning better name
                {
                    result = obj.getWinning(carno, 1); //1 for blue car
                }
                else if (lblPrimero.Text == "Red car")
                {
                    result = obj.getWinning(carno, 2); //2 for red car
                }
                else if (lblPrimero.Text == "Green car")
                {
                    result = obj.getWinning(carno, 3); //3 for green car
                }
                else
                {
                    result = obj.getWinning(carno, 4); //else 4 for violet car
                }
                //based on carnumber which won set opposite punter winning status to true
                if (result == 0)
                {
                    joe_won = true;
                }
                else if (result == 1)
                {
                    bob_won = true;
                }
                else if (result == 2)
                {
                    ai_won = true;
                }
                //change the textboxes to who won/lost and money won/lost and left
                changeText();
                for (int i = 0; i < 3; i++)
                {
                    carno[i] = 0;
                    betValue[i] = 0;
                }

            }

            if (count == 10)
            {
                if (Controller.StartedRace == true && mostrado == 0)
                {
                    int primero = control.GetFirstPlace();
                    float distanciaRecorrida = control.GetDistanceInMeters(primero);
                    lblDistancia.Text = Convert.ToString((int)distanciaRecorrida);
                    switch (primero)
                    {
                        //ui changes based on which car is first
                        case 0:
                            {
                                lblPrimero.Text = "Blue car";
                                lblbettingno.Text = "1";
                                lblPrimero.ForeColor = Color.Blue;
                                break;
                            }
                        case 1:
                            {
                                lblPrimero.Text = "Red car";
                                lblbettingno.Text = "2";
                                lblPrimero.ForeColor = Color.Red;
                                break;
                            }
                        case 2:
                            {
                                lblPrimero.Text = "Green car";
                                lblbettingno.Text = "3";
                                lblPrimero.ForeColor = Color.Green;
                                break;
                            }
                        case 3:
                            {
                                lblPrimero.Text = "Violet car";
                                lblbettingno.Text = "4";
                                lblPrimero.ForeColor = Color.Violet;
                                break;
                            }
                    }
                }
                count = 0;
            }
        }


        private void tmrPaint_Tick(object sender, EventArgs e)
        {
            UpdateLogic();
            // clean opengl to draw
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            //draws the entire scene
            control.DrawScene();
            //change buffers
            Winapi.SwapBuffers(hdc);
            //tell opengl to drop any operation he is doing and to prepare for a new frame
            Gl.glFlush();
        }
        //initialize default constraints and values
        private void MainForm_Load(object sender, EventArgs e)
        {
            numericCar.Minimum = 1;
            numericCar.Maximum = 4;
            maxlbl.ForeColor = Color.Yellow;
            lblWhoBets.ForeColor = Color.Yellow;
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedCamara--;
            if (selectedCamara == 0)
            {
                selectedCamara = 4;
            }
            //lblCamara.Text = Convert.ToString(selectedCamara);
            control.Camara.SelectCamara(selectedCamara - 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedCamara++;
            if (selectedCamara == 5)
            {
                selectedCamara = 1;
            }
            //lblCamara.Text = Convert.ToString(selectedCamara);
            control.Camara.SelectCamara(selectedCamara - 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Controller.StartedRace = true;
        }

        //reset race
        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            lblPrimero.Text = "None";
            lblDistancia.Text = "0";
            control.ResetRace();

            mostrado = 0;
            count = 0;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            Gl.glViewport(0, 0, this.Width, this.Height);
            //select the projection matrix
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            //la reseteo
            Gl.glLoadIdentity();
            //45 = angulo de vision
            //1  = proporcion de alto por ancho
            //0.1f = distancia minima en la que se pinta
            //1000 = distancia maxima
            Glu.gluPerspective(55, this.Width / (float)this.Height, 0.1f, 1000);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            control.Camara.SelectCamara(selectedCamara - 1);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.W)
            {
                moving = 1;
            }
            if (e.KeyData == Keys.S)
            {
                moving = -1;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            moving = 0;
        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        //change configurations of maximum and bet and better name based on better selected from radio button list

        //joe selected from better
        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            maxlbl.Text = Convert.ToString(joe_total);
            numericBet.Maximum = joe_total;
            lblWhoBets.Text = "Joe ";
        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        //bob selected from better
        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            maxlbl.Text = Convert.ToString(bob_total);
            numericBet.Maximum = bob_total;
            lblWhoBets.Text = "Bob ";
        }


        //ai selected from better
        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            maxlbl.Text = Convert.ToString(ai_total);
            numericBet.Maximum = ai_total;
            lblWhoBets.Text = "AI ";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericBet_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        //bet button click event to change text of who bets how much on what car 
        private void Button4_Click(object sender, EventArgs e)
        {
            if (lblWhoBets.Text == "Joe ")
            {
                txtJoe.Text = lblWhoBets.Text + " bets " + numericBet.Value + " on car " + numericCar.Value;
                carno[0] = Convert.ToInt32(numericCar.Value);
                betValue[0] = Convert.ToInt32(numericBet.Value);
            }
            else if (lblWhoBets.Text == "Bob ")
            {
                txtBob.Text = lblWhoBets.Text + " bets " + numericBet.Value + " on car " + numericCar.Value;
                carno[1] = Convert.ToInt32(numericCar.Value);
                betValue[1] = Convert.ToInt32(numericBet.Value);
            }
            else
            {
                txtAI.Text = lblWhoBets.Text + " bets " + numericBet.Value + " on car " + numericCar.Value;
                carno[2] = Convert.ToInt32(numericCar.Value);
                betValue[2] = Convert.ToInt32(numericBet.Value);
            }
        }
        //change textboxes values for punters
        private void changeText()
        {
            RacePunter punter = new RacePunter(); //create punter object
            if (joe_won == true)
            {
                joe_total = Convert.ToInt32(joe_total + betValue[0]);
                txtJoe.Text = punter.getPunterName(0) + " won and now has " + joe_total;
                joe_won = false;
            }
            else
            {
                joe_total = Convert.ToInt32(joe_total - betValue[0]);
                if (joe_total <= 0)
                {
                    txtJoe.Text = "Busted";
                    txtJoe.ForeColor = Color.Red;
                    rbJoe.Enabled = false;
                    bustCount++;
                }
                else
                {
                    txtJoe.Text = punter.getPunterName(0) + " lost and now has " + joe_total;
                }
            }
            if (bob_won == true)
            {
                bob_total = Convert.ToInt32(bob_total + betValue[1]);
                txtBob.Text = punter.getPunterName(1) + " won and now has " + bob_total;
                bob_won = false;
            }
            else
            {
                bob_total = Convert.ToInt32(bob_total - betValue[1]);
                if (bob_total <= 0)
                {
                    txtBob.Text = "Busted";
                    rbBob.Enabled = false;
                    txtBob.ForeColor = Color.Red;
                    bustCount++;
                }
                else
                {
                    txtBob.Text = punter.getPunterName(1) + " lost and now has " + bob_total;
                }
            }
            if (ai_won == true)
            {
                ai_total = Convert.ToInt32(ai_total + betValue[2]);
                txtAI.Text = punter.getPunterName(2) + " won and now has " + ai_total;
                ai_won = false;
            }
            else
            {
                ai_total = Convert.ToInt32(ai_total - betValue[2]);
                if (ai_total <= 0)
                {
                    txtAI.Text = "Busted";
                    rbAI.Enabled = false;
                    txtAI.ForeColor = Color.Red;
                    bustCount++;
                }
                else
                {
                    txtAI.Text = punter.getPunterName(2) + " lost and now has " + ai_total;
                }
            }
            if (bustCount > 1)
            {
                MessageBox.Show("Game Over");
            }

        }
        //unit test function (called to test unit test cases from testmybet project)
        public string unitTest()
        {
            string result = "";
            if (joe_won == true)
            {
                result += "joe won " + (2 * joe_total) + Environment.NewLine;
            }
            else
            {
                result += "Busted" + Environment.NewLine;
            }
            if (bob_won == true)
            {
                result += "bob won " + (2 * bob_total) + Environment.NewLine;
            }
            else
            {
                result += "Busted" + Environment.NewLine;
            }
            if (ai_won == true)
            {
                result += "ai won " + (2 * ai_total) + Environment.NewLine;
            }
            else
            {
                result += "Busted" + Environment.NewLine;
            }
            return result;
        }
    }
}

