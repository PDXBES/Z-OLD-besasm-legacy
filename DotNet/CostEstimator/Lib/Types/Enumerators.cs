using System;

namespace SystemsAnalysis.Types
{
  /// <summary>
  /// Non-creatable helper class that provides methods for generating 
  /// enumerators from strings.  Useful when converting a string value
  /// read from a database into an enumerated type.
  /// </summary>
  public class Enumerators
  {
    private Enumerators()
    {
    }

    /// <summary>
    /// Enumerator for valid inflow control types
    /// </summary>
    public enum InflowControlTypes
    {
      Parking,
      Roof,
      Street
    }

    /// <summary>
    /// Enumerator for valid LinkType values.
    /// </summary>
    public enum LinkTypes
    {
      C,
      D,
      S,
      I,
      UNKNOWN
    };

    /// <summary>
    /// Categories of pipes used for characterization reports.
    /// </summary>
    public enum PipeFlowTypes
    {
      S,
      D,
      C,
      F,
      I,
      U,
      UNKNOWN
    };

    /// <summary>
    /// Enumerator for commonly used zoning types.
    /// </summary>
    /// 
    public enum ZoningTypes
    {
      SFR,
      MFR,
      COM,
      IND,
      POS,
      ROW,
      OTH,
      UNK
    };

    /// <summary>
    /// The source of flow data used to generate modeled flows in FlowEstimationCatchments
    /// </summary>
    public enum FlowEstimationMethods
    {
      TM,
      PM,
      PS,
      ME,
      NS,
      Unknown
    };

    /// <summary>
    /// Represents the available model types in the Model Catalog. Currently,
    /// BAS = Basin Model and INT = Interceptor Model.  The Model Catalog uploader
    /// allows interceptor model results to overwrite basin model results, but not
    /// vice versa.
    /// </summary>
    public enum ModelCatalogTypes
    {
      INT,
      BAS
    };

    /// <summary>
    /// A hydrologic timeframe representing development assumptions embedded into 
    /// the master catchment data.
    /// </summary>
    public enum TimeFrames
    {
      EX,
      FU
    };

    /// <summary>
    /// Represents the connection status of a Dsc.
    /// </summary>
    public enum Sewerable
    {
      NoDetermination = 0,
      Unsewerable,
      Sewerable,
      Sewered,
      Septic
    };

    /// <summary>
    /// Valid operations for alternative operations
    /// </summary>
    public enum AlternativeOperation
    {
      UPD,
      DEL,
      ADD,
      SPL,
      CON,
      RIK
    };

    /// <summary>
    /// Indicates the future development state of a Dsc. Static properties are expected
    /// to remain in the same development state for future conditions.
    /// </summary>
    public enum DevelopmentState
    {
      Static,
      NewDevelopment,
      Redevelopment,
      Undevelopable,
      Unspecified
    }

    /// <summary>
    /// Indicates the future connection state of a Dsc.
    /// </summary>
    public enum ConnectionAssumption
    {
      WillNotConnect,
      ConnectedNoChange,
      ConnectedFutureRedevelopment,
      FutureNewConnection,
      Unspecified
    }

    public enum StreetTypeKind
    {
      Street,
      Arterial,
      MajorArterial,
      Freeway,
      None
    }

    /// <summary>
    /// Converts a 3-character zoning string to one of the enumerated ZoningTypes.
    /// </summary>
    /// <param name="zoningCode">3-character string specifying a Zoning</param>
    /// <returns>The ZoningTypes enumerated value corresponding to ZoningCode</returns>
    public static ZoningTypes GetZoningEnum(string zoningCode)
    {
      try
      {
        return (Types.Enumerators.ZoningTypes)Enum.Parse(
        typeof(Types.Enumerators.ZoningTypes),
        zoningCode, true);
      }
      catch
      {
        return ZoningTypes.UNK;
      }
    }
    /// <summary>
    /// Converts a 1- or 2-character Link Type string to one of the enumerated
    /// LinkTypes.
    /// </summary>
    /// <param name="LinkTypeCode">A 1- or 2- character string specifying a
    /// valid Link Type  </param>
    /// <returns>The LinkTypes enumerated value corresponding to LinkTypeCode</returns>
    public static LinkTypes GetLinkTypeEnum(string LinkTypeCode)
    {
      //HACK: DaveW wants interceptors counted separately from
      //other pipes.  This will not work is this class is used
      //in the future as a model-builder!
      if (LinkTypeCode.IndexOf("I") != -1)
      {
        return Types.Enumerators.LinkTypes.I;
      }
      else
        if (LinkTypeCode.IndexOf("C") != -1)
        {
          return Types.Enumerators.LinkTypes.C;
        }
        else
          if (LinkTypeCode.IndexOf("D") != -1)
          {
            return Types.Enumerators.LinkTypes.D;
          }
          else
            if (LinkTypeCode.IndexOf("S") != -1)
            {
              return Types.Enumerators.LinkTypes.S;
            }
            else
            {
              return Types.Enumerators.LinkTypes.UNKNOWN;
            }
      /*
            return (Types.Enumerators.LinkTypes)Enum.Parse(
            typeof(Types.Enumerators.LinkTypes),
            LinkTypeCode, true);
            */
    }
    /// <summary>
    /// Converts a 1-character Pipe Flow Type string to one of the enumerated
    /// PipeFlowTypes.
    /// </summary>
    /// <param name="LinkTypeCode">A 1-character string specifying a
    /// valid Pipe Flow Type  </param>
    /// <returns>The PipeFlowTypes enumerated value corresponding to PipeFlowTypeCode</returns>
    public static PipeFlowTypes GetPipeFlowTypeEnum(string PipeFlowTypeCode)
    {
      try
      {
        return (Types.Enumerators.PipeFlowTypes)Enum.Parse(
        typeof(Types.Enumerators.PipeFlowTypes),
        PipeFlowTypeCode, true);
      }
      catch
      {
        return PipeFlowTypes.UNKNOWN;
      }
    }
    /// <summary>
    /// Converts a 2-character TimeFrame string to one of the enumerated
    /// TimeFrames.
    /// </summary>
    /// <param name="TimeFrameCode">A valid timeframe string</param>
    /// <returns>The LinkTypes enumerated value corresponding to LinkTypeCode</returns>
    public static TimeFrames GetTimeFrameEnum(string TimeFrameCode)
    {
      return (Types.Enumerators.TimeFrames)Enum.Parse(
      typeof(Types.Enumerators.TimeFrames),
      TimeFrameCode, true);
    }

    /// <summary>
    /// Converts a 2-character FlowEstimationMethod string to one of the enumerated
    /// FlowEstimationMethods.
    /// </summary>
    /// <param name="FlowEstimationMethodCode">A valid FlowEstimationMethod string</param>
    /// <returns>The FlowEstimationMethods enumerated value corresponding to FlowEstimationMethodCode</returns>
    public static FlowEstimationMethods GetFlowEstimationMethodEnum(string FlowEstimationMethodCode)
    {
      try
      {
        return (Types.Enumerators.FlowEstimationMethods)Enum.Parse(
        typeof(Types.Enumerators.FlowEstimationMethods),
        FlowEstimationMethodCode, true);
      }
      catch
      {
        return FlowEstimationMethods.Unknown;
      }
    }

    public static AlternativeOperation GetAlternativeOperationEnum(string AlternativeOperationCode)
    {
      return (Types.Enumerators.AlternativeOperation)Enum.Parse(
      typeof(Types.Enumerators.AlternativeOperation),
      AlternativeOperationCode, true);
    }

    /// <summary>
    /// Converts a 3-character zoning string to one of the enumerated DevelopmentStates.
    /// </summary>
    /// <param name="devStateCode">A string representing a DevelopmentState</param>
    /// <returns>The DevelopmentState enumerated value corresponding to ZoningCode</returns>
    public static DevelopmentState GetDevelopmentStateEnum(string devStateCode)
    {
      try
      {
        return (Types.Enumerators.DevelopmentState)Enum.Parse(
        typeof(Types.Enumerators.DevelopmentState),
        devStateCode, true);
      }
      catch
      {
        return DevelopmentState.Unspecified;
      }
    }

    /// <summary>
    /// Converts a 3-character zoning string to one of the enumerated DevelopmentStates.
    /// </summary>
    /// <param name="devStateCode">A string representing a DevelopmentState</param>
    /// <returns>The DevelopmentState enumerated value corresponding to ZoningCode</returns>
    public static ConnectionAssumption GetConnectionAssumptionEnum(string connectionAssumptionCode)
    {
      try
      {
        return (Types.Enumerators.ConnectionAssumption)Enum.Parse(
        typeof(Types.Enumerators.ConnectionAssumption),
        connectionAssumptionCode, true);
      }
      catch
      {
        return ConnectionAssumption.Unspecified;
      }
    }
  }
}
