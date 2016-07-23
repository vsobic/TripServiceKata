using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
	[TestClass]
	public class TripDaoShould
	{
		[TestMethod]
		[ExpectedException(typeof(DependendClassCallDuringUnitTestException))]
		public void ThrowAnExceptionWhenAnyTripsAreReturned()
		{
			new TripDao().TripsBy(new User.User());
		}
	}
}