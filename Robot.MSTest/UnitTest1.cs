using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Robot.MSTest
{
    [TestClass]
    public class RobotTest
    {
        private IBasicOperation robot;
        [TestInitialize]
        public void Initialize()
        {
            robot = new ChampionDataRobot();
        }

        [TestMethod]
        public void PlacementTest()
        {
            robot.Place(2, 3, DirectionTypes.NORTH);            
            var currentLocation = robot.GetCurrentLocation();
            Assert.AreEqual(currentLocation.Y, 3);
            Assert.AreEqual(currentLocation.X, 2);
            Assert.AreEqual(currentLocation.Face.Description, DirectionTypes.NORTH);
        }

        [TestMethod]
        public void MovementTest()
        {
            robot.Place(2, 3, DirectionTypes.NORTH);
            robot.Move();
            var currentLocation = robot.GetCurrentLocation();
            Assert.AreEqual(currentLocation.Y, 4);
        }

        [TestMethod]
        public void LeftTest()
        {
            robot.Place(2, 3, DirectionTypes.NORTH);
            robot.Left();
            var currentLocation = robot.GetCurrentLocation();
            Assert.AreEqual(currentLocation.Face.Description, DirectionTypes.WEST);
        }

        [TestMethod]
        public void RightTest()
        {
            robot.Place(2, 3, DirectionTypes.NORTH);
            robot.Right();
            var currentLocation = robot.GetCurrentLocation();
            Assert.AreEqual(currentLocation.Face.Description, DirectionTypes.EAST);
        }
    }
}
