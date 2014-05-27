namespace MWRDTools.View
{
  class AdministrationButton : AbstractFormLaunchButton
  {
    public AdministrationButton() : base() {
      form = AdministrationFormBuilder.build(getAppHook());
    }
  }
}
