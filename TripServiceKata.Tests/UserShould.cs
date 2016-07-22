using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TripServiceKata.Tests
{
	[TestClass]
	public class UserShould
	{
		private User.User Paul { get; } = new User.User();

		private User.User Bob { get; } = new User.User();

		[TestMethod]
		public void InformWhenUsersAreNotFriends()
		{
			var user = new UserBuilder()
				.User()
				.FriendsWith(Paul)
				.Build();

			Assert.IsFalse(user.IsFriendsWith(Bob));
		}

		[TestMethod]
		public void InformWhenUsersAreFriends()
		{
			var user = new UserBuilder()
				.User()
				.FriendsWith(Bob)
				.Build();

			Assert.IsTrue(user.IsFriendsWith(Bob));
		}
	}
}