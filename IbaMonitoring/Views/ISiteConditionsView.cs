
namespace IbaMonitoring.Views
{
    public interface ISiteConditionsView
    {
        string Observer { get; }
        string Recorder { get; }
        string SiteVisited { get; }
        string StartSky { get; }
        string StartUnit { get; }
        string StartTemp { get; }
        string StartWind { get; }
        string StartTime { get; }
        string EndSky { get; }
        string EndUnit { get; }
        string EndTemp { get; }
        string EndWind { get; }
        string EndTime { get; }
        string VisitDate { get; }
        bool IsValid { get; }
    }
}
