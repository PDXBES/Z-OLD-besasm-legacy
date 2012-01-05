using System;
using System.Collections;
using SystemsAnalysis.Utils.Events;


namespace SystemsAnalysis.ModelConstruction.AlternativesBuilder
{
	/// <summary>
	/// Summary description for IGISExecuter.
	/// </summary>
	public interface IGISExecuter
	{		
		void ExecuteLibrary(string libraryName, IDictionary parameterList);

		void ShowGIS();
		void HideGIS();

		void CloseGIS();

		string GetError();

		bool WaitingForGIS { get; }

		event OnStatusChangedEventHandler StatusChanged;		
		
	}
}
