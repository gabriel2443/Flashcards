namespace Flashcards.Helpers
{
    internal class ValidateNumber
    {
        internal static int ValidateNum(string message)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();

            while (!Int32.TryParse(input, out _) || Convert.ToInt32(input) < 0)

            {
                Console.WriteLine("Please enter another stack number");
                input = Console.ReadLine();
            }
            var finalInput = Convert.ToInt32(input);
            return finalInput;
        }
    }
}