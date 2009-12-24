using System;
using System.Collections;
using SystemsAnalysis.Utils.Events;
using System.Collections.Generic;


namespace SystemsAnalysis.ModelConstruction.AlternativesToolkit
{
	/// <summary>
	/// Summary description for IGISExecuter.
	/// </summary>
	public interface IGISExecuter
	{		
		void ExecuteLibrary(string libraryName, IDictionary parameterList);
        void ExecuteFunctionGroup(string execGroup, string baseModel, string alternativeName, string outputModel, bool interactive);
        void ExecuteFunctionGroup(string execGroup, Dictionary<string, string> parameters);

		void ShowGIS();
		void HideGIS();

		void CloseGIS();

		string GetError();

		bool WaitingForGIS { get; }

		event OnStatusChangedEventHandler StatusChanged;		
		
	}
}
