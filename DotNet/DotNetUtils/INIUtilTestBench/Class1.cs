using System;

namespace INIUtilTestBench
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			SystemsAnalysis.Utils.INIFile myINI;
			myINI = new SystemsAnalysis.Utils.INIFile("Q:\\ESInterceptor\\ESInterceptor_EX_02\\Model.ini");
			System.Collections.Specialized.StringCollection ss;
			ss = myINI.GetINIKeys("rootlinks");
			foreach (string s in ss)
			{
				Console.WriteLine(s);
			}


			//
			// TODO: Add code to start application here
			//
		}
	}
}
