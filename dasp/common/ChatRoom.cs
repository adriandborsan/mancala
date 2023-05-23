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
            int playerCurrentlyMoving = 0;
            int[,] gameStateMatrix = GameStateMatrix;
            int pocketCursor = pocket;
            if (daspConnections.FirstOrDefault(con => con.PlayerState == PlayerState.CURRENTLY_MOVING) == daspConnections[0])
            {
                // player 1 is the one who is moving
                playerCurrentlyMoving = 0; // player 1
            }
            if (daspConnections.FirstOrDefault(con => con.PlayerState == PlayerState.CURRENTLY_MOVING) == daspConnections[1])
            {
                // player 2 is the one who is moving
                playerCurrentlyMoving = 1; // player 2
            }

            // however many stones we're in the chosen pocket
            int stonesInHand = gameStateMatrix[playerCurrentlyMoving, pocket];
            _sendLog("took " + stonesInHand + " stones from pocket [" + playerCurrentlyMoving + "][" + pocket + "]");

            gameStateMatrix[playerCurrentlyMoving, pocket] = 0; // stones from that pocket we're removed

            if (playerCurrentlyMoving == 0)
            {
                _sendLog("player 1 is moving...");
                moveStones(playerCurrentlyMoving, 0, pocketCursor - 1, stonesInHand);
            }
            else if (playerCurrentlyMoving == 1)
            {
                _sendLog("player 2 is moving...");
                moveStones(playerCurrentlyMoving, 1, pocketCursor + 1, stonesInHand);
            }


            gameStateUpdated(this);
            //  this retrieves the player that is currently moving
            // daspConnections.FirstOrDefault(con => con.PlayerState == PlayerState.CURRENTLY_MOVING);
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
            Thread.Sleep(1000);
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
                        addStoneToStore(playerTurn, stones);
                    }
                    else if (pocketCursor == 0 && playerTurn == 1)
                    {
                        moveStones(playerTurn, 1, pocketCursor, stones--);
                    }
                    else
                    {
                        moveStones(playerTurn, tableSide, pocketCursor--, stones--);
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
                        moveStones(playerTurn, 0, pocketCursor, stones--);
                    }
                    else if (pocketCursor == 5 && playerTurn == 1)
                    {
                        addStoneToStore(playerTurn, stones);
                    }
                    else
                    {
                        moveStones(playerTurn, tableSide, pocketCursor++, stones--);
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
                    moveStones(playerTurn, 1, 0, stones--);
                }
                else if (playerTurn == 1)
                {
                    moveStones(playerTurn, 0, 5, stones--);
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
