namespace Flashcards
{
    internal class ValidateNumber
    {
        internal static string ValidateNum(string message)
        {
            var input = Console.ReadLine();
            while (!Int32.TryParse(input, out _))
            {
                Console.WriteLine(message);
                if (string.IsNullOrEmpty(input)) Console.WriteLine("Input cannot be empty");
                input = Console.ReadLine();
            }
            return input;
        }
    }
}