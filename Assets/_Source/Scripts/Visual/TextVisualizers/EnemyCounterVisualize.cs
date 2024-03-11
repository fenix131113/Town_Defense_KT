public class EnemyCounterVisualize : ResourcesVisualizer
{
    public override void Setup()
    {
        waves.onCurrentEnemyCountChanged += UpdateTextNow;
    }
    public override void UpdateTextNow()
    {
        textToWrite.text = textPrefix + waves.CurrentEnemyCount.ToString() + textSuffix;
    }
}
