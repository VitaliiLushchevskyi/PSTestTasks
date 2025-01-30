namespace TestTasks.InternationalTradeTask.Models
{
    internal interface ICommodityGroup
    {
        public string Name { get; }

        public string SITCCode { get;  }

        public ICommodityGroup[] SubGroups { get; }

        public double? ImportTarif { get; }

        public double? ExportTarif { get; }
    }
}
