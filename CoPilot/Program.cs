using CoPilot;

var coStar = new PublicCorporation("CSGP", "CoStar Group, Inc.", "Reston, VA", 30_000_000_000m);
var altria = new PublicCorporation("ALTR", "Altria Group, Inc.", "Richmond, VA", 90_000_000_000m);

//--< LIST MANIPULATION >--
var corporationList = PublicCorporation.VirginiaCorporationsList;
corporationList.Add(coStar);
corporationList.Add(altria);

var corp = corporationList.FirstOrDefault(c => c.StockSymbol == "CSGP");
if (corp != null)
{
    corporationList.Remove(corp);
}
corp = corporationList.FirstOrDefault(c => c.StockSymbol == "ALTR");
if (corp != null)
{
    corporationList.Remove(corp);
}

//--< DICTIONARY MANIPULATION >--
var corporationDict = PublicCorporation.VirginiaCorporationsDictionary;
var values = corporationDict.Values.ToList();
var keys = corporationDict.Keys.ToList();
corporationDict.Add("CSGP", coStar);
corp = corporationDict["CSGP"];
if (corp != null)
{
    corporationDict.Remove("CSGP");
}

//--< HASH SET MANIPULATION >--
var corporationHashSet = PublicCorporation.VirginiaCorporationsStockSymbols;
corp = corporationList.FirstOrDefault(c => c.StockSymbol == "ADP");
corporationHashSet.Add("CSGP");
corporationHashSet.Add("ALTR");
corporationHashSet.Remove("CSGP");
corporationHashSet.Remove("ALTR");
