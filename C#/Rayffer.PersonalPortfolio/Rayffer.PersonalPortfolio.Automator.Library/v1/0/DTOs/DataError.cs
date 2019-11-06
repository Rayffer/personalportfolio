using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs
{
    public class DataError
    {
        public DataErrorTypes ErrorType { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Error type: {ErrorType.ToString()}{(string.IsNullOrEmpty(Description) ? string.Empty : $", { Description}")}";
        }
    }
}