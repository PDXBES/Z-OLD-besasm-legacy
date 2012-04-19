using System;

namespace SystemsAnalysis.Utils.Events
{
  public enum StatusChangeType
  {
    Info,
    Warning,
    Error
  }

  /// <summary>
  /// Event handler for reporting status changes.
  /// </summary>
  public delegate void OnStatusChangedEventHandler(SystemsAnalysis.Utils.Events.StatusChangedArgs e);

  public class StatusChanged
  {
  }

  /// <summary>
  /// Arguments for the OnStatusChanged event.
  /// </summary>
  public class StatusChangedArgs : EventArgs
  {
    private string status;
    private StatusChangeType statusChangeType;

    /// <summary>
    /// Sets the status reported by the OnStatusChanged event.
    /// </summary>
    /// <param name="status">The status message</param>
    public StatusChangedArgs(string status)
    {
      this.status = status;
      this.statusChangeType = StatusChangeType.Info;
    }

    /// <summary>
    /// Sets the status reported by the OnStatusChanged event.
    /// </summary>
    /// <param name="status">The status message</param>
    /// <param name="statusChangeType">Type of status message</param>
    public StatusChangedArgs(string status, StatusChangeType statusChangeType)
    {
      this.status = status;
      this.statusChangeType = statusChangeType;
    }
    /// <summary>
    /// Returns the new status message after
    /// a OnStatusChanged event.
    /// </summary>
    public string NewStatus
    {
      get
      {
        return this.status;
      }
    }

    /// <summary>
    /// Returns the type of the new status message after
    /// an OnStatusChanged event.
    /// </summary>
    public StatusChangeType NewStatusType
    {
      get
      {
        return this.statusChangeType;
      }
    }
  }
}
