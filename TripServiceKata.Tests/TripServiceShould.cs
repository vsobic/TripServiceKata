using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
	[TestClass]
	public class TripServiceShould
	{
		private static User.User _loggedInUser;
		private TripService _tripService;
		private User.User Guest { get; } = null;
		private User.User AnotherUser { get; } = new User.User();
		private User.User RegisteredUser { get; } = new User.User();
		private Trip.Trip ToBrazil { get; } = new Trip.Trip();
		private Trip.Trip ToLondon { get; } = new Trip.Trip();

		[TestInitialize]
		public void TestInitialize()
		{
			_tripService = new TestableTripService();
			_loggedInUser = RegisteredUser;
		}


		[TestMethod]
		[ExpectedException(typeof (UserNotLoggedInException))]
		public void ValidateTheLoggedInUser()
		{
			_loggedInUser = Guest;

			_tripService.GetTripsByUser(AnotherUser);
		}

		[TestMethod]
		public void NotReturnAnyTripsWhenUsersAreNotFriends()
		{
			var stranger = new UserBuilder()
				.User()
				.FriendsWith(AnotherUser)
				.WithTrips(ToBrazil)
				.Build();

			var trips = _tripService.GetTripsByUser(stranger);

			Assert.AreEqual(0, trips.Count);
		}

		[TestMethod]
		public void ReturnFriendTripsWhenUsersAreFriends()
		{
			var friend = new UserBuilder()
				.User()
				.FriendsWith(AnotherUser, RegisteredUser)
				.WithTrips(ToBrazil, ToLondon)
				.Build();

			var trips = _tripService.GetTripsByUser(friend);

			Assert.AreEqual(2, trips.Count);
		}

		private class TestableTripService : TripService
		{
			protected override User.User GetLoggedInUser()
			{
				return _loggedInUser;
			}

			protected override List<Trip.Trip> FindTripsBy(User.User user)
			{
				return user.Trips();
			}
		}
	}
}