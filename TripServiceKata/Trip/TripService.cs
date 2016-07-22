﻿using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
	public class TripService
	{
		public List<Trip> GetTripsByUser(User.User user)
		{
			List<Trip> tripList = new List<Trip>();
			User.User loggedUser = GetLoggedInUser();
			bool isFriend = false;
			if (loggedUser != null)
			{
				foreach (User.User friend in user.GetFriends())
				{
					if (friend.Equals(loggedUser))
					{
						isFriend = true;
						break;
					}
				}
				if (isFriend)
				{
					tripList = FindTripsBy(user);
				}
				return tripList;
			}
			else
			{
				throw new UserNotLoggedInException();
			}
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