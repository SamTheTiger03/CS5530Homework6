namespace ChessBrowser.Components
{
    // A chess player in the database, their name and Elo.
    public class ChessPlayer
    {
        public string Name { get; private set; }
        public int Elo { get; private set; }

        /// <summary>
        /// Creates a ChessPlayer object.
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="elo">The Elo of the player</param>
        public ChessPlayer(string name, int elo)
        {
            this.Name = name;
            this.Elo = elo;
        }
    }
}
