namespace MWRDTools.View
{
  class ThreatenedSpeciesButton : AbstractFormLaunchButton
  {
    public ThreatenedSpeciesButton() : base() {
      form = ThreatenedSpeciesFormBuilder.build(getAppHook());
    }
  }
}
