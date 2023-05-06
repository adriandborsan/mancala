namespace dasp
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public int NumberOfPlayers { get; set; }
        public ChatRoom(int id, int numberOfPlayers)
        {
            Id = id;
            NumberOfPlayers = numberOfPlayers;
        }
    }
}
