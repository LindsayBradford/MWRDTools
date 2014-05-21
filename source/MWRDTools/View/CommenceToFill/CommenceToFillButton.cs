namespace MWRDTools.View
{
  class CommenceToFillButton : AbstractFormLaunchButton
  {
    public CommenceToFillButton() : base() {
      form = CommenceToFillFormBuilder.build(getAppHook());
    }
  }
}
