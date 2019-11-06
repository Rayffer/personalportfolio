using log4net;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing
{
    internal class Log4NetTracing : ITracing
    {
        private Type type;

        private static object _trafficLight = new object();
        private static Dictionary<string, ILog> _logStorage = null;

        private ILog iLog;

        ///--------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        [DebuggerStepThrough]
        public Log4NetTracing()
        {
            // Use type of caller
            StackTrace stackTrace = new StackTrace();
            if (stackTrace != null)
            {
                StackFrame stackFrame = stackTrace.GetFrame(1); // deep=1 is caller
                if (stackFrame != null)
                {
                    MethodBase methodBase = stackFrame.GetMethod();
                    if (methodBase != null)
                    {
                        if (methodBase.DeclaringType != null)
                        {
                            if (methodBase.DeclaringType.AssemblyQualifiedName != null)
                            {
                                iLog = LoggerFactory(methodBase.DeclaringType.AssemblyQualifiedName, methodBase.DeclaringType);
                                return;
                            }
                        }
                    }
                }
            }

            StackFrame stackFrameGet = GetActiveFrame(stackTrace, 1);
            if (stackFrameGet != null)
            {
                Type type = stackFrameGet.GetMethod().DeclaringType;
                iLog = LoggerFactory(type.AssemblyQualifiedName, type);
            }
            else
                iLog = LoggerFactory("");
        }

        [DebuggerStepThrough]
        public Log4NetTracing(string name)
        {
            iLog = LoggerFactory(name);
        }

        [DebuggerStepThrough]
        public Log4NetTracing(Type type)
        {
            iLog = LoggerFactory(type.AssemblyQualifiedName, type);
        }

        [DebuggerStepThrough]
        private ILog LoggerFactory(string name, Type type = null)
        {
            bool executeStartApp = false;
            ILog localLog;

            lock (_trafficLight)
            {
                if (_logStorage == null)
                {
                    _logStorage = new Dictionary<string, ILog>();
                    executeStartApp = true;
                }

                if (!_logStorage.ContainsKey(name))
                {
                    if (type == null)
                        _logStorage[name] = LogManager.GetLogger(name);
                    else
                        _logStorage[name] = LogManager.GetLogger(type);
                }

                localLog = _logStorage[name];

                if (executeStartApp)
                    StartApp(localLog);
            }

            //if (type == null)
            //    _logStorage[name].InfoFormat("Log4Net by Name: {0}", name);
            //else
            //    _logStorage[name].InfoFormat("Log4Net by Type: {0}", type.AssemblyQualifiedName);

            return localLog;
        }

        [DebuggerStepThrough]
        private void StartApp(ILog log)
        {
            string loggerHeader = "STARTING EXECUTION";
            string loggerName = "";

            if (log.Logger != null)
                loggerName = log.Logger.Name;

            int textLength = Math.Max(loggerHeader.Length, loggerName.Length);

            string loggerNameLine = null;
            if (!string.IsNullOrEmpty(loggerName))
                loggerNameLine = string.Concat("=== ", new string(' ', (textLength - loggerName.Length) / 2), loggerName, new string(' ', (textLength - loggerName.Length) - ((textLength - loggerName.Length) / 2)), " ===");

            string loggerHeaderLine = string.Concat("=== ", new string(' ', (textLength - loggerHeader.Length) / 2), loggerHeader, new string(' ', (textLength - loggerHeader.Length) - ((textLength - loggerHeader.Length) / 2)), " ===");

            string line = new string('=', loggerHeaderLine.Length);

            log.Info(AttachMethodAndClass(line));
            log.Info(AttachMethodAndClass(loggerHeaderLine));
            //if (!string.IsNullOrEmpty(loggerNameLine))
            //    log.Info(AttachMethodAndClass(loggerNameLine));
            log.Info(AttachMethodAndClass(line));
        }

        [DebuggerStepThrough]
        public void Error(string message, Exception exception = null)
        {
            TraceEvent(TraceEventType.Error, message, exception);
        }

        [DebuggerStepThrough]
        public void ErrorFormat(string message, params object[] objects)
        {
            TraceEventFormat(TraceEventType.Error, message, objects);
        }

        [DebuggerStepThrough]
        public void ErrorVerbose(string message, string memberName = "", string filePath = "", int lineNumber = 0)
        {
            TraceEventFormat(TraceEventType.Error, "{0}\n\nMethod: {1}\nFilename: {2}\nLine number: {3}", message, memberName, filePath, lineNumber);
        }

        [DebuggerStepThrough]
        public void Information(string message, Exception exception = null)
        {
            TraceEvent(TraceEventType.Information, message, exception);
        }

        [DebuggerStepThrough]
        public void InformationFormat(string message, params object[] objects)
        {
            TraceEventFormat(TraceEventType.Information, message, objects);
        }

        [DebuggerStepThrough]
        public void Start(string message)
        {
            iLog.Info("************************************************************************");
            iLog.Info(string.Format("Start: {0}", message ?? "NULL"));
            iLog.Info("************************************************************************");
        }

        [DebuggerStepThrough]
        public void Stop(string message)
        {
            iLog.Info("************************************************************************");
            iLog.Info(string.Format("Stop: {0}", message ?? "NULL"));
            iLog.Info("************************************************************************");
        }

        [DebuggerStepThrough]
        public void TraceEvent(TraceEventType type, string message, Exception exception = null)
        {
            message = AttachMethodAndClass(message);

            switch (type)
            {
                case TraceEventType.Information:
                    iLog.Info(message, exception);
                    break;

                case TraceEventType.Error:
                    iLog.Error(message, exception);
                    break;

                case TraceEventType.Warning:
                    iLog.Warn(message, exception);
                    break;

                case TraceEventType.Verbose:
                    iLog.Debug(message, exception);
                    break;

                case TraceEventType.Transfer:
                    Transfer(message, Guid.NewGuid());
                    break;

                case TraceEventType.Start:
                    Start(message);
                    break;

                case TraceEventType.Stop:
                    Stop(message);
                    break;

                default:
                    iLog.Info(message, exception);
                    break;
            }
        }

        [DebuggerStepThrough]
        public void TraceEventFormat(TraceEventType type, string message, params object[] objects)
        {
            message = AttachMethodAndClass(message);

            switch (type)
            {
                case TraceEventType.Information:
                    iLog.InfoFormat(message, objects);
                    break;

                case TraceEventType.Error:
                    iLog.ErrorFormat(message, objects);
                    break;

                case TraceEventType.Warning:
                    iLog.WarnFormat(message, objects);
                    break;

                case TraceEventType.Verbose:
                    iLog.DebugFormat(message, objects);
                    break;

                case TraceEventType.Transfer:
                    Transfer(message, Guid.NewGuid());
                    break;

                case TraceEventType.Start:
                    Start(message);
                    break;

                case TraceEventType.Stop:
                    Stop(message);
                    break;

                default:
                    iLog.InfoFormat(message, objects);
                    break;
            }
        }

        [DebuggerStepThrough]
        public void Transfer(string message, Guid activity)
        {
            return;
        }

        [DebuggerStepThrough]
        public void Verbose(string message, Exception exception = null)
        {
            TraceEvent(TraceEventType.Verbose, message, exception);
        }

        [DebuggerStepThrough]
        public void VerboseFormat(string message, params object[] objects)
        {
            TraceEventFormat(TraceEventType.Verbose, message, objects);
        }

        [DebuggerStepThrough]
        public void Warning(string message, Exception exception = null)
        {
            TraceEvent(TraceEventType.Warning, message, exception);
        }

        [DebuggerStepThrough]
        public void WarningFormat(string message, params object[] objects)
        {
            TraceEventFormat(TraceEventType.Warning, message, objects);
        }

        [DebuggerStepThrough]
        private string AttachMethodAndClassOriginal(string message)
        {
            StackFrame stackInfo = null;
            int StackPosition = 0;
            while (stackInfo == null)
            {
                //string stackTemp = new StackFrame(StackPosition, true).GetMethod().DeclaringType.Name;

                bool correct = false;

                StackFrame stackFrame = new StackFrame(StackPosition, true);
                if (stackFrame != null)
                {
                    MethodBase methodBase = stackFrame.GetMethod();
                    if (methodBase != null)
                    {
                        System.Type type = methodBase.DeclaringType;
                        if (type != null)
                        {
                            string stackTemp = type.Name;

                            if (this != null)
                            {
                                System.Type localType = this.GetType();
                                if (localType != null)
                                {
                                    if (stackTemp != localType.Name)
                                        stackInfo = new StackFrame(StackPosition, true);
                                    else
                                        StackPosition++;

                                    correct = true;
                                }
                            }
                        }
                    }
                }

                //if (stackTemp != this.GetType().Name)
                //    stackInfo = new StackFrame(StackPosition, true);
                //else
                //    StackPosition++;

                if (!correct)
                {
                    return string.Format("{0}:: {1}: {2}", "NO DeclaringType.Name INFO", "NO GetMethod().Name INFO", message ?? "NULL");
                }
            }

            if (stackInfo != null)
            {
                if (stackInfo.GetMethod() != null)
                {
                    if (stackInfo.GetMethod().DeclaringType != null)
                        return string.Format("{0}:: {1}: {2}", stackInfo.GetMethod().DeclaringType.Name ?? "NULL", stackInfo.GetMethod().Name ?? "NULL", message ?? "NULL");
                }
            }

            return string.Format("{0}:: {1}: {2}", "NO DeclaringType.Name INFO", "NO GetMethod().Name INFO", message ?? "NULL");
        }

        [DebuggerStepThrough]
        private string AttachMethodAndClass(string message)
        {
            StackTrace stackTrace = new StackTrace();

            StackFrame stackFrame = GetActiveFrame(stackTrace, 1);

            if (stackFrame != null)
                return string.Format("{0}:: {1}: {2}", stackFrame.GetMethod().DeclaringType.Name ?? "NULL", stackFrame.GetMethod().Name ?? "NULL", message ?? "NULL");
            else
                return string.Format("{0}:: {1}: {2}", "NO DeclaringType.Name INFO", "NO GetMethod().Name INFO", message ?? "NULL");
        }

        [DebuggerStepThrough]
        private StackFrame GetActiveFrame(StackTrace stackTrace, int startFrame)
        {
            var asd = stackTrace.GetFrames()?.Select(sf => GetActiveFrame(sf)?.GetMethod()?.DeclaringType?.Name ?? string.Empty)?.ToList();
            for (int i = startFrame + 1; i < stackTrace.FrameCount; i++)
            {
                StackFrame stackFrame = GetActiveFrame(stackTrace.GetFrame(i));

                if (stackFrame != null)
                    return stackFrame;
            }
            return null;
        }

        [DebuggerStepThrough]
        private StackFrame GetActiveFrame(StackFrame stackFrame)
        {
            if (stackFrame != null)
            {
                MethodBase methodBase = stackFrame.GetMethod();
                if (methodBase != null)
                {
                    if (methodBase.DeclaringType != null)
                    {
                        if (methodBase.DeclaringType.Name != null)
                        {
                            if (methodBase.DeclaringType.AssemblyQualifiedName != null)
                            {
                                if (!methodBase.DeclaringType.AssemblyQualifiedName.Contains("Microsoft.Practices.")
                                    && !methodBase.DeclaringType.AssemblyQualifiedName.Contains("System")
                                    && !methodBase.DeclaringType.AssemblyQualifiedName.Contains("Tracing"))
                                    return stackFrame;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}