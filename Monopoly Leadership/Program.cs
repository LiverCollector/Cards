namespace Monopoly_Leadership
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rng  = new Random();
            int[] sixsided = new int[11];
            int[] eleven = new int[11];
            for (int i = 0; i < 10000; i++)
            {
                int roll1 = rng.Next(6);
                int roll2 = rng.Next(6);
                sixsided[roll1 + roll2]++;
                eleven[rng.Next(11)]++;
            }
            Console.WriteLine("value\tsix\televen\tdiff");
            for (int i = 0; i < sixsided.Length; i++) 
            {
                Console.WriteLine((2 + i) + ":\t" + sixsided[i] + ",\t" + eleven[i] + "\t" + (sixsided[i] - eleven[i]));
            }
        }
    }
}
