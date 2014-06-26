/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Framework;

namespace MWRDTools.View
{
  /// <summary>
  /// A abstract extension of ESRI.ArcGIS.Desktop.AddIns.Button that
  /// deals with ensuring that any forms launchhed via this type of button
  /// will conrrectly deal with the ArcMap desktop as its parent window.
  /// </summary>
  public abstract class AbstractFormLaunchButton : ESRI.ArcGIS.Desktop.AddIns.Button
  {
    private IWin32Window parentWindow;
    protected Form form;

    public AbstractFormLaunchButton() {
      parentWindow =  new ArcMapFormWrapper(
        getAppHook().hWnd
      );
    }

    /// <summary>
    /// A WIn32 HWND handle to the parent-form of this button.
    /// </summary>
    protected IWin32Window getParentWindow() {
      return parentWindow;
    }

    /// <summary>
    /// Returns the hook of this button as an instance of ESRI.ArcGIS.Framework.IApplication.
    /// </summary>
    protected IApplication getAppHook() {
      return this.Hook as IApplication;
    }

    protected override void OnClick() {
      form.Show(
        getParentWindow()
      );
    }
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

