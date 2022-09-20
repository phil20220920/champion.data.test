using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Robot
{
    public abstract class Robot : IRobot
    {
        public RobotLocation CurrentLocation { get; set; }
        protected int Max_Location_X = 4;
        protected int Max_Location_Y = 4;
    }

    public class ChampionDataRobot : Robot, IBasicOperation
    {
        public bool IsValidLocation(RobotLocation value)
        {
            if (value.X > Max_Location_X
                || value.Y > Max_Location_Y
                || value.X < 0
                || value.Y < 0)
            {
                return false;
            }

            return true;
        }

        private RobotLocation GetDirectionByType(DirectionChangeType type)
        {
            switch (type)
            {
                case DirectionChangeType.LEFT:
                    CurrentLocation.Face.ChangeDirection(-1);
                    break;
                case DirectionChangeType.RIGHT:
                    CurrentLocation.Face.ChangeDirection(1);
                    break;
            }

            return CurrentLocation;
        }

        public RobotLocation Left()
        {
            return GetDirectionByType(DirectionChangeType.LEFT);
        }

        public RobotLocation Move()
        {
            var cur = new RobotLocation()
            {
                X = CurrentLocation.X,
                Y = CurrentLocation.Y,
                Face = CurrentLocation.Face
            };

            switch (CurrentLocation.Face.Description)
            {
                case DirectionTypes.NORTH:
                    cur.Y = cur.Y + 1;
                    break;
                case DirectionTypes.EAST:
                    cur.X = cur.X + 1;
                    break;
                case DirectionTypes.SOUTH:
                    cur.Y = cur.Y - 1;
                    break;
                case DirectionTypes.WEST:
                    cur.X = cur.X - 1;
                    break;
            }

            if (IsValidLocation(cur))
            {
                CurrentLocation = cur;
            }
            else
            {
                throw new InvalidDataException("Invalid move");
            }

            return CurrentLocation;
        }

        public RobotLocation Place(int x, int y, string face)
        {
            return CurrentLocation = new RobotLocation()
            {
                X = x,
                Y = y,
                Face = new RobotDirection(face.Trim().ToUpper())
            };
        }

        public string Report()
        {
            StringBuilder str = new StringBuilder();
            str.Append($"{CurrentLocation.X},");
            str.Append($"{CurrentLocation.Y},");
            str.Append($"{CurrentLocation.Face.Description}");
            return str.ToString();
        }

        public RobotLocation Right()
        {
            return GetDirectionByType(DirectionChangeType.RIGHT);
        }

        public RobotLocation GetCurrentLocation()
        {
            return CurrentLocation;
        }
    }

    public class RobotDirection
    {
        private string description = string.Empty;
        private int value = 1;

        public RobotDirection(string description)
        {
            this.Description = description;
            this.Value = GetRightValue(description);
        }

        //public static RobotDirection LEFT = new RobotDirection(DirectionTypes.NORTH);
        //public static RobotDirection EAST = new RobotDirection(DirectionTypes.EAST);
        //public static RobotDirection SOUTH = new RobotDirection(DirectionTypes.SOUTH);
        //public static RobotDirection WEST = new RobotDirection(DirectionTypes.WEST);

        public string Description { get => description; set => description = value; }
        public int Value { get => value; set => this.value = value; }

        public void VerifyData(RobotDirection current)
        {
            if (current.Value < 1)
            {
                current.Value = 4;
            }
            else if (current.Value > 4)
            {
                current.Value = 1;
            }

            current.Description = GetRightDescription(current.Value);
        }

        private string GetRightDescription(int value)
        {
            switch (value)
            {
                case 1:
                    return DirectionTypes.NORTH;
                case 2:
                    return DirectionTypes.EAST;
                case 3:
                    return DirectionTypes.SOUTH;
                case 4:
                    return DirectionTypes.WEST;
                default:
                    throw new InvalidDataException("out of range of direction value");
            }
        }

        private int GetRightValue(string description)
        {
            switch (description)
            {
                case DirectionTypes.NORTH:
                    return 1;
                case DirectionTypes.EAST:
                    return 2;
                case DirectionTypes.SOUTH:
                    return 3;
                case DirectionTypes.WEST:
                    return 4;
                default:
                    throw new InvalidDataException("out of range of direction value");
            }
        }

        public void ChangeDirection(int offset)
        {
            this.Value += offset;
            VerifyData(this);
        }
    }

}
