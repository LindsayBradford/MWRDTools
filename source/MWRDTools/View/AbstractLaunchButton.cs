using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.Framework;

namespace MWRDTools
{
  /// <summary>
  /// A abstract extension of ESRI.ArcGIS.Desktop.AddIns.Button that
  /// deals with ensuring that any forms launchhed via this type of button
  /// will conrrectly deal with the ArcMap desktop as its parent window.
  /// </summary>
  public abstract class AbstractFormLaunchButton : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    private IWin32Window _parentWindow;

    public AbstractFormLaunchButton()
    {
      _parentWindow =  new ArcMapFormWrapper(
        getAppHook().hWnd
      );
    }

    /// <summary>
    /// A WIn32 HWND handle to the parent-form of this button.
    /// </summary>
    protected IWin32Window getParentWindow()
    {
      return _parentWindow;
    }

    /// <summary>
    /// Returns the hook of this button as an instance of ESRI.ArcGIS.Framework.IApplication.
    /// </summary>
    protected IApplication getAppHook()
    {
      return (IApplication) this.Hook;
    }

    abstract protected override void OnClick();
  }
  /// <summary>
  ///  Wrapper class for ArcMap Desktop Window handle that makes it appear as a
  ///  Win32Window, allowing other forms to use the handle as a parent form.
  ///  For details see: http://forums.esri.com/Thread.asp?c=93&f=993&t=188141#556991
  /// </summary>
  class ArcMapFormWrapper : IWin32Window
  {
    private System.IntPtr _windowHandle;

    public System.IntPtr Handle
    {
      get { return _windowHandle; }
    }

    public ArcMapFormWrapper(int Handle)
    {
      _windowHandle = new IntPtr(Handle);
    }
  }

}

