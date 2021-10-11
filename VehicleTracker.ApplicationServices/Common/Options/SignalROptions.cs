namespace VehicleTracker.ApplicationServices.Common.Options
{
    public class SignalROptions
    {
        public string PrimaryConnectionString { get; set; }
        public string SecondaryConnectionString { get; set; }
        public bool UseAzureSignalRService { get; set; }
    }
}
