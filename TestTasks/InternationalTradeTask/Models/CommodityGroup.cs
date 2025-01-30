namespace TestTasks.InternationalTradeTask.Models
{
    internal class CommodityGroup : ICommodityGroup
    {
        public string Name { get; init; }

        public string SITCCode { get; init; }

        public ICommodityGroup[] SubGroups { get; init; }

        public double? ImportTarif { get; set; }

        public double? ExportTarif { get; set; }

        public CommodityGroup(string code, string name)
        {
            SITCCode = code;
            Name = name;
        }

        public CommodityGroup(string code, string name, double? importTarif, double? exportTarif) :
            this(code, name)
        {
            ImportTarif = importTarif;
            ExportTarif = exportTarif;
        }
    }
}
