namespace _2048
{
    class Program
    {
        static int Main()
        {
            String program_name = "2048";
            String vertion = "Pre 1.0";
            String author = "Noah Fruttarol";

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("{0} Vertion: {1} by {2}", program_name, vertion, author);//program info
            Console.ResetColor();
            Console.WriteLine();


            Console.WriteLine("Give size of borad 3-8 otherwise 4");
            int x = Convert.ToInt32(Console.ReadLine());
            if (x >= 3 || x <= 8)
            {
                GameSpace game = new GameSpace(x);
                game.Start();
            }
            else//if invalid size use defalt
            {
                GameSpace game = new GameSpace();
                game.Start();
            }
            return 0;
        }
    }
}
