namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface IObserverCheck
    {
        bool IsMandatory { get; set; }
        Types.PriorityTypes Priority { get; set; }

        bool PerformCheck();
    }
}