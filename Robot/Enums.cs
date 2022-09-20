using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    public static class DirectionTypes
    {
        public const string NORTH = "NORTH";
        public const string SOUTH = "SOUTH";
        public const string EAST = "EAST";
        public const string WEST = "WEST";
    }
    public enum DirectionChangeType
    {
        LEFT,
        RIGHT,
    }   
    public class RobotLocation
    {
        public int X { get; set; }
        public int Y { get; set; }
        public RobotDirection Face { get; set; } = new RobotDirection(DirectionTypes.NORTH);
    }
}
