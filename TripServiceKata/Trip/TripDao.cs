using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
	public interface ITripDao
	{
		List<Trip> TripsBy(User.User user);
	}

	public class TripDao : ITripDao
	{
		public static List<Trip> FindTripsByUser(User.User user)
		{
			throw new DependendClassCallDuringUnitTestException(
				"TripDao should not be invoked on an unit test.");
		}

		public List<Trip> TripsBy(User.User user)
		{
			return TripDao.FindTripsByUser(user);
		}
	}
}