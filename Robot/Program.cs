using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Robot
{
    class Program
    {
        private static List<string> ReadCommandsFromFile(string fileName)
        {
            return File.ReadAllLines(fileName).ToList();
        }

        private static void ShowUserGuide()
        {
            Console.WriteLine("- file : read commands from file 'command.txt'");
            Console.WriteLine("- exit : exit the app");
            Console.WriteLine("- move : move robot to current direction to 1 unit");
            Console.WriteLine("- place : place robot to a location with direction");
            Console.WriteLine("  ex: place,1,0,north");
            Console.WriteLine("- left : turn robot to the left");
            Console.WriteLine("- right : turn robot to the right");
            Console.WriteLine("- report : print out current robot's location and direction");
            Console.WriteLine("> command: ");
        }

        private static void ExecuteCommand(IBasicOperation robot, string input)
        {
            var commands = input.Split(',').ToList();
            if (string.IsNullOrEmpty(input))
            {
                throw new InvalidDataException("invalid empty command");                
            }

            var command = commands.Count > 0 ? commands[0] : "";
            var x = commands.Count > 1 ? commands[1] : "";
            var y = commands.Count > 2 ? commands[2] : "";
            var face = commands.Count > 3 ? commands[3] : "";

            try
            {
                switch (command.ToLower())
                {
                    case "place":                        
                        robot.Place(int.Parse(x), int.Parse(y), face.ToUpper());
                        break;
                    case "move":
                        robot.Move();
                        break;
                    case "left":
                        robot.Left();
                        break;
                    case "right":
                        robot.Right();
                        break;
                    case "report":
                        Console.WriteLine(robot.Report());
                        break;
                    default:
                        Console.WriteLine("no command found");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void Main()
        {
            ShowUserGuide();
            var robot = new ChampionDataRobot();
            robot.Place(0, 0, DirectionTypes.SOUTH);

            while (true)
            {                
                var input = Console.ReadLine();
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else if (input.Equals("file", StringComparison.OrdinalIgnoreCase))
                {
                    ReadCommandsFromFile("./commands.txt").ForEach(command => {
                        Console.WriteLine(command);
                        ExecuteCommand(robot, command);
                    });
                }
                else
                {
                    ExecuteCommand(robot, input);
                }                
            }                
        }
    }
}
