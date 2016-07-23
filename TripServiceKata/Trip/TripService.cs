using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
	public class TripService
	{
		public List<Trip> GetTripsByUser(User.User user, User.User loggedInUser)
		{
			if (loggedInUser == null)
			{
				throw new UserNotLoggedInException();
			}

			return user.IsFriendsWith(loggedInUser) ? FindTripsBy(user) : new List<Trip>();
		}

		protected virtual List<Trip> FindTripsBy(User.User user)
		{
			return TripDao.FindTripsByUser(user);
		}
	}
}