using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using SystemsAnalysis.Utils.Events;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Reporting.ReportLibraries;

namespace SystemsAnalysis.Reporting.ReportLibraries
{
    /// <summary>
    /// Abstract class that all report classes must inherit from
    /// </summary>
    public abstract class ReportBase
    {
        /// <summary>
        /// A light-weight subclass that allows a class inheriting from ReportBase to 
        /// share report metadata without requiring calling the class constructor. Often
        /// times it is necessary to retrieve report metadata, but without the overhead of
        /// constructing the report.  This class provides that funciontality.
        /// </summary>
        public class ReportInfo
        {
            private string reportName;
            public ReportInfo(string reportName)
            {
                this.reportName = reportName; // this.GetType().DeclaringType.Name;
            }
            public string ReportName
            {
                get
                {
                    return reportName;
                }
            }
            public virtual Dictionary<string, string> AuxilaryDataDescription
            {
                get
                {
                    throw new Exception("This report does not use auxilary data");
                }
            }
            public virtual bool RequiresAuxilaryData
            {
                get
                {
                    return false;
                }
            }
            public virtual Dictionary<string, Parameter> AuxilaryData
            {
                get
                {
                    throw new Exception("This report does not use auxilary data");
                }
                set
                {
                    throw new Exception("This report does not use auxilary data");
                }
            }
        }
        /// <summary>
        /// Loads auxilary data into a ReportBase.  This class must be overridden by
        /// any reports that require auxilary data.
        /// </summary>
        /// <param name="AuxilaryData"></param>
        public virtual void LoadAuxilaryData(Dictionary<string, Parameter> AuxilaryData)
        {
            throw new Exception("This report does not use auxilary data");
        }

        /// <summary>
        /// Calls a function based on a string representation of the function's name. The function must
        /// be available a class derived from this class, e.g. ModelReport.
        /// </summary>
        /// <param name="functionName">The name of a function to execute.</param>
        /// <param name="parameters">A collection of parameters required to execute the specified function.</param>
        /// <returns>The return value of functionName as formatted string.</returns>        
        public string EvaluateFunction(string functionName, IDictionary<string, Parameter> parameters, string formatString)
        {
            /*  this function works by enumerating all
            functions in a ReportBase instance and creating a delegate to execute the named function
            if found. */
            System.Reflection.MethodInfo m;

            m = this.GetType().GetMethod(functionName);
            if (m == null) { return "Function '" + functionName + "'  not found"; }
            IFormattable formattable;
            object[] o = new object[parameters.Count];
            int i = 0;

            switch (m.ReturnParameter.ParameterType.Name.ToUpper())
            {
                case "STRING":
                    EvaluateStringFunctionDelegate stringDelegate = null;
                    stringDelegate = (EvaluateStringFunctionDelegate)Delegate.CreateDelegate(typeof(EvaluateStringFunctionDelegate), this, m);
                    return stringDelegate(parameters).ToString();
                case "INT32":
                case "INT":
                    EvaluateIntFunctionDelegate intDelegate = null;
                    intDelegate = (EvaluateIntFunctionDelegate)Delegate.CreateDelegate(typeof(EvaluateIntFunctionDelegate), this, m);
                    formattable = (IFormattable)intDelegate(parameters);
                    return formattable.ToString(formatString, null);
                case "DOUBLE":
                    EvaluateDoubleFunctionDelegate doubleDelegate = null;
                    doubleDelegate = (EvaluateDoubleFunctionDelegate)Delegate.CreateDelegate(typeof(EvaluateDoubleFunctionDelegate), this, m);
                    formattable = (IFormattable)doubleDelegate(parameters);
                    return formattable.ToString(formatString, null);
                default:
                    return "Unknown return type";
            }
        }

        /// <summary>
        /// Delegate signature for functions executed via HandleFunction
        /// </summary>
        /// <param name="parameters">A collection of parameters required to execute the specified function.</param>
        /// <returns>The return value of functionName as formatted string.</returns>
        public delegate int EvaluateIntFunctionDelegate(IDictionary<string, Parameter> parameters);
        public delegate string EvaluateStringFunctionDelegate(IDictionary<string, Parameter> parameters);
        public delegate double EvaluateDoubleFunctionDelegate(IDictionary<string, Parameter> parameters);


        /// <summary>
        /// Event that occurs when a Report's status has changed.
        /// </summary>
        public event OnStatusChangedEventHandler StatusChanged;

        /// <summary>
        /// Internally called function that fires the OnStatusChanged event.
        /// </summary>
        /// <param name="e">Parameter that contains the string describing the new state.</param>
        public virtual void OnStatusChanged(StatusChangedArgs e)
        {
            if (StatusChanged != null)
                StatusChanged(e);
        }

        /// <summary>
        /// Contains parameters read from report xml file and accessor methods
        /// for retrieving parameters as various datatypes.
        /// </summary>
        public class Parameter
        {
            string name;
            string value;

            public Parameter(string name, string value)
            {
                this.name = name;
                this.value = value;
            }
            public string Name
            {
                get { return name; }
            }
            public string Value
            {
                get { return value; }
            }
            public bool ValueAsBool
            {
                get { return value == "true"; }
            }
            public int ValueAsInt
            {
                get
                {
                    if (value.ToUpper() == "MAX")
                    {
                        return Int32.MaxValue;
                    }
                    if (value.ToLower() == "MIN")
                    {
                        return Int32.MinValue;
                    }
                    return Convert.ToInt32(value);
                }
            }
            public double ValueAsDouble
            {
                get
                {
                    if (value.ToUpper() == "MAX")
                    {
                        return Double.MaxValue;
                    }
                    if (value.ToUpper() == "MIN")
                    {
                        return Double.MinValue;
                    }
                    return Convert.ToDouble(value);
                }
            }
        }

    }
}
