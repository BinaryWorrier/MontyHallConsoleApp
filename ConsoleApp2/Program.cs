using System;

public enum Doors
{
    Door1,
    Door2,
    Door3,
};

public class Program
{
    public static void Main(string[] args)
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");


        int iterations;
        bool stick;
        if (args.Length == 2)
        {
            iterations = int.Parse(args[0]);
            stick = bool.Parse(args[1]);
        }
        else
        {
            iterations = 1000;
            stick = true;
        }

        RunSet(iterations, stick);
        RunSet(1000000, false);
        RunSet(1000000, true);
        RunSet(1000000, false);
        RunSet(1000000, true);
        RunSet(1000000, false);
        RunSet(1000000, true);
        RunSet(1000000, false);
        RunSet(1000000, true);
        RunSet(1000000, false);
        RunSet(1000000, true);
        RunSet(1000000, false);
        RunSet(1000000, true);

    }

    private static void RunSet(int iterations, bool stick)
    {
        var game = new Game();
        game.Run(iterations, stick);

        Console.Write($"Stick {(stick ? "Yes" : "No ")} ");
        Console.Write($"Count {game.Count:#,###,##0} ");
        Console.Write($"Win {game.Won:#,###,##0} ");
        Console.WriteLine($"Lost {game.Lost:#,###,##0} ");
        Console.WriteLine();
    }
}

public class Game
{
    private readonly Random rnd = new (DateTime.Now.Ticks.GetHashCode());

    public int Count { get; private set; }
    
    public int Won { get; private set; }

    public int Lost { get; private set;  }

    public void Run(int iterations, bool stick)
    {
        for(int i = 0; i < iterations; i++)
        {
            Count++;
            if(IsWinningGame(stick))
            {
                Won++;
            }
            else
            {
                Lost++;
            }
        }
    }

    public bool IsWinningGame(bool stick)
    {
        var allDoors = new List<Doors> { Doors.Door1, Doors.Door2, Doors.Door3 };

        var winningDoor = allDoors[rnd.Next(3)];

        var guess = allDoors[rnd.Next(3)];

        if(!stick)
        {
            var doorsMontyCanOpen = allDoors.ToList();
            doorsMontyCanOpen.Remove(winningDoor);
            doorsMontyCanOpen.Remove(guess);

            var montyOpensDoor = doorsMontyCanOpen.First();

            var otherDoor = allDoors.Except(new[] { montyOpensDoor, guess }).First();

            guess = otherDoor;
        }

        return guess == winningDoor;
    }
}