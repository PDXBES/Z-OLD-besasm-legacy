using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsAnalysis.Modeling
{
  /// <summary>
  /// Utility class for looking up values (similar to VLookup)
  /// </summary>
  /// <typeparam name="T">Type of key</typeparam>
  /// <typeparam name="U">Type of value</typeparam>
  public class LookupTable<T, U>
  {
    private SortedList<T, U> _table;

    public LookupTable(SortedList<T, U> table)
    {
      _table = table;
    }

    public U LookupValue(T indexValue, bool withCeiling, bool withFloor)
    {
      if (_table.Count == 0)
        throw new IndexOutOfRangeException("No items to lookup");

      List<T> elementDistTableKeys = new List<T>(_table.Keys);
      int lookupIndex = elementDistTableKeys.BinarySearch(indexValue);
      if (lookupIndex > 0)
        return _table.Values[lookupIndex];
      else
      {
        int nearestLookupIndex = ~lookupIndex;
        if ((nearestLookupIndex >= 0) && (nearestLookupIndex < _table.Count))
        {
          if (nearestLookupIndex > 0)
          {
            return _table.Values[nearestLookupIndex-1];
          }
          else if (withFloor)
          {
            throw new IndexOutOfRangeException(
              String.Format("Index {0} below lowest key value {1}", 
                indexValue, elementDistTableKeys[0]));
          }
          else
          {
            return _table.Values[0];
          }
        }
        else if (withCeiling)
        {
          throw new IndexOutOfRangeException(
            String.Format("Index {0} above highest key value {1}",
              indexValue, elementDistTableKeys[elementDistTableKeys.Count - 1]));
        }
        else
        {
          return _table.Values[_table.Count - 1];
        }
      }
    }

    public U LookupValue(T indexValue)
    {
      return LookupValue(indexValue, false, false);
    }

    public U LookupValueWithCeiling(T indexValue)
    {
      return LookupValue(indexValue, true, false);
    }

    public U LookupValueWithFloor(T indexValue)
    {
      return LookupValue(indexValue, false, true);
    }

    public U LookupValueWithinTable(T indexValue)
    {
      return LookupValue(indexValue, true, true);
    }
  }
}
