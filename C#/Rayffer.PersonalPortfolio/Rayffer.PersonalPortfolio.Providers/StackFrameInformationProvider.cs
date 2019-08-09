using System.Diagnostics;

namespace Rayffer.PersonalPortfolio.Providers
{
    /// <summary>
    /// A class that allows the retrieval of information of the current stack frame
    /// </summary>
    public static class StackFrameInformationProvider
    {
        /// <summary>
        /// Gets the name of the currently called method.
        /// </summary>
        /// <param name="stackFrameLevel">The level or depth within the current Stack frame.
        /// Default to 1.</param>
        /// <returns>
        /// The method name.
        /// </returns>
        public static string GetCurrentMethodName(int stackFrameLevel = 1)
        {
            //return MethodBase.GetCurrentMethod().Name;
            return new StackFrame(stackFrameLevel).GetMethod().Name;
        }
    }
}