using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using System;
using System.Runtime.InteropServices;
namespace SystemsAnalysis.Tracer
{
	
	[Guid("a91387e9-2cc6-4af7-8a9b-d2264333d96a")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("FeatureClassTracer.TracerSettings")]
	public class TracerSettings : IComPropertyPage
	{
		public TracerSettings()
		{
		}

	
		#region IComPropertyPage Implementations
		public bool IsPageDirty
		{
			get
			{
				// TODO:	Add TracerSettings.IsPageDirty getter implementation
				return true;
			}
		}

		public void Cancel()
		{
			// TODO:	Add TracerSettings.Cancel implementation
		}

		public int get_HelpContextID(int controlID)
		{
			// TODO:	Add TracerSettings.get_HelpContextID implementation
			return 0;
		}

		public string Title
		{
			get
			{
				// TODO:	Add TracerSettings.Title getter implementation
				return null;
			}
			set
			{
				// TODO:	Add TracerSettings.Title setter implementation
			}
		}

		public void SetObjects(ISet objects)
		{
			// TODO:	Add TracerSettings.SetObjects implementation
		}

		public int Width
		{
			get
			{
				// TODO:	Add TracerSettings.Width getter implementation
				return 0;
			}
		}

		public int Priority
		{
			get
			{
				// TODO:	Add TracerSettings.Priority getter implementation
				return 0;
			}
			set
			{
				// TODO:	Add TracerSettings.Priority setter implementation
			}
		}

		public void Apply()
		{
			// TODO:	Add TracerSettings.Apply implementation
		}

		public IComPropertyPageSite PageSite
		{
			set
			{
				// TODO:	Add TracerSettings.PageSite setter implementation
			}
		}

		public void Deactivate()
		{
			// TODO:	Add TracerSettings.Deactivate implementation
		}

		public int Height
		{
			get
			{
				// TODO:	Add TracerSettings.Height getter implementation
				return 0;
			}
		}

		public void Show()
		{
			// TODO:	Add TracerSettings.Show implementation
		}

		public string HelpFile
		{
			get
			{
				// TODO:	Add TracerSettings.HelpFile getter implementation
				return null;
			}
		}

		public int Activate()
		{
			// TODO:	Add TracerSettings.Activate implementation
			return 0;
		}

		public bool Applies(ISet objects)
		{
			// TODO:	Add TracerSettings.Applies implementation
			return true;
		}

		public void Hide()
		{
			// TODO:	Add TracerSettings.Hide implementation
		}
		#endregion

	}
}
