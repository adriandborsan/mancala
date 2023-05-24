namespace dasp
{
    public class ChatRoom
    {
        public int ID { get; set; }
        public List<DaspConnection> daspConnections;
        private static int nextRoomId = 1;
        public int[,] GameStateMatrix { get; set; }
        public Func<ChatRoom, Task> gameStateUpdated;
        public bool MoveInProgress { get; set; }=false;

        private bool PlayerHasExtraTurn = false;

        Func<string, Task> _sendLog;
        public ChatRoom(List<DaspConnection> daspConnections, Func<ChatRoom, Task> gameStateUpdated, Func<string, Task> _sendLog)
        {
            GameStateMatrix = new int[2, 7] {
                { 4, 4, 4, 4, 4, 4 ,0},
                { 4, 4, 4, 4, 4, 4 ,0} };
            this.ID = nextRoomId++;
            this.daspConnections = daspConnections;
            this.gameStateUpdated = gameStateUpdated;
            this._sendLog = _sendLog;
        }

        public ChatRoomInfo ConvertToChatRoomInfo()
        {
            return new ChatRoomInfo(ID, daspConnections.Count);
        }

        internal void PocketMoved(int pocket)
        {
            MoveInProgress = true;

            int playerCurrentlyMoving = 0;
            int pocketCursor = pocket;

            DaspConnection playerThatIsMoving = daspConnections.FirstOrDefault(con => con.PlayerState == PlayerState.CURRENTLY_MOVING);
            DaspConnection playerThatIsWaitingToMove = daspConnections.FirstOrDefault(con => con.PlayerState == PlayerState.WAITING_TO_MOVE);
            if (playerThatIsMoving == daspConnections[0])
            {
                // player 1 is the one who is moving
                playerCurrentlyMoving = 0; // player 1
            }
            if (playerThatIsMoving == daspConnections[1])
            {
                // player 2 is the one who is moving
                playerCurrentlyMoving = 1; // player 2
            }

            // however many stones we're in the chosen pocket
            int stonesInHand = GameStateMatrix[playerCurrentlyMoving, pocket];
            _sendLog("took " + stonesInHand + " stones from pocket [" + playerCurrentlyMoving + "][" + pocket + "]");
            GameStateMatrix[playerCurrentlyMoving, pocket] = 0; // stones from that pocket we're removed

            if (playerCurrentlyMoving == 0)
            {
                _sendLog("player 1 is moving...");

                if (pocket == 0)
                {
                    addStoneToStore(0, stonesInHand);
                }
                else
                {
                    moveStones(playerCurrentlyMoving, 0, pocketCursor - 1, stonesInHand);
                }
                
            }
            else if (playerCurrentlyMoving == 1)
            {
                _sendLog("player 2 is moving...");

                if (pocket == 5)
                {
                    addStoneToStore(1, stonesInHand);
                }
                else
                {
                    moveStones(playerCurrentlyMoving, 1, pocketCursor + 1, stonesInHand);
                }
            }
            // if the player doesnt have an extra turn, then change the turn to the other player
            if (PlayerHasExtraTurn == false)
            {
                playerThatIsMoving.PlayerState = PlayerState.WAITING_TO_MOVE;
                playerThatIsWaitingToMove.PlayerState = PlayerState.CURRENTLY_MOVING;
            }
            MoveInProgress = false;
            PlayerHasExtraTurn = false;




            //  this retrieves the player that is currently moving
            // daspConnections.FirstOrDefault(con => con.PlayerState == PlayerState.CURRENTLY_MOVING);

            // if either side of the table is filled with 0 then the game is over
            if (
                (GameStateMatrix[0, 0] == 0 &&
                GameStateMatrix[0, 1] == 0 &&
                GameStateMatrix[0, 2] == 0 &&
                GameStateMatrix[0, 3] == 0 &&
                GameStateMatrix[0, 4] == 0 &&
                GameStateMatrix[0, 5] == 0) ||
                (GameStateMatrix[1, 0] == 0 &&
                GameStateMatrix[1, 1] == 0 &&
                GameStateMatrix[1, 2] == 0 &&
                GameStateMatrix[1, 3] == 0 &&
                GameStateMatrix[1, 4] == 0 &&
                GameStateMatrix[1, 5] == 0))
            {
                playerThatIsMoving.PlayerState = PlayerState.WAITING_TO_MOVE;
                playerThatIsWaitingToMove.PlayerState = PlayerState.WAITING_TO_MOVE;
            }

            gameStateUpdated(this);
        }

        internal void GameStarted(DaspConnection connection, DaspConnection targetConnection)
        {
            connection.PlayerState = PlayerState.CURRENTLY_MOVING;
            targetConnection.PlayerState = PlayerState.WAITING_TO_MOVE;
            gameStateUpdated(this);
        }

        private void moveStones(int playerTurn, int tableSide, int pocketCursor, int stones)
        {
            _sendLog("moveStones was called!!");
            // we are on player 1's side of the table
            if (tableSide == 0)
            {
                // we still have stones left to move
                if (stones > 0)
                {
                    GameStateMatrix[tableSide, pocketCursor]++;
                    _sendLog("added stone to pocket [" + tableSide + "][" + pocketCursor + "]");
                    if (pocketCursor == 0 && playerTurn == 0)
                    {
                        // cursor reached 0 while on player 1's turn so we have to place a stone in the store next
                        stones = stones - 1;

                        int reverseTableSide;
                        if (tableSide == 1) reverseTableSide = 0;
                        else reverseTableSide = 1;

                        if (stones == 0 && GameStateMatrix[tableSide, pocketCursor] == 1) // the stone that was last placed, was placed in a previously empty pocket
                        {
                            int totalStonesToAddToScore = 1; // initialized with 1 because we already know the stone we place is to be added to our score
                            totalStonesToAddToScore = totalStonesToAddToScore + GameStateMatrix[reverseTableSide, pocketCursor];
                            GameStateMatrix[reverseTableSide, pocketCursor] = 0;
                            GameStateMatrix[tableSide, pocketCursor] = 0;
                            GameStateMatrix[playerTurn, 6] = GameStateMatrix[playerTurn, 6] + totalStonesToAddToScore;
                        }
                        else addStoneToStore(playerTurn, stones);
                    }
                    else if (pocketCursor == 0 && playerTurn == 1)
                    {
                        stones = stones - 1;

                        int reverseTableSide;
                        if (tableSide == 1) reverseTableSide = 0;
                        else reverseTableSide = 1;

                        if (stones == 0 && GameStateMatrix[tableSide, pocketCursor] == 1) // the stone that was last placed, was placed in a previously empty pocket
                        {
                            int totalStonesToAddToScore = 1; // initialized with 1 because we already know the stone we place is to be added to our score
                            totalStonesToAddToScore = totalStonesToAddToScore + GameStateMatrix[reverseTableSide, pocketCursor];
                            GameStateMatrix[reverseTableSide, pocketCursor] = 0;
                            GameStateMatrix[tableSide, pocketCursor] = 0;
                            GameStateMatrix[playerTurn, 6] = GameStateMatrix[playerTurn, 6] + totalStonesToAddToScore;
                        }
                        else
                        {
                            moveStones(playerTurn, 1, pocketCursor, stones);
                        }

                    }
                    else
                    {
                        pocketCursor = pocketCursor - 1;
                        stones = stones - 1;

                        int tempPocketCursor = pocketCursor + 1;

                        int reverseTableSide;
                        if (tableSide == 1) reverseTableSide = 0;
                        else reverseTableSide = 1;

                        if (stones == 0 && GameStateMatrix[tableSide, tempPocketCursor] == 1) // the stone that was last placed, was placed in a previously empty pocket
                        {
                            int totalStonesToAddToScore = 1; // initialized with 1 because we already know the stone we place is to be added to our score
                            totalStonesToAddToScore = totalStonesToAddToScore + GameStateMatrix[reverseTableSide, tempPocketCursor];
                            GameStateMatrix[reverseTableSide, tempPocketCursor] = 0;
                            GameStateMatrix[tableSide, tempPocketCursor] = 0;
                            GameStateMatrix[playerTurn, 6] = GameStateMatrix[playerTurn, 6] + totalStonesToAddToScore;
                        }
                        else
                        {
                            moveStones(playerTurn, tableSide, pocketCursor, stones);
                        }
                    }
                }
                else
                {
                    _sendLog("got out of moveStones recursion for table side 0");
                }
            }
            // we are on player 2's side of the table
            else if (tableSide == 1)
            {
                if (stones > 0)
                {
                    GameStateMatrix[tableSide, pocketCursor]++;
                    _sendLog("added stone to pocket [" + tableSide + "][" + pocketCursor + "]");
                    if (pocketCursor == 5 && playerTurn == 0)
                    {
                        stones = stones - 1;

                        int reverseTableSide;
                        if (tableSide == 1) reverseTableSide = 0;
                        else reverseTableSide = 1;

                        if (stones == 0 && GameStateMatrix[tableSide, pocketCursor] == 1) // the stone that was last placed, was placed in a previously empty pocket
                        {
                            int totalStonesToAddToScore = 1; // initialized with 1 because we already know the stone we place is to be added to our score
                            totalStonesToAddToScore = totalStonesToAddToScore + GameStateMatrix[reverseTableSide, pocketCursor];
                            GameStateMatrix[reverseTableSide, pocketCursor] = 0;
                            GameStateMatrix[tableSide, pocketCursor] = 0;
                            GameStateMatrix[playerTurn, 6] = GameStateMatrix[playerTurn, 6] + totalStonesToAddToScore;
                        }
                        else
                        {
                            moveStones(playerTurn, 0, pocketCursor, stones);
                        }
                    }
                    else if (pocketCursor == 5 && playerTurn == 1)
                    {
                        stones = stones - 1;

                        int reverseTableSide;
                        if (tableSide == 1) reverseTableSide = 0;
                        else reverseTableSide = 1;

                        if (stones == 0 && GameStateMatrix[tableSide, pocketCursor] == 1) // the stone that was last placed, was placed in a previously empty pocket
                        {
                            int totalStonesToAddToScore = 1; // initialized with 1 because we already know the stone we place is to be added to our score
                            totalStonesToAddToScore = totalStonesToAddToScore + GameStateMatrix[reverseTableSide, pocketCursor];
                            GameStateMatrix[reverseTableSide, pocketCursor] = 0;
                            GameStateMatrix[tableSide, pocketCursor] = 0;
                            GameStateMatrix[playerTurn, 6] = GameStateMatrix[playerTurn, 6] + totalStonesToAddToScore;
                        }
                        else addStoneToStore(playerTurn, stones);
                    }
                    else
                    {
                        pocketCursor = pocketCursor + 1;
                        stones = stones - 1;

                        int tempPocketCursor = pocketCursor - 1;

                        int reverseTableSide;
                        if (tableSide == 1) reverseTableSide = 0;
                        else reverseTableSide = 1;

                        if (stones == 0 && GameStateMatrix[tableSide, tempPocketCursor] == 1) // the stone that was last placed, was placed in a previously empty pocket
                        {
                            int totalStonesToAddToScore = 1; // initialized with 1 because we already know the stone we place is to be added to our score
                            totalStonesToAddToScore = totalStonesToAddToScore + GameStateMatrix[reverseTableSide, tempPocketCursor];
                            GameStateMatrix[reverseTableSide, tempPocketCursor] = 0;
                            GameStateMatrix[tableSide, tempPocketCursor] = 0;
                            GameStateMatrix[playerTurn, 6] = GameStateMatrix[playerTurn, 6] + totalStonesToAddToScore;
                        }
                        else moveStones(playerTurn, tableSide, pocketCursor, stones);
                    }
                }
                else
                {
                    _sendLog("got out of moveStones recursion for table side 1");
                }
            }

        }

        private void addStoneToStore(int playerTurn, int stones)
        {
            // if we still have stones to
            if (stones > 0)
            {   
                //adding the stone
                GameStateMatrix[playerTurn, 6]++;
                _sendLog("added stone to store [" + playerTurn + "][" + 6 + "]");
                if (playerTurn == 0)
                {
                    stones = stones - 1;
                    if (stones == 0) // basically means this was the last stone placed
                    {
                        PlayerHasExtraTurn = true;
                    }
                    else
                    {
                        moveStones(playerTurn, 1, 0, stones);
                    }
                }
                else if (playerTurn == 1)
                {
                    stones = stones - 1;
                    if (stones == 0) // basically means this was the last stone placed
                    {
                        PlayerHasExtraTurn = true;
                    }
                    else
                    {
                        moveStones(playerTurn, 0, 5, stones);
                    }
                }
            }
        }
    }

    public class ChatRoomInfo
    {
        public int Id { get; set; }
        public int NumberOfPlayers { get; set; }
        public ChatRoomInfo(int id, int numberOfPlayers)
        {
            Id = id;
            NumberOfPlayers = numberOfPlayers;
        }

    }

}
