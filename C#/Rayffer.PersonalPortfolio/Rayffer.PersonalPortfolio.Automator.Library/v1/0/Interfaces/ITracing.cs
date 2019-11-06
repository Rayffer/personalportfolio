using System;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface ITracing
    {
        void Start(string message);

        void Stop(string message);

        void Transfer(string message, Guid activity);

        void TraceEvent(System.Diagnostics.TraceEventType type, string message, Exception exception = null);

        void TraceEventFormat(System.Diagnostics.TraceEventType type, string message, params object[] objects);

        void Error(string message, Exception exception = null);

        void ErrorFormat(string message, params object[] objects);

        void ErrorVerbose(string message, string memberName = "", string filePath = "", int lineNumber = 0);

        void Information(string message, Exception exception = null);

        void InformationFormat(string message, params object[] objects);

        void Verbose(string message, Exception exception = null);

        void VerboseFormat(string message, params object[] objects);

        void Warning(string message, Exception exception = null);

        void WarningFormat(string message, params object[] objects);
    }
}