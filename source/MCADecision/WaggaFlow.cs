using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

namespace MCADecision
{
    /// <summary>
    /// Summary description for WaggaFlow.
    /// </summary>
    [Guid("cc2f456a-3f00-4e67-997a-25c62cc86aa6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MCADecision.WaggaFlow")]
    public sealed class WaggaFlow : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private const int GWL_HWNDPARENT = -8;
        [DllImport("user32")]
        private static extern int SetWindowLong(int hWnd, int nIndex, int dwNewLong);

        private IApplication m_application;
        public WaggaFlow()
        {

            base.m_category = "MCMATools";
            base.m_caption = "Commence to Fill";
            base.m_message = "Commence to Fill";
            base.m_toolTip = "Launch the Commence to Fill Tool";
            base.m_name = "MCMATools_WaggaFlow";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

            try
            {
                base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("MCADecision.Graphics.WaggaFlow.bmp"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            frmCommenceToFill frm = new frmCommenceToFill(m_application);
            SetWindowLong(frm.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd);
            frm.Show();
        }

        #endregion
    }
}
