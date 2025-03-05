using System.Security.Policy;

namespace ChessBrowser.Components
{
    public class ChessData
    {
        public ChessGame game { get; private set; }

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
            ChessPlayer wPlayer = new ChessPlayer(wp, wElo);
            ChessPlayer bPlayer = new ChessPlayer(bp, bElo);
            ChessEvent gEvent = new ChessEvent(ev, site, evDate);
            game = new ChessGame(gEvent, round, wPlayer, bPlayer, result, moves);
        }

        /*public override string ToString()
        {
            return "Event: " + Event + "\nSite: " + Site + "\nRound: " + Round + "\nwPlayer: " + WhitePlayer +
                "\nbPlayer: " + BlackPlayer + "\nwElo: " + WhiteElo + "\nbElo: " + BlackElo + "\nResult: " + Result + 
                "\neDate: " + EventDate + "\nMoves: " + Moves;
        }*/
    }
}
