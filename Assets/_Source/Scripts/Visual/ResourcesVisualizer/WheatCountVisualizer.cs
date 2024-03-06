public class WheatCountVisualizer : ResourcesVisualizer
{
    public override void Setup()
    {
        resources.onWheatChanged += UpdateTextNow;
    }
    public override void UpdateTextNow()
    {
        textToWrite.text = textPrefix + resources.Wheat.ToString() + textSuffix;
    }
}
