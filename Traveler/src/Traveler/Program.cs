using System;
using System.IO;
using System.Linq;

namespace Traveler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var result = TravelParser.Run(File.ReadAllText("D:\\GIT\\Homework-master\\Homework-master\\Traveler\\data\\input.txt"));//args.First()));
            foreach (var item in result)
            {
                Console.WriteLine("X={0} Y={1} D={2}", item.x, item.y, item.direction);
            }
        }
    }
}
