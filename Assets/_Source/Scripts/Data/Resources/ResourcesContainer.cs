
public class ResourcesContainer
{
	private int _wheat = 5;
	private int _peasant = 2;
	private int _warriors = 0;

	public int Wheat { get => _wheat; set { _wheat = value; onWheatChanged?.Invoke(); } }
	public int Peasant { get => _peasant; set { _peasant = value; onPeasantChanged?.Invoke(); } }
	public int Warriors { get => _warriors; set { _warriors = value; onWarriorsChanged?.Invoke(); } }


	public int HiredWarriorsCount { get; set; }
	public int HiredPeasantCount { get; set; }


	public delegate void OnWheatChanged();
	public OnWheatChanged onWheatChanged;

	public delegate void OnPeasantChanged();
	public OnPeasantChanged onPeasantChanged;

	public delegate void OnWarriorsChanged();
	public OnWarriorsChanged onWarriorsChanged;
}