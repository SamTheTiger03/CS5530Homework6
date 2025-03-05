namespace ChessBrowser.Components
{
    public class ChessData
    {
        private string Event { get; }
        private string Site { get; }
        private string Round { get; }
        private string WhitePlayer { get; }
        private string BlackPlayer { get; }
        private int WhiteElo { get; }
        private int BlackElo { get; }
        private char Result { get; }
        private string EventDate { get; }
        private string Moves { get; }

        /// <summary>
        /// Creates a ChessData object. Created by the PgnParser and used by the ChessBrowser to create
        /// the ChessEvent, ChessPlayer, and ChessGame objects.
        /// </summary>
        /// <param name="ev">Event name</param>
        /// <param name="site">Site name</param>
        /// <param name="round">Round name</param>
        /// <param name="wp">White player name</param>
        /// <param name="bp">Black player name</param>
        /// <param name="wElo">White player elo</param>
        /// <param name="bElo">Black player elo</param>
        /// <param name="result">Game result</param>
        /// <param name="evDate">Evnet date</param>
        /// <param name="moves">List of moves as a string</param>
        public ChessData(string ev, string site, string round, string wp, string bp, int wElo, int bElo,
            char result, string evDate, string moves)
        {
            this.Event = ev;
            this.Site = site;
            this.Round = round;
            this.WhitePlayer = wp;
            this.BlackPlayer = bp;
            this.WhiteElo = wElo;
            this.BlackElo = bElo;
            this.Result = result;
            this.EventDate = evDate;
            this.Moves = moves;
        }

        public override string ToString()
        {
            return "Event: " + Event + "\nSite: " + Site + "\nRound: " + Round + "\nwPlayer: " + WhitePlayer +
                "\nbPlayer: " + BlackPlayer + "\nwElo: " + WhiteElo + "\nbElo: " + "\nResult: " + Result + 
                "\neDate: " + EventDate + "\nMoves: " + Moves;
        }
    }
}
