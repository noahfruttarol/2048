namespace _2048
{
    public class Block
    {
        //constructs block as empty.
        public Block()
        {
            Value = 0;
            beenMerged = false;
        }



        //hold the value of a block.
        private int Value;

        //tell if block is already merged this turn
        private bool beenMerged;

        //gets the value of a block.
        public int Get_val()
        {
            return Value;
        }

        //sets block to other blcok if empty else doubles blocks value if they have the same val else returns 0
        public int Merge(ref Block other)
        {   //checks if block has been merged this turn as cannot not merg block twice in one turn and if other block has anything in it
            if (beenMerged || other.Get_val() == 0)
                return 0;//returns 0 to show block has not been moved
                         //checks if block empty
            if (Get_val() == 0)
            {   //if block empty then sets block to other block and emptys other block
                Value = other.Get_val();
                beenMerged = other.beenMerged;
                other.Clear();
                return Get_val(); //returns block value to show it's been moved
            }
            //if block not empty but not same value, or other block is already been merged, then dont move other block and return 0 to show that nothing moved 
            if (other.Get_val() != Get_val() || other.beenMerged)
                return 0;
            //gets here if blocks have same value so they get merged and returns new value to show they have been merged
            Value *= 2;
            other.Clear();
            beenMerged = true;
            return Get_val();
        }

        //sets block to 2 if empty 
        public bool Fill()
        {
            if (Get_val() != 0)
            {
                return false;
            }

            var rand = new Random();
            int k = rand.Next(0, 10);
            switch (k)
            { //to add 10% chance of a 4 to spawn
                case 1:
                    Value = 4;
                    break;
                default:
                    Value = 2;
                    break;
            }

            return true;
        }

        //sets block to 0 and been_merg to false
        private void Clear()
        {
            beenMerged = false;
            Value = 0;
        }

        //sets been_merg to false as its new turn
        public void Turn_reset()
        {
            beenMerged = false;
        }
    }
}
