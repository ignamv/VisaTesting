# Mocks and helpers to test Ivi Visa drivers

Example usage:

```C#
[TestMethod()]
public void getPositionTest()
{
	ExpectMessageBasedSession expect = new ExpectMessageBasedSession
	{
		SimulatedReads = new Queue<string>(new[] { "Executed! PIGetXYPosition. X= 3 Y= 4\n" }),
		ExpectedWrites = new Queue<string>(new[] { "PIGetXYPosition\n" }),
	};
	using (expect)
	{
		PilotDriver myDriver = new PilotDriver { Session = expect.session };
		PointF position = myDriver.getPosition();
		Assert.AreEqual(3, position.X);
		Assert.AreEqual(4, position.Y);
	}
}
```