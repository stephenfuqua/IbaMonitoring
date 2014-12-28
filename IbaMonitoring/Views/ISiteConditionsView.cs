
namespace IbaMonitoring.Views
{
    public interface ISiteConditionsView
    {
        string SiteVisitObserverAccessor { get; }
        string SiteVisitRecorderAccessor { get; }
        string SiteVisitedAccessor { get; }
        string StartSkyAccessor { get; }
        string StartTempUnitsAccessor { get; }
        string StartTempAccessor { get; }
        string StartWindAccessor { get; }
        string StartTimeAccessor { get; }
        string EndSkyAccessor { get; }
        string EndTempUnitsAccessor { get; }
        string EndTempAccessor { get; }
        string EndWindAccessor { get; }
        string EndTimeAccessor { get; }
        string VisitDateAccessor { get; }
        bool IsValid { get; }
    }
}
