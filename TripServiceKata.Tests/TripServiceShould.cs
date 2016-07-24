using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
	[TestClass]
	public class TripServiceShould
	{
		private Mock<ITripDao> _tripDao;
		private TripService _tripService;
		private User.User Guest { get; } = null;
		private User.User AnotherUser { get; } = new User.User();
		private User.User RegisteredUser { get; } = new User.User();
		private Trip.Trip ToBrazil { get; } = new Trip.Trip();
		private Trip.Trip ToLondon { get; } = new Trip.Trip();

		[TestInitialize]
		public void TestInitialize()
		{
			_tripDao = new Mock<ITripDao>();
			_tripService = new TripService(_tripDao.Object);
		}


		[TestMethod]
		[ExpectedException(typeof (UserNotLoggedInException))]
		public void ValidateTheLoggedInUser()
		{
			_tripDao = new Mock<ITripDao>();
			_tripService.GetTripsByUser(AnotherUser, Guest);
		}

		[TestMethod]
		public void NotReturnAnyTripsWhenUsersAreNotFriends()
		{
			var stranger = new UserBuilder()
				.User()
				.FriendsWith(AnotherUser)
				.WithTrips(ToBrazil)
				.Build();

			var trips = _tripService.GetTripsByUser(stranger, RegisteredUser);

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
			_tripDao.Setup(t => t.TripsBy(friend)).Returns(friend.Trips);

			var trips = _tripService.GetTripsByUser(friend, RegisteredUser);

			Assert.AreEqual(2, trips.Count);
		}
	}
}