using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PNSLogger : ILogHandler
{
    private Logger logger;

    public string LoggerName { get; private set; }
    public bool IsReadyToLog { get; set; }

    private void logMessage(string loggerTag, string message) { this.logger.Log(loggerTag, message); }

    public PNSLogger(string loggerName)
    {
        this.logger = new Logger(this);
        this.LoggerName = loggerName;

        this.IsReadyToLog = true;
    }


    public void Log(string message)
    {
        if(this.IsReadyToLog)
            this.logMessage(this.LoggerName, message);
    }

    public void Log(object message)
    {
        if (this.IsReadyToLog)
            this.logMessage(this.LoggerName, message.ToString());
    }

    public void LogException(Exception exception, UnityEngine.Object context)
    {
        Debug.unityLogger.LogException(exception, context);
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        Debug.unityLogger.logHandler.LogFormat(logType, context, format, args);
    }
}