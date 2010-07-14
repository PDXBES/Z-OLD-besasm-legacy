using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsAnalysis.Modeling
{
  public enum PipeConstructionType { CSP, HDPE, CIPP, PipeBurst };


  public class PipeConstruction
  {
    private PipeConstructionType _ConstructionType;

    public PipeConstruction()
    {
      _ConstructionType = PipeConstructionType.CSP;
    }

    public PipeConstruction(Link link)
    {
      string linkMaterial = link.Material.ToUpper();

      switch (linkMaterial)
      {
        case "HDPE":
        case "PVC":
          _ConstructionType = PipeConstructionType.HDPE;
          break;
        case "CIPP":
          _ConstructionType = PipeConstructionType.CIPP;
          break;
        case "PIPEBURST":
          _ConstructionType = PipeConstructionType.PipeBurst;
          break;
        default:
          _ConstructionType = PipeConstructionType.CSP;
          break;
      }
    }

    public override string ToString()
    {
      switch (_ConstructionType)
      {
        case PipeConstructionType.HDPE:
          return "HDPE";
        case PipeConstructionType.CSP:
          return "CSP";
        case PipeConstructionType.CIPP:
          return "CIPP";
        case PipeConstructionType.PipeBurst:
          return "PipeBurst";
        default:
          return "CSP";
      }
    }
  }
}
