using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.Design;

namespace ExtnHelpers
{
    /// <summary>
    /// Severity: information, warning, error, and unspecified 
    /// </summary>	
    public enum LogSeverity
    {
        /// <summary>
        /// Log with unspecified severity
        /// </summary>
        //Unspecified = Severity.Unspecified,
        Unspecified = 1,
        /// <summary>
        /// Log with Information severity
        /// </summary>
        //Information = Severity.Information,
        Information = 2,
        /// <summary>
        /// Log with Warning severity
        /// </summary>
        //Warning = Severity.Warning,
        Warning = 3,
        /// <summary>
        /// Log with Error severity
        /// </summary>
        //Error = Severity.Error
        Error = 4
    };

}