namespace dasp
{
    public class DaspRequest
    {
        public DaspHeader DaspHeader { get; set; }
        public DaspBody DaspBody { get; set; }
        public int Size { get; set; }
        public DaspRequest(DaspHeader daspHeader, DaspBody daspBody, int size)
        {
            DaspHeader = daspHeader;
            DaspBody = daspBody;
            Size = size;
        }
        public DaspRequest(DaspHeader daspHeader, DaspBody daspBody)
        {
            DaspHeader = daspHeader;
            DaspBody = daspBody;
        }
    }

    public class DaspHeader
    {
        public string Command { get; set; }

        public DaspHeader(string command)
        {
            Command = command;
        }
    }

    public class DaspBody
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoomId { get; set; }
        public string Recipient { get; set; }
        public string Message { get; set; }
        public List<string> Players { get; set; }
        public List<ChatRoomInfo> Rooms { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public int Pocket { get; set; }

        public int[,] GameStateMatrix { get; set; }
        public bool PlayerTurnInformation { get; set; }
        public int GameEndStatus { get; set; }


    }

    public class DaspBodyBuilder
    {
        private readonly DaspBody _daspBody = new();

        public DaspBodyBuilder WithUsername(string username)
        {
            _daspBody.Username = username;
            return this;
        }

        public DaspBodyBuilder WithPassword(string password)
        {
            _daspBody.Password = password;
            return this;
        }

        public DaspBodyBuilder WithRoomId(int roomId)
        {
            _daspBody.RoomId = roomId;
            return this;
        }

        public DaspBodyBuilder WithRecipient(string recipient)
        {
            _daspBody.Recipient = recipient;
            return this;
        }

        public DaspBodyBuilder WithMessage(string message)
        {
            _daspBody.Message = message;
            return this;
        }

        public DaspBodyBuilder WithPlayers(List<string> players)
        {
            _daspBody.Players = players;
            return this;
        }

        public DaspBodyBuilder WithRooms(List<ChatRoomInfo> rooms)
        {
            _daspBody.Rooms = rooms;
            return this;
        }

        public DaspBodyBuilder WithReason(string reason)
        {
            _daspBody.Reason = reason;
            return this;
        }

        public DaspBodyBuilder WithStatus(string status)
        {
            _daspBody.Status = status;
            return this;
        }

        public DaspBodyBuilder WithPocket(int pocket)
        {
            _daspBody.Pocket = pocket;
            return this;
        }

        public DaspBodyBuilder WithGameStateMatrix(int[,] gameStateMatrix)
        {
            _daspBody.GameStateMatrix = gameStateMatrix;
            return this;
        }

        public DaspBodyBuilder WithPlayerTurnInformation(bool playerTurnInformation)
        {
            _daspBody.PlayerTurnInformation = playerTurnInformation;
            return this;
        }

        public DaspBodyBuilder WithGameEndStatus(int gameEndStatus)
        {
            _daspBody.GameEndStatus = gameEndStatus;
            return this;
        }

        public DaspBody Build()
        {
            return _daspBody;
        }
    }

}
