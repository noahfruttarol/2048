using System;
namespace _2048{
    public class block
    {
        //constructs block as empty.
        public block()
        {
            Value = 0;
        }


        
        //brief hold the value of a block.
        private int Value{ get; set; }

        //gets the value of a block.
        public int get_val()
        {
            return Value;
        }

        //sets block to other blcok if empty else doubles blocks value if they have the same val else returns 0
        public int merg(block other)
        {
            if (get_val() == 0)
            {
                Value = other.get_val();
                return get_val();

            }

            if (other.get_val() != get_val()) return 0;
            
            Value *= 2;
            return get_val();
        }

        //sets block to 2 if empty 
        public bool fill()
        {
            if (get_val() != 0)
            {
                return false;
            }
            Value = 2;
            return true;
        }

        //sets block to 0 
        public void clear()
        {
            Value = 0;
        }

    }
    
    public class borad 
    { 
        //constructs with defalt size of 4
        public borad()
        {
            Size = 4;
            blocks = new block[Size, Size];
        }

        //constructs with size of input
        public borad(int input)
        {
            Size = input;
            blocks = new block[Size, Size];
        }

        //this is the length and hight of the borad
        private int Size{ get; set; }

        //holds all the blocks in the borad
        private block[,] blocks { get; set; }

    }

    class Program
    {
        static int Main()
        {
            String program_name = "2048";
            String vertion = "Pre 0.1";
            String author = "Noah Fruttarol";

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("{0} Vertion: {1} by {2}", program_name, vertion, author);
            Console.ResetColor();
            Console.WriteLine();


            return 0;
        }
    }
}
