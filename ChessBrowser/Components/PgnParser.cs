using System.Diagnostics;

namespace ChessBrowser.Components
{
    // Parses a pgn file to upload to the Chess database.
    public class PgnParser
    {
        private string Event = "";
        private string Site = "";
        private string Round = "";
        private string WhitePlayer = "";
        private string BlackPlayer = "";
        private int WhiteElo = 0;
        private int BlackElo = 0;
        private char Result = 'a';
        private string EventDate = "";
        private string Moves = "";
        private bool readingMoves = false;

        public PgnParser() { }

        /// <summary>
        /// Parses an input PGN file from an array of strings. Returns the data as a list of ChessData.
        /// </summary>
        /// <param name="PGNFileLines">A list of ChessData objets parsed from the input file.</param>
        /// <returns></returns>
        public List<ChessData> Parse(string[] PGNFileLines)
        {
            List<ChessData> data = new();
            foreach (string line in PGNFileLines)
            {
                if (!readingMoves)
                {
                    if (line.StartsWith("[Event "))
                        Event = line.Substring(8, line.Length - 10);
                    else if (line.StartsWith("[Site "))
                        Site = line.Substring(7, line.Length - 9);
                    else if (line.StartsWith("[Round "))
                        Round = line.Substring(8, line.Length - 10);
                    else if (line.StartsWith("[White "))
                        WhitePlayer = line.Substring(8, line.Length - 10);
                    else if (line.StartsWith("[Black "))
                        BlackPlayer = line.Substring(8, line.Length - 10);
                    else if (line.StartsWith("[Result "))
                    {
                        string resultVal = line.Substring(9, line.Length - 11);
                        if (resultVal == "1/2-1/2")
                            Result = 'D';
                        else if (resultVal == "1-0")
                            Result = 'W';
                        else if (resultVal == "0-1")
                            Result = 'B';
                    }
                        
                    else if (line.StartsWith("[WhiteElo "))
                        WhiteElo = int.Parse(line.Substring(11, line.Length - 13));
                    else if (line.StartsWith("[BlackElo "))
                        BlackElo = int.Parse(line.Substring(11, line.Length - 13));
                    else if (line.StartsWith("[EventDate "))
                        EventDate = line.Substring(12, line.Length - 14);
                    else if (line.StartsWith("["))
                        // Ignore other tags
                        continue;
                    else if (line.StartsWith("1."))
                    {
                        Moves = line;
                        readingMoves = true;
                    }
                }
                else
                {
                    if (line.Length == 0)
                    {
                        readingMoves = false;
                        // Create data object and add to list
                        ChessData temp = new ChessData(Event, Site, Round, WhitePlayer, BlackPlayer, WhiteElo, BlackElo,
                            Result, EventDate, Moves);
                        data.Add(temp);
                    }
                    else
                        Moves += line;
                }
            }
            return data;
        }
    }
}