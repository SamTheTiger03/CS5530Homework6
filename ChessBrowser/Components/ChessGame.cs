namespace ChessBrowser.Components
{
    // Represents a chess game in the database, the two players playing, the round and event it took place in, and
    // the list of moves in the game.
    public class ChessGame
    {
        public ChessEvent Event { get; private set; }
        public ChessPlayer WhitePlayer { get; private set; }
        public ChessPlayer BlackPlayer { get; private set; }
        public char Result { get; private set; }
        public string Moves { get; private set; }
        public string Round {  get; private set; }

        /// <summary>
        /// Creates a ChessGame object.
        /// </summary>
        /// <param name="e">The event object</param>
        /// <param name="round">The round</param>
        /// <param name="wp">A player object, white player</param>
        /// <param name="bp">A player object, black player</param>
        /// <param name="result">The result</param>
        /// <param name="moves">The list of moves</param>
        public ChessGame(ChessEvent e, string round, ChessPlayer wp, ChessPlayer bp, char result, string moves)
        {
            this.Event = e;
            this.Round = round;
            this.WhitePlayer = wp;
            this.BlackPlayer = bp;
            this.Result = result;
            this.Moves = moves;
        }
    }
}
