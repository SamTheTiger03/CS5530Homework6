using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace ChessBrowser.Components.Pages
{
    public partial class ChessBrowser
    {
        /// <summary>
        /// Bound to the Unsername form input
        /// </summary>
        private string Username = "";

        /// <summary>
        /// Bound to the Password form input
        /// </summary>
        private string Password = "";

        /// <summary>
        /// Bound to the Database form input
        /// </summary>
        private string Database = "";

        /// <summary>
        /// Represents the progress percentage of the current
        /// upload operation. Update this value to update 
        /// the progress bar.
        /// </summary>
        private int Progress = 0;

        /// <summary>
        /// This method runs when a PGN file is selected for upload.
        /// Given a list of lines from the selected file, parses the 
        /// PGN data, and uploads each chess game to the user's database.
        /// </summary>
        /// <param name="PGNFileLines">The lines from the selected file</param>
        private async Task InsertGameData(string[] PGNFileLines)
        {
            // This will build a connection string to your user's database on atr,
            // assuimg you've filled in the credentials in the GUI
            string connection = GetConnectionString();

            // TODO:
            //   Parse the provided PGN data
            //   We recommend creating separate libraries to represent chess data and load the file            
            PgnParser parser = new();
            List<ChessData> chessData = parser.Parse(PGNFileLines);


            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                try
                {
                    // Open a connection
                    conn.Open();

                    // TODO:
                    //   Iterate through your data and generate appropriate insert commands

                    int counter = 0;
                    int length = chessData.Count;
                    foreach (ChessData cD in chessData)
                    {
                        MySqlCommand wPlayerCommand = conn.CreateCommand();
                        wPlayerCommand.CommandText = "insert into Players (Name, Elo) values (@pName, @elo) on duplicate key update Elo = if(@elo > Elo, @elo, Elo)";
                        MySqlCommand bPlayerCommand = conn.CreateCommand();
                        bPlayerCommand.CommandText = "insert into Players (Name, Elo) values (@pName, @elo) on duplicate key update Elo = if(@elo > Elo, @elo, Elo)";
                        MySqlCommand eventCommand = conn.CreateCommand();
                        eventCommand.CommandText = "insert ignore into Events (Name, Site, Date) values (@eName, @site, @date)";
                        MySqlCommand gameCommand = conn.CreateCommand();
                        gameCommand.CommandText = "insert ignore into Games values (@round, @result, @moves," +
                                                    "(select pID from Players where Name = @bName)," +
                                                    "(select pID from Players where Name = @wName)," +
                                                    "(select eID from Events where Name = @eName and Site = @site and Date = @date))";
                        //Adds white player
                        wPlayerCommand.Parameters.AddWithValue("@pName", cD.game.WhitePlayer.Name);
                        wPlayerCommand.Parameters.AddWithValue("@elo", cD.game.WhitePlayer.Elo);
                        wPlayerCommand.ExecuteNonQuery();
                        //Adds black player
                        bPlayerCommand.Parameters.AddWithValue("@pName", cD.game.BlackPlayer.Name);
                        bPlayerCommand.Parameters.AddWithValue("@elo", cD.game.BlackPlayer.Elo);
                        bPlayerCommand.ExecuteNonQuery();
                        //Adds event
                        eventCommand.Parameters.AddWithValue("@eName", cD.game.Event.Name);
                        eventCommand.Parameters.AddWithValue("@site", cD.game.Event.Site);
                        eventCommand.Parameters.AddWithValue("@date", cD.game.Event.EventDate);
                        eventCommand.ExecuteNonQuery();
                        //Adds game
                        gameCommand.Parameters.AddWithValue("@round", cD.game.Round);
                        gameCommand.Parameters.AddWithValue("@result", cD.game.Result);
                        gameCommand.Parameters.AddWithValue("@moves", cD.game.Moves);
                        gameCommand.Parameters.AddWithValue("@bName", cD.game.BlackPlayer.Name);
                        gameCommand.Parameters.AddWithValue("@wName", cD.game.WhitePlayer.Name);
                        gameCommand.Parameters.AddWithValue("@eName", cD.game.Event.Name);
                        gameCommand.Parameters.AddWithValue("@site", cD.game.Event.Site);
                        gameCommand.Parameters.AddWithValue("@date", cD.game.Event.EventDate);
                        gameCommand.ExecuteNonQuery();
                        // TODO:
                        //   Update the Progress member variable every time progress has been made
                        //   (e.g. one iteration of your upload loop)
                        //   This will update the progress bar in the GUI
                        //   Its value should be an integer representing a percentage of completion
                        counter++;
                        Progress = (int)((counter * 100) / length);
                        // This tells the GUI to redraw after you update Progress (this should go inside your loop)
                        await InvokeAsync(StateHasChanged);
                    }




                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

        }


        /// <summary>
        /// Queries the database for games that match all the given filters.
        /// The filters are taken from the various controls in the GUI.
        /// </summary>
        /// <param name="white">The white player, or "" if none</param>
        /// <param name="black">The black player, or "" if none</param>
        /// <param name="opening">The first move, e.g. "1.e4", or "" if none</param>
        /// <param name="winner">The winner as "W", "B", "D", or "" if none</param>
        /// <param name="useDate">true if the filter includes a date range, false otherwise</param>
        /// <param name="start">The start of the date range</param>
        /// <param name="end">The end of the date range</param>
        /// <param name="showMoves">true if the returned data should include the PGN moves</param>
        /// <returns>A string separated by newlines containing the filtered games</returns>
        private string PerformQuery(string white, string black, string opening,
          string winner, bool useDate, DateTime start, DateTime end, bool showMoves)
        {
            // This will build a connection string to your user's database on atr,
            // assuimg you've typed a user and password in the GUI
            string connection = GetConnectionString();

            // Build up this string containing the results from your query
            string parsedResult = "";

            // Use this to count the number of rows returned by your query
            // (see below return statement)
            int numRows = 0;

            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                try
                {
                    // Open a connection
                    conn.Open();

                    // TODO:
                    //   Generate and execute an SQL command,
                    //   then parse the results into an appropriate string and return it.
                    //This gives date, site, eventName
                    MySqlCommand selectCommand = conn.CreateCommand();
                    selectCommand.CommandText = "select e.Name as Event, e.Site, e.Date, wp.Name as White, wp.Elo as wElo, bp.Name as Black, bp.Elo as bElo, g.Result, g.Moves " +
                            "from Events e natural join Games g join Players wp right join Players bp " +
                            "on bp.pID = g.BlackPlayer where wp.pID = g.WhitePlayer " +
                            "and wp.Name like @wName " +
                            "and bp.name like @bName " +
                            "and g.Moves like @opening " +
                            "and g.Result like @winner " +
                            "and e.Date >= @start " +
                            "and e.Date <= @end";
                    selectCommand.Parameters.AddWithValue("@wName", white + "%");
                    selectCommand.Parameters.AddWithValue("@bName", black + "%");
                    selectCommand.Parameters.AddWithValue("@opening", opening + "%");
                    selectCommand.Parameters.AddWithValue("@winner", winner + "%");
                    if (useDate)
                    {
                        selectCommand.Parameters.AddWithValue("@start", start);
                        selectCommand.Parameters.AddWithValue("@end", end);
                    }
                    else
                    {
                        selectCommand.Parameters.AddWithValue("@start", "0000-00-00");
                        selectCommand.Parameters.AddWithValue("@end", DateTime.Today);
                    }

                    using (MySqlDataReader eventReader = selectCommand.ExecuteReader())
                    {
                        while (eventReader.Read())
                        {
                            numRows += 1;
                            parsedResult += "\nEvent: " + eventReader["Event"]
                                        + "\nSite: " + eventReader["Site"]
                                        + "\nDate: " + eventReader["Date"]
                                        + "\nWhite: " + eventReader["White"] + " (" + eventReader["wElo"] + ")"
                                        + "\nBlack: " + eventReader["Black"] + " (" + eventReader["bElo"] + ")"
                                        + "\nResult: " + eventReader["Result"] + "\n";
                            if (showMoves)
                            {
                                parsedResult += eventReader["Moves"] + "\n";
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

            return numRows + " results\n" + parsedResult;
        }


        private string GetConnectionString()
        {
            return "server=atr.eng.utah.edu;database=" + Database + ";uid=" + Username + ";password=" + Password;
        }


        /// <summary>
        /// This method will run when the file chooser is used.
        /// It loads the files contents as an array of strings,
        /// then invokes the InsertGameData method.
        /// </summary>
        /// <param name="args">The event arguments, which contains the selected file name</param>
        private async void HandleFileChooser(EventArgs args)
        {
            try
            {
                string fileContent = string.Empty;

                InputFileChangeEventArgs eventArgs = args as InputFileChangeEventArgs ?? throw new Exception("unable to get file name");
                if (eventArgs.FileCount == 1)
                {
                    var file = eventArgs.File;
                    if (file is null)
                    {
                        return;
                    }

                    // load the chosen file and split it into an array of strings, one per line
                    using var stream = file.OpenReadStream(1000000); // max 1MB
                    using var reader = new StreamReader(stream);
                    fileContent = await reader.ReadToEndAsync();
                    string[] fileLines = fileContent.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                    // insert the games, and don't wait for it to finish
                    // _ = throws away the task result, since we aren't waiting for it
                    _ = InsertGameData(fileLines);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("an error occurred while loading the file..." + e);
            }
        }

    }

}
