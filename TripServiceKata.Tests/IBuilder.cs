namespace TripServiceKata.Tests
{
	public interface IBuilder<out T>
	{
		T Build();
	}
}