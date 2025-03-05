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

        public List<ChessData> Parse(string[] PGNFileLines)
        {
            List<ChessData> data = new();
            foreach (string line in PGNFileLines)
            {
                if (!readingMoves)
                {
                    switch (line)
                    {
                        case (line.StartsWith("[Event ")):
                            Event = line.Substring(8, line.Length - 2); break;
                        case (line.StartsWith("[Site ")):
                            Site = line.Substring(7, line.Length - 2); break;
                        case (line.StartsWith("[Round ")):
                            Round = line.Substring(8, line.Length - 2); break;
                        case (line.StartsWith("[White ")):
                            WhitePlayer = line.Substring(8, line.Length - 2); break;
                        case (line.StartsWith("[Black ")):
                            BlackPlayer = line.Substring(8, line.Length - 2); break;
                        case (line.StartsWith("[Result ")):
                            Result = line.Substring(9, line.Length - 2); break;
                        case (line.StartsWith("[WhiteElo ")):
                            WhiteElo = int.Parse(line.Substring(11, line.Length - 2)); break;
                        case (line.StartsWith("[BlackElo ")):
                            BlackElo = int.Parse(line.Substring(11, line.Length - 2)); break;
                        case (line.StartsWith("[EventDate ")):
                            EventDate = line.Substring(12, line.Length - 2); break;
                        case (line.StartsWith("[")):
                            // Ignore other tags
                            break;
                        case (line.StartsWith("1.")):
                            Moves = line;
                            readingMoves = true; break;
                        default: break;
                    }
                }
                else
                {
                    if (line.StartsWith(""))
                    {
                        readingMoves = false;
                        // Create data object and add to list
                        data.Add(new ChessData(Event, Site, Round, WhitePlayer, BlackPlayer, WhiteElo, BlackElo,
                            Result, EventDate, Moves));
                    }
                    else
                        Moves += line;
                }
            }
            return data;
        }
    }
}