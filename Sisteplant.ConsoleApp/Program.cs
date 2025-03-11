using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sisteplant.Application.Queries.Exercise1;
using Sisteplant.Application.Query.Exercise2;

class Program
{
    private static IMediator _mediator;

    static void Main(string[] args)
    {
        // Configure the service collection and add MediatR
        var serviceProvider = new ServiceCollection()
            // Register MediatR from the assembly containing the query handlers
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Exercise1QueryHandler).Assembly))
            .BuildServiceProvider();

        _mediator = serviceProvider.GetRequiredService<IMediator>();

        // Run the main logic
        Run().GetAwaiter().GetResult();
    }

    static async Task Run()
    {
        while (true)
        {
            // Menu options
            Console.WriteLine("Select an exercise:");
            Console.WriteLine("1. Insert '5' into a number");
            Console.WriteLine("2. Find the index of the maximum sequence of 1s");
            Console.WriteLine("3. Exit");

            string choice = Console.ReadLine();
            if (choice == "3") break;

            switch (choice)
            {
                case "1":
                    await ShowExercise1Async();
                    break;

                case "2":
                    await ShowExercise2Async();
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }


            static async Task ShowExercise1Async()
            {
                string inputStr = "y"; // Changed to 'y' for clarity in intention

                while (inputStr.ToLower() == "y")
                {
                    Console.Clear();
                    Console.WriteLine("Exercise 1\n");

                    // Prompt for an integer
                    Console.Write("Please enter an integer: ");
                    string input = Console.ReadLine();

                    // Validate input for integer range
                    if (!int.TryParse(input, out int number))
                    {
                        Console.WriteLine("\nInvalid number, please try again.");
                        continue; // Use continue to allow re-entry instead of return to restart the loop
                    }

                    // Call the solution method and print the result
                    var result = await _mediator.Send(new Exercise1Query(number));
                    Console.WriteLine($"Given {number}, the result is {result}.\n");

                    // Prompt to continue or not
                    Console.Write("Do you want to continue (Y/N): ");
                    inputStr = Console.ReadLine();
                }
            }


            static async Task ShowExercise2Async()
            {
                Console.Clear();
                Console.WriteLine("Exercise 2\n");

                // Prompt for array length
                Console.Write("Please enter the length of the array A[]: ");
                string input = Console.ReadLine();

                // Validate input for array length
                if (!int.TryParse(input, out int arrayLength) || arrayLength < 0)
                {
                    Console.WriteLine("Invalid number, please try again.");
                    return;
                }

                Console.WriteLine();
                int[] arrayA = new int[arrayLength];

                // Populate the array with values
                for (int index = 0; index < arrayLength; index++)
                {
                    while (true)
                    {
                        Console.Write($"Please enter the value for A[{index}]: ");
                        string value = Console.ReadLine();

                        if (!int.TryParse(value, out int number) || number < 0)
                        {
                            Console.WriteLine("Invalid number, please try again.");
                            continue; // Prompt again for the same index
                        }

                        arrayA[index] = number;
                        break; // Exit the loop if valid input is received
                    }
                }

                Console.WriteLine();
                // Call the solution method and output the result
                int result = await _mediator.Send(new Exercise2Query(arrayA));

                if (result == -1)
                {
                    Console.WriteLine($"No sequence of values 1 was found. Result: {result}");
                }
                else
                {
                    Console.WriteLine($"The first sequence of values 1 equal to the maximum is found at index {result}.");
                }
            }

        }
    }

}
