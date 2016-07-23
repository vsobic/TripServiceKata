using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
	public class TripService
	{
		private readonly ITripDao _tripDao;

		public TripService(ITripDao tripDao)
		{
			_tripDao = tripDao;
		}

		public List<Trip> GetTripsByUser(User.User user, User.User loggedInUser)
		{
			Validate(loggedInUser);

			return user.IsFriendsWith(loggedInUser)
				? FindTripsBy(user)
				: NoTrips();
		}

		private static List<Trip> NoTrips()
		{
			return new List<Trip>();
		}

		private static void Validate(User.User loggedInUser)
		{
			if (loggedInUser == null)
			{
				throw new UserNotLoggedInException();
			}
		}

		private List<Trip> FindTripsBy(User.User user)
		{
			return _tripDao.TripsBy(user);
		}
	}
}