using System;
namespace _2048{
    public class Block
    {
        //constructs block as empty.
        public Block()
        {
            Value = 0;
            Been_merg = false;
        }


        
        //hold the value of a block.
        private int Value{ get; set; }

        //tell if block is already merged this turn
        private bool Been_merg { get; set; }

        //gets the value of a block.
        public int Get_val()
        {
            return Value;
        }

        //sets block to other blcok if empty else doubles blocks value if they have the same val else returns 0
        public int Merg(ref Block other)
        {   //checks if block has been merged this turn as cannot not merg block twice in one turn and if other block has anything in it
            if (Been_merg || other.Get_val() == 0)
                return 0;//returns 0 to show block has not been moved
            //checks if block empty
            if (Get_val() == 0)
            {   //if block empty then sets block to other block and emptys other block
                Value = other.Get_val();
                Been_merg = other.Been_merg;
                other.Clear();
                return Get_val(); //returns block value to show it's been moved
            }
            //if block not empty but not same value, or other block is already been merged, then dont move other block and return 0 to show that nothing moved 
            if (other.Get_val() != Get_val()||other.Been_merg) 
                return 0;
            //gets here if blocks have same value so they get merged and returns new value to show they have been merged
            Value *= 2;
            other.Clear();
            Been_merg = true;
            return Get_val();
        }

        //sets block to 2 if empty 
        public bool Fill()
        {
            if (Get_val() != 0)
            {
                return false;
            }
            Value = 2;
            return true;
        }

        //sets block to 0 and been_merg to false
        private void Clear()
        {
            Been_merg = false;
            Value = 0;
        }

        //sets been_merg to false as its new turn
        public void Turn_reset() 
        {
            Been_merg = false;
        }
    }
    
    public class Borad 
    { 
        //constructs with defalt size of 4
        public Borad()
        {
            Turns = 0;
            Size = 4;
            End = 2048;
            Blocks = new Block[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Blocks[i, j] = new Block();
                }
            }
        }

        //constructs with size of input
        public Borad(int input)
        {
            Turns = 0;
            if(input < 3 || input > 8) //borad is too small at 2 and would be too big at 9
                throw new ArgumentOutOfRangeException("size too big should be between 3 and 8");
            Size = input;
            End = 2048;
            Blocks = new Block[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Blocks[i, j] = new Block();
                }
            }
        }

        //this is the length and hight of the borad
        private int Size{ get; set; }

        //value if reached game ends
        private int End { get; set; }

        //holds all the blocks in the borad
        private Block[,] Blocks { get; set; }

        //number of turns 
        private int Turns { get; set; }

        //starts the game
        public void Start()
        {
            Console.WriteLine("Starting game with {0} X {0} borad", Size);//starting text
            Console.WriteLine("Rules: w = up, s = down, a = left, d = right");//rules
            Console.WriteLine("Good Luck");
            New_turn();//sets up borad with a block spawn
            Print_borad();
            bool game_runing = true;//to see if the game is still in a valid state
            while (game_runing)
            {
                bool move_happend = true;//for if the wrong key is presed 
                Console.WriteLine("What is your next move?");
                string? s = Console.ReadLine();//gets input
                char c = 'l';//incase no input
                if(s != null)
                    if (s.Length != 0)
                        c = s[0];//sets c to input if there is any
                switch(c){
                    case 'w':
                        move_happend = Up();
                        break;
                    case 's':
                        move_happend = Down();
                        break;
                    case 'a':
                        move_happend = Left();
                        break;
                    case 'd':
                        move_happend = Right();
                        break;
                    default:
                        Console.WriteLine("Enter only a 'w', 'a', 's' or 'd'");
                        move_happend = false;
                        break;
                }
                if (move_happend == false)
                {
                    Console.WriteLine("So, nothing moved");//invalid input or nothing moved skips does not add a turn
                    continue;
                }
                Turns++;
                game_runing = New_turn(); //checks for end states and spawns new block
                if(game_runing)
                    Print_borad();
            }
        }

        //ends the game if 0 the player gets a block to the end amount or 1 if the borad is filled after the player made there turn
        private void Finish(int ending)
        {
            Print_borad();
            if (ending == 1)
            {
                Console.WriteLine("Borad filled, no more room for blocks. Better luck next time.");
                return;//failed ending 
            }
            Console.WriteLine("Congrats you won in {0} turns, good job!", Turns);//sucsess ending
        }

        //prints borad to console
        private void Print_borad()
        {
            Console.Write("_");
            for (int i = 0; i < Size; i++)
                Console.Write("_____");//prints top line
            for (int i = 0; i < Size; i++)
            {
                Print_row(i);
            }
        }

        //prints row to console
        private void Print_row(int row) 
        {
            
            Console.WriteLine();
            Console.Write("|");//prints left most line
            for (int i = 0; i < Size; i++)
            {
                Set_color(row, i); //sets coulor of block
                Console.Write("    |");
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("|");//next loop the same exsept adds value of block  
            for (int i = 0; i < Size; i++)
            {
                Set_color(row, i);
                if(Blocks[row, i].Get_val() != 0)
                    Console.Write("{0,-4}|", Blocks[row, i].Get_val());
                else
                    Console.Write("    |");//if empty block no number
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("|");
            for (int i = 0; i < Size; i++)
            {
                Set_color(row, i);
                Console.Write("____|");//prints bottom of box
            }
            Console.ResetColor();
            Console.WriteLine("");
        }

        private void Set_color(int row, int col)
        {
            Block temp = Blocks[row, col];
            switch (temp.Get_val())//sets color based on value of block
            {
                case 0:
                    Console.ResetColor();
                    return;
                case 2:
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    return;
                case 4:
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    return;
                case 8:
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    return;
                case 16: 
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    return;
                case 32:
                    Console.BackgroundColor = ConsoleColor.Red;
                    return;
                case 64:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    return;
                case 2048:
                    Console.BackgroundColor = ConsoleColor.Green;
                    return;
                default:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    return;

            }
        }

        //resets been_merged in each block and spawns a new block on the borad
        private bool New_turn()
        {   //gets array to hold indexes of empty blocks
            int[] temp = new int[Size * Size];
            int temp_index = 0;//amount of empty blocks

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Blocks[i, j].Get_val() == 0)
                    {   //empty block added to temp so it may be filled 
                        temp[temp_index] = i * Size + j;//stores index as i*size + j so just one int to worry about
                        temp_index++;
                    }
                    else
                    {   //else ends game if it is the end value
                        if (Blocks[i, j].Get_val() == End)
                        {
                            Finish(0);
                            return false;//returns false if end of game
                        }
                        Blocks[i, j].Turn_reset(); //resets been merged as it is new turn
                    }
                }
            }
            if (temp_index == 0)
            {
                Finish(1); //no empty blocks therefore end of game 
                return false;//end of game returns false
            }
            var rand = new Random();
            int index = temp[rand.Next(0, temp_index)]; //gets random index of empty block
            Blocks[index/Size, index%Size].Fill(); //filles the block
            return true;//returns true if game still going
        }

        private bool Up() 
        {   
            bool moved = false;
            for(int col = 0; col < Size; col++)
            {   //loop to go over each column
                for (int i = 1; i < Size; i++)//i to keep track of witch row has been moved
                {   //loop that covers each block down 1 from the top 
                    int k = i;//k to keep track of the block to move up
                    for (int j = i - 1; j >= 0; j--)//j keeps track of block being merged with
                    {   //loop to keep moving the block up untill it cant be merged
                        int temp = Blocks[j, col].Merg(ref Blocks[k, col]);//merges block at row k with block above 
                        if (temp == 0)//if temp is 0 then the block was not moved and there can no longer be a merg from block at row k
                            break;
                        moved = true;
                        k--;
                    }
                }
            }
            return moved;
        }

        private bool Down()
        {
            bool moved = false;
            for (int col = 0; col < Size; col++)
            {   //loop to go over each column
                for (int i = Size - 2; i >= 0; i--)//i to keep track of witch row has been moved
                {   //loop that covers each block up 1 from the bottom 
                    int k = i;//k to keep track of the block to move down
                    for (int j = i + 1; j < Size; j++)//j keeps track of block being merged with
                    {   //loop to keep moving the block down untill it cant be merged
                        int temp = Blocks[j, col].Merg(ref Blocks[k, col]);//merges block at row k with block below
                        if (temp == 0)//if temp is 0 then the block was not moved and there can no longer be a merg from block at row k
                            break;
                        moved = true;
                        k++;
                    }
                }
            }
            return moved;
        }

        private bool Left()
        {
            bool moved = false;
            for (int row = 0; row < Size; row++)
            {   //loop to go over each row
                for (int i = 1; i < Size; i++)
                {   //loop that covers each block right 1 from the left
                    int k = i;//k to keep track of the block to move left
                    for (int j = i - 1; j >= 0; j--)//j keeps track of block being merged with
                    {   //loop to keep moving the block left untill it cant be merged
                        int temp = Blocks[row, j].Merg(ref Blocks[row, k]);//merges block at column k with block left
                        if (temp == 0)//if temp is 0 then the block was not moved and there can no longer be a merg from block at column k
                            break;
                        k--;
                        moved = true;
                    }
                }
            }
            return moved;
        }

        private bool Right()
        {
            bool moved = false;
            for (int row = 0; row < Size; row++)
            {   //loop to go over each row
                for (int i = Size - 2; i >= 0; i--)
                {   //loop that covers each block left 1 from the right
                    int k = i;//k to keep track of the block to move right
                    for (int j = i + 1; j < Size; j++)//j keeps track of block being merged with
                    {   //loop to keep moving the block right untill it cant be merged
                        int temp = Blocks[row, j].Merg(ref Blocks[row, k]);//merges block at column k with block right
                        if (temp == 0)//if temp is 0 then the block was not moved and there can no longer be a merg from block at column k
                            break;
                        k++;
                        moved = true;
                    }
                }
            }
            return moved;
        }

    }

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
                Borad game = new Borad(x);
                game.Start();
            }
            else//if invalid size use defalt
            {
                Borad game = new Borad();
                game.Start();
            }
            return 0;
        }
    }
}
