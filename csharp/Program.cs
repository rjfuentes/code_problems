// rjfuentes 2023.09.30 

using System;
using System.Collections.Generic;

/*
not sure that the ! at the end of the Console.ReadLine() method is a good idea,
but it was a suggested fix to the nullable warning, CS8600
*/

class Program
{
    static void Main()
    {
        string repeatProgram;
        do
        {
            Console.Clear();
            Console.WriteLine($"Print A Shape With Text{Environment.NewLine}");
            string shape = UserInput.GetShape();
            int height = UserInput.GetHeight(shape);
            char asciiChar = DrawShape.Character();

            string shapeOutput = DrawShape.Draw(shape, asciiChar, height);
            Console.WriteLine($"Here's your {height} row {shape}:{Environment.NewLine}");
            Console.WriteLine(shapeOutput);

            Console.WriteLine("Enter a Y to re-run this shape printing program. Press \"Enter\" key in order to exit.");
            repeatProgram = Console.ReadLine()?.ToUpper().Trim()!;
        }
        while (repeatProgram == "Y");
        Console.WriteLine("Goodbye!");
    }
}


class UserInput
{
    public static string GetShape()
    {
        List<string> shapes = new List<string> { "triangle", "square", "diamond" };

        while (true)
        {
            Console.Write("Enter the shape you would like to print: ");
            string inputShape = Console.ReadLine()?.ToLower().Trim()!;

            if (shapes.Contains(inputShape))
            {
                return inputShape;
            }

            Console.WriteLine($"I can only understand these {shapes.Count} shapes:");
            shapes.ForEach(shape => Console.WriteLine(shape));
        }
    }


    public static int GetHeight(string shape)
    {
        int height;
        const int minHeight = 1;
        const int maxHeight = 50;

        Console.Write($"Enter the number of rows the {shape} should have (1-50): ");
        do
        {
            string inputHeightString = Console.ReadLine()?.Trim()!;

            if (int.TryParse(inputHeightString, out height) && height >= minHeight && height <= maxHeight)
            {
                break; // Exit the loop if the input is valid
            }
            Console.Write($"Please try again by entering an integer between {minHeight} and {maxHeight}: ");
        }
        while (true);

        return height;
    }

}


class DrawShape
{
    public static char Character()
    {
        Random random = new();
        int randomNumber = random.Next(33, 127);
        return (char)randomNumber;
    }

    public static string Draw(string shape, char asciiChar, int height)
    {
        switch (shape)
        {
            case "triangle":
                return CreateTriangle(asciiChar, height);
            case "diamond":
                return CreateDiamond(asciiChar, height);
            case "square":
                return CreateSquare(asciiChar, height);
            default:
                return string.Empty;
        }
    }

    public static string CreateTriangle(char asciiChar, int input, bool upsideDown = false, int extraSpace = 0)
    {
        int adjustedInput = input - 1;
        string triangleOutput = "";

        if (!upsideDown)
        {
            for (int row = 0; row <= adjustedInput; row += 1)
            {
                for (int space = 0; space < adjustedInput - row; space += 1)
                {
                    triangleOutput += " ";
                }
                for (int i = 0; i < 2 * row + 1; i += 1)
                {
                    triangleOutput += asciiChar;
                }
                triangleOutput += Environment.NewLine;
            }
        }
        else
        {
            for (int row = adjustedInput; row >= 0; row -= 1)
            {
                for (int space = -extraSpace; space < adjustedInput - row; space += 1)
                {
                    triangleOutput += " ";
                }
                for (int i = 0; i < 2 * row + 1; i += 1)
                {
                    triangleOutput += asciiChar;
                }
                triangleOutput += Environment.NewLine;
            }
        }

        return triangleOutput;
    }

    public static string CreateDiamond(char asciiChar, int height)
    {
        int halfHeight = height / 2;
        int middleRow = height % 2 == 0 ? 0 : 1;

        string diamondOutput = CreateTriangle(asciiChar, halfHeight + middleRow);
        diamondOutput += CreateTriangle(asciiChar, halfHeight, true, middleRow);

        return diamondOutput;
    }

    public static string CreateSquare(char asciiChar, int height)
    {
        string squareOutput = "";
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < height; col++)
            {
                squareOutput += $"{asciiChar} ";
            }
            squareOutput += Environment.NewLine;
        }
        return squareOutput;
    }
}

