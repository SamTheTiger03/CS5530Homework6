namespace ChessBrowser.Components
{
    // Represents a Chess event to add to the database, its name, site, and date.
    public class ChessEvent
    {
        private string Name { get; }
        private string Site { get; }
        private string EventDate { get; }

        /// <summary>
        /// Creates a ChessEvent object.
        /// </summary>
        /// <param name="name">The event name</param>
        /// <param name="site">The event site</param>
        /// <param name="eventDate">The date of the event</param>
        public ChessEvent(string name, string site, string eventDate)
        {
            this.Name = name;
            this.Site = site;
            this.EventDate = eventDate;
        }
    }
}
