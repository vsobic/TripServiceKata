using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
	public class TripService
	{
		public List<Trip> GetTripsByUser(User.User user)
		{
			if (GetLoggedInUser() == null)
			{
				throw new UserNotLoggedInException();
			}

			return user.IsFriendsWith(GetLoggedInUser()) ? FindTripsBy(user) : new List<Trip>();
		}

		protected virtual List<Trip> FindTripsBy(User.User user)
		{
			return TripDao.FindTripsByUser(user);
		}

		protected virtual User.User GetLoggedInUser()
		{
			return UserSession.GetInstance().GetLoggedUser();
		}
	}
}