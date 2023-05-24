using dasp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class GameForm : Form
    {
        ClientForm parentClientForm;

        // this matrix holds the amount of stones in each pocket, including the score pockets
        // first row, elements 1 to 6 are the 6 pockets for player 1, the last element stores the amount of stones in the score pocket
        // second row, same thing but for player 2
        private int[,] gameStateMatrix = new int[2, 7];

        private bool myTurn;
        private bool gameInProgress;
        private int playerNumber;

        private int infoLabelUpdatedIndex = 0;

        private const string GAME_IN_PROGRESS = "Game in progress...";
        private const string WAITING_FOR_GAME_TO_START = "Waiting for game to start...";
        private const string GAME_WON = "GAME WON!";
        private const string GAME_LOST = "GAME LOST...";
        private const string GAME_DRAW = "THe game ended in a draw...";
        private const string NOT_YOUR_TURN = "Not your turn...";

        public GameForm(ClientForm parentClientForm)
        {
            InitializeComponent();

            gameStatusLabel.Text = ".....";
            this.parentClientForm = parentClientForm;
            gameInProgress = false;
            myTurn = false;

            updateInfoLabels();
        }

        public void initMancalaStonesMatrixWithDefaultGameState()
        {
            // initialize the matrix with the default game state
            gameStateMatrix = new int[2, 7] {
                { 4, 4, 4, 4, 4, 4 ,0},
                { 4, 4, 4, 4, 4, 4 ,0}
            };
        }

        public void updatePocketNumbers()
        {
            player1Pocket1.Text = gameStateMatrix[0,0].ToString();
            player1Pocket2.Text = gameStateMatrix[0, 1].ToString();
            player1Pocket3.Text = gameStateMatrix[0, 2].ToString();
            player1Pocket4.Text = gameStateMatrix[0, 3].ToString();
            player1Pocket5.Text = gameStateMatrix[0, 4].ToString();
            player1Pocket6.Text = gameStateMatrix[0, 5].ToString();
            player1ScorePocket.Text = gameStateMatrix[0, 6].ToString();

            player2Pocket1.Text = gameStateMatrix[1, 0].ToString();
            player2Pocket2.Text = gameStateMatrix[1, 1].ToString();
            player2Pocket3.Text = gameStateMatrix[1, 2].ToString();
            player2Pocket4.Text = gameStateMatrix[1, 3].ToString();
            player2Pocket5.Text = gameStateMatrix[1, 4].ToString();
            player2Pocket6.Text = gameStateMatrix[1, 5].ToString();
            player2ScorePocket.Text = gameStateMatrix[1, 6].ToString();
        }

        public void SendPressedPocket(int pressedPocketIndex)
        {
            // if its currently our turn we send the index
            if (myTurn)
            {
                parentClientForm.SendPressedPocket(pressedPocketIndex);
            }
            else
            {
                InformPlayer(NOT_YOUR_TURN);
            }
            
        }

        public void ReceiveGameState(int[,] gameStateMatrix, bool playerTurnInformation, int gameEndStatus)
        {
            // gameEndStatus = -1 for a game in progress
            // gameEndStatus = 1 for a game that ended

            if (gameEndStatus == -1)
            {
                this.gameInProgress = true;
            }
            else
            {
                this.gameInProgress = false;
            }
            this.gameStateMatrix = gameStateMatrix;
            this.myTurn = playerTurnInformation;

            if (gameEndStatus != -1)
            {
                GameFinished(gameEndStatus);
            }

            updateInfoLabels();
            updatePocketNumbers();
            checkForGameEnded();
        }

        private void checkForGameEnded()
        {
            if (
                (gameStateMatrix[0, 0] == 0 &&
                gameStateMatrix[0, 1] == 0 &&
                gameStateMatrix[0, 2] == 0 &&
                gameStateMatrix[0, 3] == 0 &&
                gameStateMatrix[0, 4] == 0 &&
                gameStateMatrix[0, 5] == 0) ||
                (gameStateMatrix[1, 0] == 0 &&
                gameStateMatrix[1, 1] == 0 &&
                gameStateMatrix[1, 2] == 0 &&
                gameStateMatrix[1, 3] == 0 &&
                gameStateMatrix[1, 4] == 0 &&
                gameStateMatrix[1, 5] == 0))
            {
                // if im player 1 and my score is higher
                if (playerNumber == 1 && (gameStateMatrix[0,6] > gameStateMatrix[1,6]))
                {
                    gameStatusLabel.Text = GAME_WON;
                }
                // else if im player 1 and my score is lower
                else if (playerNumber == 1 && (gameStateMatrix[0, 6] < gameStateMatrix[1, 6]))
                {
                    gameStatusLabel.Text = GAME_LOST;
                }
                // ekse if im player 1 and my score is even
                else if (playerNumber == 1 && (gameStateMatrix[0, 6] == gameStateMatrix[1, 6]))
                {
                    gameStatusLabel.Text = GAME_DRAW;
                }

                // if im player 2 and my score is higher
                if (playerNumber == 2 && (gameStateMatrix[1, 6] > gameStateMatrix[0, 6]))
                {
                    gameStatusLabel.Text = GAME_WON;
                }
                // else if im player 2 and my score is lower
                else if (playerNumber == 2 && (gameStateMatrix[1, 6] < gameStateMatrix[0, 6]))
                {
                    gameStatusLabel.Text = GAME_LOST;
                }
                // ekse if im player 2 and my score is even
                else if (playerNumber == 2 && (gameStateMatrix[1, 6] == gameStateMatrix[0, 6]))
                {
                    gameStatusLabel.Text = GAME_DRAW;
                }
                gameInProgress = false;
                updateInfoLabels();
            }
        }

        public void InformPlayer(string value)
        {
            if (gameInProgress)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(InformPlayer), new object[] { value });
                    return;
                }
                gameStatusLabel.Text = value;

                if (!value.Equals("..."))
                {
                    System.Timers.Timer timer =
                                            new System.Timers.Timer(1000) { Enabled = true };
                    timer.Elapsed += (sender, args) =>
                    {
                        this.InformPlayer("...");
                        timer.Dispose();
                    };
                }
            }
        }

        private void GameFinished(int status)
        {
            gameInProgress = false;
            if (status == 1)
            {
                gameStatusLabel.Text = GAME_WON;
            }
            else if (status == 0) 
            {
                gameStatusLabel.Text = GAME_LOST;
            }
        }


        public void GameStarted()
        {
            if (parentClientForm.startGameButton.Visible == true)
            {
                // this client is player 1
                playerNumber = 1;
            }
            else
            {
                // this client is player 2 (or spectator.. too bad!)
                playerNumber = 2;
            }
            infoLabel3.Text = "Client is player " + playerNumber;
            gameInProgress = true;
        }

        public void updateInfoLabels()
        {
            infoLabel1.Text = "Game in progress: " + gameInProgress;
            infoLabel2.Text = "My Turn: " + myTurn;

            infoLabel4.Text = "Updated Index: " + infoLabelUpdatedIndex;
            infoLabelUpdatedIndex++;
        }

        private void player1Pocket1_Click(object sender, EventArgs e)
        {
            if (playerNumber == 1)
                SendPressedPocket(0);
        }

        private void player1Pocket2_Click(object sender, EventArgs e)
        {
            if (playerNumber == 1)
                SendPressedPocket(1);
        }

        private void player1Pocket3_Click(object sender, EventArgs e)
        {
            if (playerNumber == 1)
                SendPressedPocket(2);
        }

        private void player1Pocket4_Click(object sender, EventArgs e)
        {
            if (playerNumber == 1)
                SendPressedPocket(3);
        }

        private void player1Pocket5_Click(object sender, EventArgs e)
        {
            if (playerNumber == 1)
                SendPressedPocket(4);
        }

        private void player1Pocket6_Click(object sender, EventArgs e)
        {
            if (playerNumber == 1)
                SendPressedPocket(5);
        }

        private void player2Pocket1_Click(object sender, EventArgs e)
        {
            if (playerNumber == 2)
                SendPressedPocket(0);
        }

        private void player2Pocket2_Click(object sender, EventArgs e)
        {
            if (playerNumber == 2)
                SendPressedPocket(1);
        }

        private void player2Pocket3_Click(object sender, EventArgs e)
        {
            if (playerNumber == 2)
                SendPressedPocket(2);
        }

        private void player2Pocket4_Click(object sender, EventArgs e)
        {
            if (playerNumber == 2)
                SendPressedPocket(3);
        }

        private void player2Pocket5_Click(object sender, EventArgs e)
        {
            if (playerNumber == 2)
                SendPressedPocket(4);
        }

        private void player2Pocket6_Click(object sender, EventArgs e)
        {
            if (playerNumber == 2)
                SendPressedPocket(5);
        }
    }
}
