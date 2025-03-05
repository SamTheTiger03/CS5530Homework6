namespace ChessBrowser.Components
{
    // Represents a Chess event to add to the database, its name, site, and date.
    public class ChessEvent
    {
        public string Name { get; private set; }
        public string Site { get; private set; }
        public string EventDate { get; private set; }

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
