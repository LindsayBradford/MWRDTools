using ESRI.ArcGIS.Framework;

namespace MWRDTools.View
{
  class ThreatenedSpeciesFormBuilder : AbstractFormBuilder {
    public static ThreatenedSpeciesForm build(IApplication appHook) {

      ThreatenedSpeciesForm form = new ThreatenedSpeciesForm(appHook);

      return form;
    }
  }
}
