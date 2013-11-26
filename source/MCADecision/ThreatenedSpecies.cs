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
    /// Summary description for ThreatenedSpecies.
    /// </summary>
    [Guid("bc73a9cd-cc7f-4297-8ef2-5f5b710471cf")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MCADecision.ThreatenedSpecies")]
    public sealed class ThreatenedSpecies : BaseCommand
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
        public ThreatenedSpecies()
        {
            base.m_category = "MCMATools";
            base.m_caption = "Threatened Species";
            base.m_message = "Threatened Species";
            base.m_toolTip = "Threatened Species";
            base.m_name = "MCADecision_ThreatenedSpecies";

            try
            {
                base.m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("MCADecision.Graphics.frog.bmp"));
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

        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            frmThreatenedSpecies frm = new frmThreatenedSpecies(m_application);
            SetWindowLong(frm.Handle.ToInt32(), GWL_HWNDPARENT, m_application.hWnd);
            frm.Show();
        }

        #endregion
    }
}
