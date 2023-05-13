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
        public ChatRoom(List<DaspConnection> daspConnections, Func<ChatRoom, Task> gameStateUpdated)
        {
            this.ID = nextRoomId++;
            this.daspConnections = daspConnections;
            this.gameStateUpdated = gameStateUpdated;
        }

        public ChatRoomInfo ConvertToChatRoomInfo()
        {
            return new ChatRoomInfo(ID, daspConnections.Count);
        }

        internal void PocketMoved(int pocket)
        {
            gameStateUpdated(this);
            //this retrieves the player that is currently moving
           // daspConnections.FirstOrDefault(con => con.PlayerState == PlayerState.CURRENTLY_MOVING);
            throw new NotImplementedException();
        }

        internal void GameStarted(DaspConnection connection, DaspConnection targetConnection)
        {
            connection.PlayerState = PlayerState.CURRENTLY_MOVING;
            targetConnection.PlayerState= PlayerState.WAITING_TO_MOVE;
            gameStateUpdated(this);
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
