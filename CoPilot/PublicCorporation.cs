namespace CoPilot
{
    /// <summary>
    /// Represents a Virginia public corporation with essential financial information.
    /// </summary>
    public class PublicCorporation
    {
        /// <summary>
        /// Gets an array of the top Virginia public corporations.
        /// </summary>
        private static PublicCorporation[] VirginiaCorporationsArray { get; } = new PublicCorporation[]
        {
            new PublicCorporation("GD", "General Dynamics Corporation", "Reston, VA", 75_200_000_000m),
            new PublicCorporation("NVR", "NVR, Inc.", "Reston, VA", 28_400_000_000m),
            new PublicCorporation("ADP", "Automatic Data Processing (HQ2)", "Norfolk, VA", 25_800_000_000m),
            new PublicCorporation("CACI", "CACI International Inc.", "Reston, VA", 15_600_000_000m),
            new PublicCorporation("SAIC", "Science Applications International Corp.", "Reston, VA", 10_900_000_000m),
            new PublicCorporation("HII", "Huntington Ingalls Industries", "Newport News, VA", 8_700_000_000m),
            new PublicCorporation("VRS", "Verso Corporation", "Fairfax, VA", 5_200_000_000m),
            new PublicCorporation("MPW", "Medical Properties Trust, Inc.", "Richmond, VA", 4_800_000_000m),
            new PublicCorporation("TGLS", "Tecnoglass Inc.", "Fairfax, VA", 3_900_000_000m),
            new PublicCorporation("WMS", "Advanced Drainage Systems", "Virginia Beach, VA", 3_200_000_000m)
        };

        /// <summary>
        /// Gets a list of the top Virginia public corporations, created from the array.
        /// </summary>
        public static List<PublicCorporation> VirginiaCorporationsList { get; } = new List<PublicCorporation>(VirginiaCorporationsArray);

        /// <summary>
        /// Gets a dictionary of Virginia public corporations where the key is the stock symbol.
        /// </summary>
        public static Dictionary<string, PublicCorporation> VirginiaCorporationsDictionary { get; } = 
            VirginiaCorporationsArray.ToDictionary(corp => corp.StockSymbol, corp => corp);

        /// <summary>
        /// Gets a hash set of all Virginia public corporation stock symbols.
        /// </summary>
        public static HashSet<string> VirginiaCorporationsStockSymbols { get; } = 
            new HashSet<string>(VirginiaCorporationsArray.Select(corp => corp.StockSymbol));

        /// <summary>
        /// Gets a queue of Virginia public corporations in FIFO order, created from the array.
        /// </summary>
        public static Queue<PublicCorporation> VirginiaCorporationsQueue { get; } = 
            new Queue<PublicCorporation>(VirginiaCorporationsArray);

        /// <summary>
        /// Gets a stack of Virginia public corporations in LIFO order, created from the array.
        /// </summary>
        public static Stack<PublicCorporation> VirginiaCorporationsStack { get; } = 
            new Stack<PublicCorporation>(VirginiaCorporationsArray);

        /// <summary>
        /// Gets or sets the stock ticker symbol.
        /// </summary>
        public string StockSymbol { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the legal name of the corporation.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the location (city, state) of the corporation's headquarters.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the market capitalization in dollars.
        /// </summary>
        public decimal MarketCap { get; set; }

        /// <summary>
        /// Initializes a new instance of the PublicCorporation class.
        /// </summary>
        public PublicCorporation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PublicCorporation class with all fields.
        /// </summary>
        /// <param name="stockSymbol">The stock ticker symbol.</param>
        /// <param name="name">The legal name of the corporation.</param>
        /// <param name="location">The location of the corporation's headquarters.</param>
        /// <param name="marketCap">The market capitalization in dollars.</param>
        public PublicCorporation(string stockSymbol, string name, string location, decimal marketCap)
        {
            StockSymbol = stockSymbol ?? throw new ArgumentNullException(nameof(stockSymbol));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Location = location ?? throw new ArgumentNullException(nameof(location));
            MarketCap = marketCap;
        }

        /// <summary>
        /// Returns a formatted market cap string (e.g., "$1.5B", "$250M").
        /// </summary>
        /// <returns>The formatted market capitalization.</returns>
        public string GetFormattedMarketCap()
        {
            if (MarketCap >= 1_000_000_000)
                return $"${MarketCap / 1_000_000_000:F2}B";
            else if (MarketCap >= 1_000_000)
                return $"${MarketCap / 1_000_000:F2}M";
            else if (MarketCap >= 1_000)
                return $"${MarketCap / 1_000:F2}K";
            else
                return $"${MarketCap:F2}";
        }

        /// <summary>
        /// Returns a string representation of the corporation.
        /// </summary>
        /// <returns>A string containing the corporation's key information.</returns>
        public override string ToString()
        {
            return $"{StockSymbol} - {Name} | {Location} | Market Cap: {GetFormattedMarketCap()}";
        }

        /// <summary>
        /// Validates whether the corporation has all required information.
        /// </summary>
        /// <returns>True if all required fields are populated; otherwise, false.</returns>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(StockSymbol) &&
                   !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Location) &&
                   MarketCap > 0;
        }
    }
}
