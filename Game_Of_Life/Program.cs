using System;
using System.Text;
using System.Threading;

namespace Game_Of_Life
{
    class Program
    {
        static void Main(string[] args)
        {
            setup set = new setup();
            drawer draw = new drawer();
            iteration iter = new iteration();


            Console.WriteLine("#######____GAME OF LIFE____#######");
            Console.WriteLine("Input side length");
            int n = Convert.ToInt32(Console.ReadLine());
            set.frameSetup(n);
            draw.drawCur(set.cellBlock, n);
            while (true)
            {
                iter.updater(set.cellBlock, n);
                if (set.cellBlock == iter.stepAray) break; 
                set.cellBlock = iter.stepAray;
                draw.drawCur(set.cellBlock, n);
                Thread.Sleep(100); // Time of buffering beatween rewrites
            }
            Console.WriteLine("beigasigues");

        }
    }

    class setup
    {  
        //Maine matrix of data containing all cells 
        public int[,] cellBlock;
        //generate matrix with dimensions of nxn
        public void frameSetup(int n)
        {
            Random ran = new Random();
            cellBlock = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //Input random value of 1 or 0
                    cellBlock[i, j] = ran.Next(2); 

                }
            }
        }
    }

    class iteration
    {
        //extra array for next generations calculations
        public int[,] stepAray;

        //Calculates the efective values of naboring celss ant sets new array
        public void updater(int[,] arr, int n)
        {
            stepAray = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int sum = 0;
                    if (i == 0)
                    {
                        if (j == 0) sum = arr[i, j + 1] + arr[i + 1, j] + arr[i + 1, j + 1];
                        else if (j == n-1) sum = arr[i, j - 1] + arr[i + 1, j - 1] + arr[i + 1, j];
                        else sum = arr[i, j - 1] + arr[i, j + 1] + arr[i + 1, j - 1] + arr[i + 1, j] + arr[i + 1, j + 1];
                    }
                    else if (i == n-1)
                    {
                        if (j == 0) sum = arr[i - 1, j] + arr[i - 1, j + 1] + arr[i, j + 1];
                        else if (j == n-1) sum = arr[i - 1, j - 1] + arr[i - 1, j] + arr[i, j - 1];
                        else sum = arr[i - 1, j - 1] + arr[i - 1, j] + arr[i - 1, j + 1] + arr[i, j - 1] + arr[i, j + 1];
                    }
                    else {
                        if (j == 0) sum = arr[i - 1, j] + arr[i - 1, j + 1] + arr[i, j + 1] + arr[i + 1, j] + arr[i + 1, j + 1];
                        else if (j == n-1) sum = arr[i - 1, j - 1] + arr[i - 1, j] + arr[i, j - 1] + arr[i + 1, j - 1] + arr[i + 1, j];
                        else sum = arr[i - 1, j - 1] + arr[i - 1, j] + arr[i - 1, j + 1] + arr[i, j - 1] + arr[i, j + 1] + arr[i + 1, j - 1] + arr[i + 1, j] + arr[i + 1, j + 1];
                    }
                    
                    if (arr[i,j]==1) 
                    {
                        if (sum == 2 || sum == 3) stepAray[i, j] = 1;
                        else stepAray[i, j] = 0;
                    }
                    else
                    {
                        if (sum == 3) stepAray[i, j] = 1;
                        else stepAray[i, j] = 0;
                    }
                }
            }
            

        }
    }


    class drawer
    {
        //Printer resets the pointer in console and rewrites matrix
        public void drawCur(int[,] cellBlock, int n)
        {
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("                                 ");
            for (int i = 0; i < n; i++)
            {
                string textline;
                var line = new StringBuilder();
                for (int j = 0; j < n; j++)
                {
                    line.Append(cellBlock[i, j]);
                }
                textline = line.ToString();
                Console.WriteLine("| " + textline + " |");
            }
        }
    }
}
