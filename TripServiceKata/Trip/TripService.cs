using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
	public class TripService
	{
		private ITripDao _tripDao;

		public TripService(ITripDao tripDao)
		{
			this._tripDao = tripDao;
		}

		public List<Trip> GetTripsByUser(User.User user, User.User loggedInUser)
		{
			if (loggedInUser == null)
			{
				throw new UserNotLoggedInException();
			}

			return user.IsFriendsWith(loggedInUser) ? FindTripsBy(user) : new List<Trip>();
		}

		private List<Trip> FindTripsBy(User.User user)
		{
			return _tripDao.TripsBy(user);
		}
	}
}