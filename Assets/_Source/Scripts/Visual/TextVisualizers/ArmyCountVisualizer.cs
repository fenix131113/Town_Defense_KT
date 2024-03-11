public class ArmyCountVisualizer : ResourcesVisualizer
{
    public override void Setup()
    {
        resources.onWarriorsChanged += UpdateTextNow;
    }
    public override void UpdateTextNow()
    {
        textToWrite.text = textPrefix + resources.Warriors.ToString() + textSuffix;
    }
}