namespace ChessBrowser.Components
{
    // Represents a chess game in the database, the two players playing, the round and event it took place in, and
    // the list of moves in the game.
    public class ChessGame
    {
        private ChessEvent Event { get; }
        private ChessPlayer WhitePlayer { get; }
        private ChessPlayer BlackPlayer { get; }
        private char Result { get; set; }
        private string Moves { get; set; }
        private string Round {  get; set; }

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
