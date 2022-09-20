using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    public interface IRobot
    { 

    }
    public interface IBasicOperation
    {
        RobotLocation Move();
        RobotLocation Place(int x, int y, string face);
        bool IsValidLocation(RobotLocation value);
        RobotLocation Left();
        RobotLocation Right();
        string Report();
        RobotLocation GetCurrentLocation();
    }
}
