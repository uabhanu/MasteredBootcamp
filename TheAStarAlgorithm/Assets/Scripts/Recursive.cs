public class Recursive : Maze
{
    protected override void Generate()
    {
        Generate(5 , 5);
    }

    private void Generate(int x , int z)
    {
        while(true)
        {
            if(CountSquareNeighbours(x , z) >= 2) return;
            Map[x , z] = 0;

            Directions.Shuffle();

            Generate(x + Directions[0].x , z + Directions[0].z);
            Generate(x + Directions[1].x , z + Directions[1].z);
            Generate(x + Directions[2].x , z + Directions[2].z);
            
            x = x + Directions[3].x;
            z = z + Directions[3].z;
        }
    }
    
    //TODO If instructor doesn't tell you already, do the maze size and colour as shown by the instructor as yours is different and not that good
}
