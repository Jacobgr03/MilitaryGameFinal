using MilitaryGame.Properties;
using System.CodeDom;
using System.Configuration;

namespace MilitaryGame
{
    
    public partial class Form1 : Form
    {
        // Global Variables which are used throughout the program in different functions and need to be transfered between functions. // 
        int Rounds;
        int diffBox;
        int colScore;
        int Shade;
        int Game;
        int DangerCount;
        int DangerScore;
        int position;
        int percentage;
        int difficulty;
        PictureBox[] Boxes = new PictureBox[9];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Rounds = 5;
            // on load set the position to 10 since this position does not exist on the page and shouldn't interfere with anything //
            position = 10;
            // Hide Start button
            StartButton.Visible= false;
            PassClose.Visible = false;
            // on load hide the reaction test Componenets so the colour blindness test must be passed first //
            Start.Visible = false;
            DangerDisplay.Visible = false;
            TimeBar.Visible = false;
            TimeBar.Panel1.BackColor = Color.White;
            // fill the Boxes array with the names of the boxes that change colour or background image //
            Boxes[0] = TopLeft;
            Boxes[1] = TopMiddle;
            Boxes[2] = TopRight;
            Boxes[3] = MidLeft;
            Boxes[4] = MidMiddle;
            Boxes[5] = MidRight;
            Boxes[6] = BotLeft;
            Boxes[7] = BotMiddle;
            Boxes[8] = BotRight;
            // Set the startup text for each text box //
            ColText.Text = "Colour Test Score = 0";
            DangerDisplay.Text = "Reactions Test Score = 0";
            Footer.Text = "When you are ready begin the Colour Blindness test by pressing the colour blind test button";
        }
 
        private void TestFail()
        {
            if (Rounds <= 0) 
            {
                PassFailBox.Text = "\r\nUnfortunately you have failed the tests too many times.\r\n\r\nTherefore you will not be able to continue with the military application process.\r\n\r\nThankyou\r\n\r\nYou may now close the window \r\n";
                PassFailBox.Visible = true;
            }
        }
        private void TestPass()
        {
            if (Game == 2 & DangerScore >= 1500)
            {
                Rounds = 100000;
                PassFailBox.Visible = true;
                PassClose.Visible = true;
            }
        }
        // color game function //
        private void ColorGame()
        {
            // set the game number so that functions can change for game 1 (colour) and game 2 ( Reactions ) //
            Game = 1;
            // set the 3 colour variables between 0 and 255 this make it so that each colour on the page is randomly made and won't be the same //
            Random rand = new Random();
            int Color1 = rand.Next(0, 255);
            int Color2 = rand.Next(0, 255);
            int Color3 = rand.Next(0, 255);
            // randomly decide which box will be a lighter shade //
            diffBox = rand.Next(0, Boxes.Length);
            // loop through the Boxes array setting the colour of the boxes //
            foreach (PictureBox Box in Boxes)
                {
                    Box.Image = null;
                    Box.BackColor = Color.FromArgb(255, Color1, Color2, Color3);
                }
            // make the 1 box that is a different colour //
            Boxes[diffBox].BackColor = Color.FromArgb(Shade, Color1, Color2, Color3);
        }
        // on completion of the colour game //
        private void ColGameOver()
        {
            // remove the game selection //
            Game = 0;
            // set the different box value to a number outside the array limit to stop interferance //
            diffBox = 10;
            // loop throught the array removing background image and setting the background to white
            foreach (PictureBox Box in Boxes)
            {
                Box.Image = null;
                Box.BackColor = Color.FromArgb(255, 255, 255, 255);
            }
            if (colScore >= 15)
            {
                // if the score was larger then 15 then allow access to the reactions test Game //
                Start.Visible = true;
                DangerDisplay.Visible = true;
                TimeBar.Visible = true;
                Footer.Text = "Congratulations, now try the reactions test, by pressing the Reactions Test Button";
            }
            else
            {
                Footer.Text = " Target score not reached, please try again";
                Rounds = Rounds - 1;
                TestFail();
            }
        }

        private void ReactionGame()
        {
            // game = 2 to ensure the second game is played //
            Game = 2;
            // reset the danger count to 0 //
            DangerCount = 0;
            // percentage set to maximum to fill up the timer bar at the bottom of the page //
            percentage = 800;
            // the 10 mS timer to tick down the timer bar at the bottom of the page //
            timer10.Enabled = true;
            // Pick image is randomly generated so the picture box will display the danger or safe image //
            int PickImage;
            Random rand = new Random();
            // loop through the boxes array assigning the image depending on what the random number generates // 
            foreach (PictureBox Box in Boxes)
            {
                // sets pick image between  and 10 //
                PickImage = rand.Next(11);
                if (PickImage < 5)
                {
                    Box.BackColor = Color.Black;
                    Box.Image = Resources.Dangerous_Man1;
                    DangerCount = DangerCount + 1;
                }
                else
                {
                    Box.Image = Resources.Man;
                    Box.BackColor = Color.White;
                }
            }
            // if not danger is counter then run again //
            if (DangerCount == 0)
            {
                ReactionGame();
            }
            // speed up the countdown depending on score, difficulty is the speed that the timer bar goes down //
            switch (DangerScore)
            {
                case < 200:
                    difficulty = 1;
                    Footer.Text = "Level 1";
                    break;
                case >= 200 and < 500:
                    difficulty = 2;
                    Footer.Text = "Level 2";
                    break;
                case >= 500 and < 1000:
                    difficulty = 3;
                    Footer.Text = "Level 3";
                    break;
                case >= 1000 and < 1500:
                    difficulty = 4;
                    Footer.Text = "Level 4";
                    break;
                case >= 1500 and < 2500:
                    difficulty = 5;
                    Footer.Text = "Level 5";
                    break;
                case >= 2500 and < 4500:
                    difficulty = 6;
                    Footer.Text = "Level 6";
                break;
                case >= 4500:
                    difficulty = 7;
                    Footer.Text = "Final Level";
                    break;
            }
        }

        // when the reaction game is over //
        private void ReactionGameOver()
        {
            // turn the blue part of the timer bar white //
            TimeBar.Panel1.BackColor = Color.White;
            // stop the countdown timer //
            timer10.Enabled = false;
            // remove the  image and back colour of the display boxes
            foreach (PictureBox Box in Boxes)
            {
                Box.Image = null;
                Box.BackColor = Color.FromArgb(255, 255, 255, 255);
            }
            // minimum of 1500 points score to pass the test //
            if (DangerScore >= 1500)
            {
                Footer.Text = " Congratulations, You have passed the test";
                PassFailBox.Text = "\r\nCongratulations, You have Passed both tests\r\n\r\nPlease press close to continue if you want to try beating your score \r\n\r\n or Close the window if you wish to quit.";
                TestPass();
            }
            else
            {
                Footer.Text = "Target score not reached. Try Again by pressing the Danger Reaction Test Button";
                Rounds = Rounds - 1;
                TestFail();
            }
        }
        // load the explanation of each game depending on what the game is //
        private void Explanation()
        {
            if (Game == 1)
            {
                ColBlindExplain.Visible = true;
                StartButton.Visible = true;

            }
            if (Game == 2)
            {
                ReactionsExplain.Visible = true;
                StartButton.Visible = true;
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
       private void Start_Click(object sender, EventArgs e)
        {
            // assign the reactions game and load the explanation
            Game = 2;
            DangerScore = 0;
            Explanation();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Footer.Text = "Get a score of 15 or more to progress to the next test";
            // on click of the colour blindness game set the score to 0 and the shade of the different box to 50, initialise the colorgame function //
            colScore = 0;
            Shade = 50;
            // assign the reactions game and load the explanation
            Game = 1;
            Explanation();
        }
        private void TopLeft_Click(object sender, EventArgs e)
        {
            position = 0;
            // if the colour game is active //
            if (Game == 1)
            {
                // if the correct box has been chosen //
                if (diffBox == position)
                {
                    // increase score and shade if not at maximum, play the game again with the difficulty now increased with the shade //
                    colScore = colScore + 1;
                    ColText.Text = "Colour Test Score = " + colScore;
                    if (Shade < 245)
                    {
                        Shade = Shade + 10;
                    }
                    ColorGame();
                }
                // if the wrong box was selected finish the game //
                else
                {
                    ColGameOver();
                }
            }
            // if the reaction game is active //
            if (Game == 2)
            {
                // if displaying the dangerous image,  had to change the back colour too so that i could do this comparison //
                if (Boxes[position].BackColor == Color.Black)
                {
                    // lower the danger count //
                    DangerCount--;
                    // change the image to safe and back colour to white so that it can be compared //
                    Boxes[position].Image = Resources.Man;
                    Boxes[position].BackColor = Color.White;
                    // increase danger count by 5 for clearing a danger //
                    DangerScore = DangerScore + 5;
                    if (DangerCount == 0)
                    {
                        // inrease danger score by 50 if the board have been cleared, also restart the reaction game //
                        DangerScore = DangerScore + 50;
                        ReactionGame();
                    }
                }
                // remove 10 points if a safe box is clicked //
                else
                {
                    DangerScore = DangerScore - 10;
                }
            }
        }
        private void TopMiddle_Click(object sender, EventArgs e)
        {
            position = 1;
            // if the colour game is active //
            if (Game == 1)
            {
                // if the correct box has been chosen //
                if (diffBox == position)
                {
                    // increase score and shade if not at maximum, play the game again with the difficulty now increased with the shade //
                    colScore = colScore + 1;
                    ColText.Text = "Colour Test Score = " + colScore;
                    if (Shade < 245)
                    {
                        Shade = Shade + 10;
                    }
                    ColorGame();
                }
                // if the wrong box was selected finish the game //
                else
                {
                    ColGameOver();
                }
            }
            // if the reaction game is active //
            if (Game == 2)
            {
                // if displaying the dangerous image,  had to change the back colour too so that i could do this comparison //
                if (Boxes[position].BackColor == Color.Black)
                {
                    // lower the danger count //
                    DangerCount--;
                    // change the image to safe and back colour to white so that it can be compared //
                    Boxes[position].Image = Resources.Man;
                    Boxes[position].BackColor = Color.White;
                    // increase danger count by 5 for clearing a danger //
                    DangerScore = DangerScore + 5;
                    if (DangerCount == 0)
                    {
                        // inrease danger score by 50 if the board have been cleared, also restart the reaction game //
                        DangerScore = DangerScore + 50;
                        ReactionGame();
                    }
                }
                // remove 10 points if a safe box is clicked //
                else
                {
                    DangerScore = DangerScore - 10;
                }
            }
        }

        private void TopRight_Click(object sender, EventArgs e)
        {
            position = 2;
            // if the colour game is active //
            if (Game == 1)
            {
                // if the correct box has been chosen //
                if (diffBox == position)
                {
                    // increase score and shade if not at maximum, play the game again with the difficulty now increased with the shade //
                    colScore = colScore + 1;
                    ColText.Text = "Colour Test Score = " + colScore;
                    if (Shade < 245)
                    {
                        Shade = Shade + 10;
                    }
                    ColorGame();
                }
                // if the wrong box was selected finish the game //
                else
                {
                    ColGameOver();
                }
            }
            // if the reaction game is active //
            if (Game == 2)
            {
                // if displaying the dangerous image,  had to change the back colour too so that i could do this comparison //
                if (Boxes[position].BackColor == Color.Black)
                {
                    // lower the danger count //
                    DangerCount--;
                    // change the image to safe and back colour to white so that it can be compared //
                    Boxes[position].Image = Resources.Man;
                    Boxes[position].BackColor = Color.White;
                    // increase danger count by 5 for clearing a danger //
                    DangerScore = DangerScore + 5;
                    if (DangerCount == 0)
                    {
                        // inrease danger score by 50 if the board have been cleared, also restart the reaction game //
                        DangerScore = DangerScore + 50;
                        ReactionGame();
                    }
                }
                // remove 10 points if a safe box is clicked //
                else
                {
                    DangerScore = DangerScore - 10;
                }
            }
        }

        private void MidLeft_Click(object sender, EventArgs e)
        {
            position = 3;
            // if the colour game is active //
            if (Game == 1)
            {
                // if the correct box has been chosen //
                if (diffBox == position)
                {
                    // increase score and shade if not at maximum, play the game again with the difficulty now increased with the shade //
                    colScore = colScore + 1;
                    ColText.Text = "Colour Test Score = " + colScore;
                    if (Shade < 245)
                    {
                        Shade = Shade + 10;
                    }
                    ColorGame();
                }
                // if the wrong box was selected finish the game //
                else
                {
                    ColGameOver();
                }
            }
            // if the reaction game is active //
            if (Game == 2)
            {
                // if displaying the dangerous image,  had to change the back colour too so that i could do this comparison //
                if (Boxes[position].BackColor == Color.Black)
                {
                    // lower the danger count //
                    DangerCount--;
                    // change the image to safe and back colour to white so that it can be compared //
                    Boxes[position].Image = Resources.Man;
                    Boxes[position].BackColor = Color.White;
                    // increase danger count by 5 for clearing a danger //
                    DangerScore = DangerScore + 5;
                    if (DangerCount == 0)
                    {
                        // inrease danger score by 50 if the board have been cleared, also restart the reaction game //
                        DangerScore = DangerScore + 50;
                        ReactionGame();
                    }
                }
                // remove 10 points if a safe box is clicked //
                else
                {
                    DangerScore = DangerScore - 10;
                }
            }
        }

        private void MidMiddle_Click(object sender, EventArgs e)
        {
            position = 4;
            // if the colour game is active //
            if (Game == 1)
            {
                // if the correct box has been chosen //
                if (diffBox == position)
                {
                    // increase score and shade if not at maximum, play the game again with the difficulty now increased with the shade //
                    colScore = colScore + 1;
                    ColText.Text = "Colour Test Score = " + colScore;
                    if (Shade < 245)
                    {
                        Shade = Shade + 10;
                    }
                    ColorGame();
                }
                // if the wrong box was selected finish the game //
                else
                {
                    ColGameOver();
                }
            }
            // if the reaction game is active //
            if (Game == 2)
            {
                // if displaying the dangerous image,  had to change the back colour too so that i could do this comparison //
                if (Boxes[position].BackColor == Color.Black)
                {
                    // lower the danger count //
                    DangerCount--;
                    // change the image to safe and back colour to white so that it can be compared //
                    Boxes[position].Image = Resources.Man;
                    Boxes[position].BackColor = Color.White;
                    // increase danger count by 5 for clearing a danger //
                    DangerScore = DangerScore + 5;
                    if (DangerCount == 0)
                    {
                        // inrease danger score by 50 if the board have been cleared, also restart the reaction game //
                        DangerScore = DangerScore + 50;
                        ReactionGame();
                    }
                }
                // remove 10 points if a safe box is clicked //
                else
                {
                    DangerScore = DangerScore - 10;
                }
            }
        }

        private void MidRight_Click(object sender, EventArgs e)
        {
            position = 5;
            // if the colour game is active //
            if (Game == 1)
            {

                // if the correct box has been chosen //
                if (diffBox == position)
                {
                    // increase score and shade if not at maximum, play the game again with the difficulty now increased with the shade //
                    colScore = colScore + 1;
                    ColText.Text = "Colour Test Score = " + colScore;
                    if (Shade < 245)
                    {
                        Shade = Shade + 10;
                    }
                    ColorGame();
                }
                // if the wrong box was selected finish the game //
                else
                {
                    ColGameOver();
                }
            }
            // if the reaction game is active //
            if (Game == 2)
            {
                // if displaying the dangerous image,  had to change the back colour too so that i could do this comparison //
                if (Boxes[position].BackColor == Color.Black)
                {
                    // lower the danger count //
                    DangerCount--;
                    // change the image to safe and back colour to white so that it can be compared //
                    Boxes[position].Image = Resources.Man;
                    Boxes[position].BackColor = Color.White;
                    // increase danger count by 5 for clearing a danger //
                    DangerScore = DangerScore + 5;
                    if (DangerCount == 0)
                    {
                        // inrease danger score by 50 if the board have been cleared, also restart the reaction game //
                        DangerScore = DangerScore + 50;
                        ReactionGame();
                    }
                }
                // remove 10 points if a safe box is clicked //
                else
                {
                    DangerScore = DangerScore - 10;
                }
            }
        }

        private void BotLeft_Click(object sender, EventArgs e)
        {
            position = 6;
            // if the colour game is active //
            if (Game == 1)
            {
                // if the correct box has been chosen //
                if (diffBox == position)
                {
                    // increase score and shade if not at maximum, play the game again with the difficulty now increased with the shade //
                    colScore = colScore + 1;
                    ColText.Text = "Colour Test Score = " + colScore;
                    if (Shade < 245)
                    {
                        Shade = Shade + 10;
                    }
                    ColorGame();
                }
                // if the wrong box was selected finish the game //
                else
                {
                    ColGameOver();
                }
            }
            // if the reaction game is active //
            if (Game == 2)
            {
                // if displaying the dangerous image,  had to change the back colour too so that i could do this comparison //
                if (Boxes[position].BackColor == Color.Black)
                {
                    // lower the danger count //
                    DangerCount--;
                    // change the image to safe and back colour to white so that it can be compared //
                    Boxes[position].Image = Resources.Man;
                    Boxes[position].BackColor = Color.White;
                    // increase danger count by 5 for clearing a danger //
                    DangerScore = DangerScore + 5;
                    if (DangerCount == 0)
                    {
                        // inrease danger score by 50 if the board have been cleared, also restart the reaction game //
                        DangerScore = DangerScore + 50;
                        ReactionGame();
                    }
                }
                // remove 10 points if a safe box is clicked //
                else
                {
                    DangerScore = DangerScore - 10;
                }
            }
        }

        private void BotMiddle_Click(object sender, EventArgs e)
        {
            position = 7;
            // if the colour game is active //
            if (Game == 1)
            {
  
                // if the correct box has been chosen //
                if (diffBox == position)
                {
                    // increase score and shade if not at maximum, play the game again with the difficulty now increased with the shade //
                    colScore = colScore + 1; 
                    ColText.Text = "Colour Test Score = " + colScore;
                    if (Shade < 245)
                    {
                        Shade = Shade + 10;
                    }
                    ColorGame();
                }
                // if the wrong box was selected finish the game //
                else
                {
                    ColGameOver();
                }
            }
            // if the reaction game is active //
            if (Game == 2)
            {
                // if displaying the dangerous image,  had to change the back colour too so that i could do this comparison //
                if (Boxes[position].BackColor == Color.Black)
                {
                    // lower the danger count //
                    DangerCount--;
                    // change the image to safe and back colour to white so that it can be compared //
                    Boxes[position].Image = Resources.Man;
                    Boxes[position].BackColor = Color.White;
                    // increase danger count by 5 for clearing a danger //
                    DangerScore = DangerScore + 5;
                    if (DangerCount == 0)
                    {
                        // inrease danger score by 50 if the board have been cleared, also restart the reaction game //
                        DangerScore = DangerScore + 50;
                        ReactionGame();
                    }
                }
                // remove 10 points if a safe box is clicked //
                else
                {
                    DangerScore = DangerScore - 10;
                }
            }
        }

        private void BotRight_Click(object sender, EventArgs e)
        {
            position = 8;
            // if the colour game is active //
            if (Game == 1)
            {
                // if the correct box has been chosen //
                if (diffBox == position)
                {
                    // increase score and shade if not at maximum, play the game again with the difficulty now increased with the shade //
                    colScore = colScore + 1;
                    ColText.Text = "Colour Test Score = " + colScore;
                    if (Shade < 245)
                    {
                        Shade = Shade + 10;
                    }
                    ColorGame();
                }
                // if the wrong box was selected finish the game //
                else
                {
                    ColGameOver();
                }
            }
            // if the reaction game is active //
            if (Game == 2)
            {
                // if displaying the dangerous image,  had to change the back colour too so that i could do this comparison //
                if (Boxes[position].BackColor == Color.Black)
                {
                    // lower the danger count //
                    DangerCount--;
                    // change the image to safe and back colour to white so that it can be compared //
                    Boxes[position].Image = Resources.Man;
                    Boxes[position].BackColor = Color.White;
                    // increase danger count by 5 for clearing a danger //
                    DangerScore = DangerScore + 5;
                    if (DangerCount == 0)
                    {
                        // inrease danger score by 50 if the board have been cleared, also restart the reaction game //
                        DangerScore = DangerScore + 50;
                        ReactionGame();
                    }
                }
                // remove 10 points if a safe box is clicked //
                else
                {
                    DangerScore = DangerScore - 10;
                }
            }
        }


        // button so that i can skip the colour test for testing, means i don't have to do the colour test each time //
        private void Skip_Click(object sender, EventArgs e)
        {
            Start.Visible = true;
            DangerDisplay.Visible = true;
            TimeBar.Visible = true;
        }

        // whenever the 10 mS timer is complete //
        private void timer10_Tick(object sender, EventArgs e)
        {
            // update the score for the reactions test //
            DangerDisplay.Text = "Reactions Score = " + DangerScore.ToString();
            // turn the right bar panel to blue so it can be seen //
            TimeBar.Panel1.BackColor = Color.Blue;
            // change where the split is in the bar //
            TimeBar.SplitterDistance = percentage;
            // decrease the bar by the difficulty percentage //
            percentage = percentage - difficulty;
            // if the bar reaches 0 then end the reactions game //
            if (percentage < 0)
            {
                ReactionGameOver();          
            } 
        }
        
        // when the start button is clicked start whichever game is seleted and close the explanation screen //
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (Game == 1 )
            {
                ColBlindExplain.Visible = false;
                StartButton.Visible = false;
                ColorGame();
            }
            if (Game == 2)
            {
                ReactionsExplain.Visible = false;
                StartButton.Visible = false;
                ReactionGame();
            }
        }
        private void PassClose_Click(object sender, EventArgs e)
        {
            PassFailBox.Visible = false;
            PassClose.Visible = false;
        }
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DangerScore = 2000;
            ReactionGame();
        }
        private void ReactionsExplain_TextChanged(object sender, EventArgs e)
        {

        }
        private void FailureNotice_TextChanged(object sender, EventArgs e)
        {

        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void DangerDisplay_TextChanged(object sender, EventArgs e)
        {

        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        } 
        private void ReactTime_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}