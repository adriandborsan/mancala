namespace dasp
{
    public class ChatRoom
    {

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
