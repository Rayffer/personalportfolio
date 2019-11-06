namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface IObserverMapper
    {
        T Map<T>(object source);

        T2 Map<T1, T2>(T1 source, T2 destination);

        void AssertConfigurationIsValid();
    }
}