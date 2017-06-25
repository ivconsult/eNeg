using citPOINT.eNeg.Data.Web;

namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Loading Types.
    /// </summary>
    public enum LoadTypes
    {
        /// <summary>
        /// Closed Negotiation.
        /// </summary>
        ClosedNegotiation,

        /// <summary>
        /// Published Negotiation.
        /// </summary>
        PublishedNegotiation
    }

    /// <summary>
    /// Loading Queue
    /// </summary>
    public class LoadingQueue
    {
        /// <summary>
        /// Gets or sets the current negotiation.
        /// </summary>
        /// <value>The current negotiation.</value>
        public Negotiation CurrentNegotiation { get; set; }

        /// <summary>
        /// Gets or sets the type of the load.
        /// </summary>
        /// <value>The type of the load.</value>
        public LoadTypes LoadType { get; set; }
    }
}
