public class PeasantCountVisualizer : ResourcesVisualizer
{
    public override void Setup()
    {
        resources.onPeasantChanged += UpdateTextNow;
    }

    public override void UpdateTextNow()
    {
        textToWrite.text = textPrefix + resources.Peasant.ToString() + textSuffix;
    }
}
