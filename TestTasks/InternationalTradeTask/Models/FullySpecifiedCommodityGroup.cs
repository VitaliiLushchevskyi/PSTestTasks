namespace TestTasks.InternationalTradeTask.Models
{
    internal class FullySpecifiedCommodityGroup : ICommodityGroup
    {
        public string Name { get; init; }

        public string SITCCode { get; init; }

        public ICommodityGroup[] SubGroups { get; init; }

        public double? ImportTarif { get; init; }

        public double? ExportTarif { get; init; }

        public FullySpecifiedCommodityGroup(string code, string name, double importTarif, double exportTarif)
        {
            SITCCode = code;
            Name = name;
            ImportTarif = importTarif;
            ExportTarif = exportTarif;
        }
    }
}
