using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VritualRain
{
    /// <summary>
    // Class used to store layer name and file attributes
    /// </summary>
    class layerInfo
    {
        public string layerName;
        public long layerAttr;
        public override string ToString() { return layerName; }
    }
}
