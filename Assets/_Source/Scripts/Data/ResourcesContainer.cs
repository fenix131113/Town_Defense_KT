
public class ResourcesContainer
{
    private int _wheat = 5;
    private int _peasant;
    private int _warriors;

    public int Wheat { get => _wheat; set { _wheat = value; onWheatChanged?.Invoke(); } }
    public int Peasant { get => _peasant; set { _peasant = value; onPeasantChanged?.Invoke(); } }
    public int Warriors { get => _warriors; set { _warriors = value; onWarriorsChanged?.Invoke(); } }

    public delegate void OnWheatChanged();
    public OnWheatChanged onWheatChanged;

    public delegate void OnPeasantChanged();
    public OnPeasantChanged onPeasantChanged;

    public delegate void OnWarriorsChanged();
    public OnWarriorsChanged onWarriorsChanged;
}
