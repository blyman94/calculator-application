using CalculatorLibrary;

// TODO: Update documentation to reflect any changes in prototype methodology. 

/// <summary>
/// Console application simulating a calculator. Allows the user to enter an 
/// infix expression, then evaluates that expression, printing the result of the
/// expression to the console. Allows the user to select one of 3 custom
/// operations: Factorial, Metric Conversion (inches-to-cm), or Pythagorean 
/// solve. Returns the result of the custom operation given the proper inputs.
/// This application is a prototype for an application that will be built with 
/// a frontend GUI in the Unity engine.
/// </summary>
class CalculatorApplication
{
    /// <summary>
    /// Prompts the user for input and performs calculations based on received
    /// input.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    public static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        Console.Clear();

        do
        {
            // Display current value and prompt user for input.
            Console.WriteLine();
            Console.WriteLine(calculator.CurrentValue.ToString());
            Console.WriteLine();
            Console.WriteLine("Enter an Expression delimited by space.");
            Console.WriteLine("Available Custom Functions: Factorial <F>, " + 
                "Metric Conversion <M>, Pythagorean Solve<P>");
            Console.WriteLine("Enter <C> to clear current value.");
            Console.WriteLine("Press <Enter> key without entering anything to exit.");
            string? input = Console.ReadLine();

            // Exit the program if the user presses <Enter> without entering an
            // expression first.
            if (string.IsNullOrEmpty(input))
            {
                break;
            }

            // Otherwise, evaluate the inputs to update calculator's current 
            // value.
            if (input != null)
            {
                try
                {
                    calculator.AcceptInputArray(input.Split(" "));
                    Console.Clear();
                }
                catch (Exception e)
                {
                    if (e is CalculatorException)
                    {
                        calculator.CurrentValue = 0;
                        Console.WriteLine($"Syntax Error: {e.Message}");
                        continue;
                    }
                }
            }
        }
        while (true);
    }
}
