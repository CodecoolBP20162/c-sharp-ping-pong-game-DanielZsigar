using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPongGame
{
    public partial class Form1 : Form
    {
        public Random random = new Random();
        public int ballSpeed = 4;
        public int direction = 4;
        public int score = 1;
        public int level = 1;

        public Form1()
        {
            InitializeComponent();

            Random random = new Random();
            timer1.Enabled = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            paddle.Top = playground.Bottom - (playground.Bottom / 10);
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
           

            ball.Left += ballSpeed;
            ball.Top += direction;

            //collison detection of the ball when it touches the paddle
            if (ball.Bottom >= paddle.Top && ball.Bottom <= paddle.Bottom && ball.Left >= paddle.Left &&ball.Right <= paddle.Right)
            {
                
                direction = -direction;
                Score.Text ="Score: "+ score++.ToString();
                progressBar1.Value += 10;
            }

            // changes the size of the paddle and level text if the progress bar filled 
            if (progressBar1.Value == 50)
            {
                progressBar1.Value = 0;
                paddle.Size = new Size(paddle.Size.Width -paddle.Size.Width/5, paddle.Size.Height);
                level++;
                label1.Text = "Level " + level;
            }

            //collision detection when the ball touches the edge of the field
            if (ball.Left <= playground.Left)
            {
                ballSpeed = -ballSpeed;
            }
            else if (ball.Top <= playground.Top+40)
            {
                direction = -direction;
            }
            else if (ball.Right >= playground.Right)
            {
                ballSpeed = -ballSpeed;
            }
            else if(ball.Bottom >= playground.Bottom)
            {
                timer1.Enabled = false;
            }

            // collision detection when the paddle touches the right or left edge of the field
            if(paddle.Left <= playground.Left)
            {
                paddle.Location = new Point(paddle.Location.X + 6,paddle.Location.Y);
            }
            if (paddle.Right >= playground.Right)
            {
                paddle.Location = new Point(paddle.Location.X - 6, paddle.Location.Y);
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // controll of the paddle with arrow keys(right,left) pressing
            if (e.KeyCode == Keys.Right)
            {
                paddle.Location = new Point(paddle.Location.X +100, paddle.Location.Y);
            }
            else if (e.KeyCode == Keys.Left)
            {
                paddle.Location = new Point(paddle.Location.X -100, paddle.Location.Y);
            }
            
            // escape afunction with key pressing
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            // pause afunction with key pressing
            else if (e.KeyCode == Keys.Space)
                if (timer1.Enabled.Equals(true))
                {
                    timer1.Enabled = false;
                }
                else 
                {
                    timer1.Enabled = true;
                }
            // resets the position of the ball and clear the statistics and randomly release the ball 
            else if(e.KeyCode == Keys.R)
            {
                ball.Top = 340;
                ball.Left = 700;
                score = 0;
                direction = 4;
                ballSpeed = random.Next(-4, 4);
                progressBar1.Value = 0;
                paddle.Width = 277;
                paddle.Height = 22;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Level "+level;
            ball.Location = new Point(playground.Location.X+600, paddle.Location.Y -600);
            progressBar1.Maximum = 50;
            
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {         
            // insrease the ball's speed if label text changes (level up)
            if(label1.Text != "Level 1") {

                if (direction.ToString().Contains("-")){
                    direction -= 2;
                    ballSpeed += 2;
                }
                else
                {
                    direction += 2;
                    ballSpeed += 2;
                }
            }
        }
    }
}
