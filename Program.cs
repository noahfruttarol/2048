using System;
namespace _2048{
    public class block
    {
        //constructs block as empty.
        public block()
        {
            Value = 0;
            been_merg = false;
        }


        
        //hold the value of a block.
        private int Value{ get; set; }

        //tell if block is already merged this turn
        private bool been_merg { get; set; }

        //gets the value of a block.
        public int get_val()
        {
            return Value;
        }

        //sets block to other blcok if empty else doubles blocks value if they have the same val else returns 0
        public int merg(ref block other)
        {   //checks if block has been merged this turn as cannot not merg block twice in one turn and if other block has anything in it
            if (been_merg || other.get_val() == 0)
                return 0;//returns 0 to show block has not been moved
            //checks if block empty
            if (get_val() == 0)
            {   //if block empty then sets block to other block and emptys other block
                Value = other.get_val();
                been_merg = other.been_merg;
                other.clear();
                return get_val(); //returns block value to show it's been moved
            }
            //if block not empty but not same value, or other block is already been merged, then dont move other block and return 0 to show that nothing moved 
            if (other.get_val() != get_val()||other.been_merg) 
                return 0;
            //gets here if blocks have same value so they get merged and returns new value to show they have been merged
            Value *= 2;
            other.clear();
            been_merg = true;
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

        //sets block to 0 and been_merg to false
        private void clear()
        {
            been_merg = false;
            Value = 0;
        }

        //sets been_merg to false as its new turn
        public void turn_reset() 
        {
            been_merg = false;
        }
    }
    
    public class borad 
    { 
        //constructs with defalt size of 4
        public borad()
        {
            Size = 4;
            end = 2048;
            blocks = new block[Size, Size];
        }

        //constructs with size of input
        public borad(int input)
        {
            if(input < 3 || input > 8) //borad is too small at 2 and would be too big at 9
                throw new ArgumentOutOfRangeException("size too big should be between 3 and 8");
            Size = input;
            end = 2048;
            blocks = new block[Size, Size];
        }

        //this is the length and hight of the borad
        private int Size{ get; set; }

        //value if reached game ends
        private int end { get; set; }

        //holds all the blocks in the borad
        private block[,] blocks { get; set; }

        //starts the game
        public void start()
        {

        }

        //ends the game if 0 the player gets a block to the end amount or 1 if the borad is filled after the player made there turn
        private void finish(int ending)
        {

        }

        //resets been_merged in each block and spawns a new block on the borad
        private bool new_turn()
        {   //gets array to hold indexes of empty blocks
            int[] temp = new int[Size * Size];
            int temp_index = 0;//amount of empty blocks

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (blocks[i, j].get_val() == 0)
                    {   //empty block added to temp so it may be filled 
                        temp[temp_index] = i * Size + j;//stores index as i*size + j so just one int to worry about
                        temp_index++;
                    }
                    else
                    {   //else ends game if it is the end value
                        if (blocks[i, j].get_val() == end)
                        {
                            finish(0);
                            return false;//returns false if end of game
                        }
                        blocks[i, j].turn_reset(); //resets been merged as it is new turn
                    }
                }
            }
            if (temp_index == 0)
            {
                finish(1); //no empty blocks therefore end of game 
                return false;//end of game returns false
            }
            var rand = new Random();
            int index = temp[rand.Next(0, temp_index)]; //gets random index of empty block
            blocks[index/Size, index%Size].fill(); //filles the block
            return true;//returns true if game still going
        }

        private void up() 
        { 
            for(int col = 0; col < Size; col++)
            {   //loop to go over each column
                for (int i = 1; i < Size; i++)//i to keep track of witch row has been moved
                {   //loop that covers each block down 1 from the top 
                    int k = i;//k to keep track of the block to move up
                    for (int j = i - 1; j >= 0; j--)//j keeps track of block being merged with
                    {   //loop to keep moving the block up untill it cant be merged
                        int temp = blocks[j, col].merg(ref blocks[k, col]);//merges block at row k with block above 
                        if (temp == 0)//if temp is 0 then the block was not moved and there can no longer be a merg from block at row k
                            break;
                        k--;
                    }
                }
            }
        }

        private void down()
        {   
            for (int col = 0; col < Size; col++)
            {   //loop to go over each column
                for (int i = Size - 1; i >= 0; i--)//i to keep track of witch row has been moved
                {   //loop that covers each block up 1 from the bottom 
                    int k = i;//k to keep track of the block to move down
                    for (int j = i + 1; j < Size; j++)//j keeps track of block being merged with
                    {   //loop to keep moving the block down untill it cant be merged
                        int temp = blocks[j, col].merg(ref blocks[k, col]);//merges block at row k with block below
                        if (temp == 0)//if temp is 0 then the block was not moved and there can no longer be a merg from block at row k
                            break;
                        k++;
                    }
                }
            }
        }

        private void left()
        { 
            for (int row = 0; row < Size; row++)
            {   //loop to go over each row
                for (int i = 1; i < Size; i++)
                {   //loop that covers each block right 1 from the left
                    int k = i;//k to keep track of the block to move left
                    for (int j = i - 1; j >= 0; j--)//j keeps track of block being merged with
                    {   //loop to keep moving the block left untill it cant be merged
                        int temp = blocks[row, j].merg(ref blocks[row, k]);//merges block at column k with block left
                        if (temp == 0)//if temp is 0 then the block was not moved and there can no longer be a merg from block at column k
                            break;
                        k--;
                    }
                }
            }
        }

        private void right()
        { 
            for (int row = 0; row < Size; row++)
            {   //loop to go over each row
                for (int i = Size - 1; i >= 0; i--)
                {   //loop that covers each block left 1 from the right
                    int k = i;//k to keep track of the block to move right
                    for (int j = i + 1; j < Size; j++)//j keeps track of block being merged with
                    {   //loop to keep moving the block right untill it cant be merged
                        int temp = blocks[row, j].merg(ref blocks[row, j]);//merges block at column k with block right
                        if (temp == 0)//if temp is 0 then the block was not moved and there can no longer be a merg from block at column k
                            break;
                        k++;
                    }
                }
            }
        }

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
