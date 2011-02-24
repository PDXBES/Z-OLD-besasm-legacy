using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ReferenceLibrary
    {
    [Guid ( "234bee39-c7bf-4cc5-9bda-aae83cbc82d5" )]
    [ClassInterface ( ClassInterfaceType.None )]
    [ProgId ( "ReferenceLibrary.ArcConnectionLibrary" )]
    public class ArcConnectionLibrary
        {
        /// <summary>
        /// Creates the PropertySet for an SDE Connection using User/Password
        /// </summary>
        /// <param name="server">varies</param>
        /// <param name="instance">varies</param>
        /// <param name="database">varies</param>
        /// <param name="user">Provided UserName (NOT your login name)</param>
        /// <param name="password">Provided password</param>
        /// <param name="version">Typically SDE.DEFAULT</param>
        /// <returns>IPropertySet</returns>
        public ESRI.ArcGIS.esriSystem.IPropertySet create_ArcSDE_Connection ( string server,
                                                            string instance,
                                                            string database,
                                                            string user,
                                                            string password,
                                                            string version )
            {
            ESRI.ArcGIS.esriSystem.IPropertySet propertySet = new ESRI.ArcGIS.esriSystem.PropertySetClass ();

            propertySet.SetProperty ( "SERVER", server );
            propertySet.SetProperty ( "INSTANCE", instance );
            propertySet.SetProperty ( "DATABASE", database );
            propertySet.SetProperty ( "USER", user );
            propertySet.SetProperty ( "PASSWORD", password );
            propertySet.SetProperty ( "VERSION", version );

            return propertySet;
            }
        /// <summary>
        /// Creates the PropertySet for an SDE Connection using Windows Authentication
        /// </summary>
        /// <param name="server">varies</param>
        /// <param name="instance">varies</param>
        /// <param name="database">varies</param>
        /// <param name="version">Typically SDE.DEFAULT</param>
        /// <param name="authentication">"OSA" for Operating System Authentication (Windows)</param>
        /// <returns>IPropertySet</returns>
        public ESRI.ArcGIS.esriSystem.IPropertySet create_ArcSDE_Connection ( string server,
                                                            string instance,
                                                            string database,
                                                            string version,
                                                            string authentication )
            {
            ESRI.ArcGIS.esriSystem.IPropertySet propertySet = new ESRI.ArcGIS.esriSystem.PropertySetClass ();

            propertySet.SetProperty ( "SERVER", server );
            propertySet.SetProperty ( "INSTANCE", instance );
            propertySet.SetProperty ( "DATABASE", database );
            propertySet.SetProperty ( "VERSION", version );
            propertySet.SetProperty ( "AUTHENTICATION_MODE", authentication );

            return propertySet;
            }
        }
    }
