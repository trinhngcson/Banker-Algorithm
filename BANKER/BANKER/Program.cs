using System;

namespace BANKER
{
    class Program
    {
        static void CalculateNeed(int numP, int numR, int[,] need, int[,] maxm, int[,] allot)
        {
            for(int i = 0; i < numP; i++)
                for(int j = 0; j < numR; j++)
                {
                    need[i, j] = maxm[i, j] - allot[i, j];
                }
        }

        static bool isSafe (int numP, int numR, int[] processes, int[] avail, int[,] need, int[,] allot)
        {
            bool[] finish = new bool[numP];
            int[] safeSeq = new int[numP];
            int[] availC = new int[numR];

            for (int i = 0; i < numR; i++)
            {
                availC[i] = avail[i];
            }

            int count = 0;
            while (count < numP)
            {
                bool found = false;
                for (int p = 0; p < numP; p++)
                {
                    if (finish[p] == false)
                    {
                        int j;
                        for (j = 0; j < numR ; j++)
                        {
                            if (need[p, j] > availC[j])
                                break;
                        }
                        if (j == numR)
                        {
                            for (int k = 0;k < numR; k++)
                            {
                                availC[k] += allot[p, k];
                            }
                            safeSeq[count++] = p;
                            finish[p] = true;
                            found = true;
                            break;
                        }
                    }
                }
                if (found == false)
                {
                    Console.Write("\nHe thong khong an toan");
                    return false;
                }
            }
            Console.Write("\nHe thong an toan, thu tu cap phat an toan la: ");
            for ( int i =0; i < numP; i++)
            {
                Console.Write(safeSeq[i] + " ");
            }
            return true;
        }
        static bool Banker(int p, int[] Request, int numP, int numR, int[] processes, int[] avail, int[,] need, int[,] allot)
        {
            int[,] needC = new int[numP, numR];
            int[,] allotC = new int[numP, numR];
            int[] availC = new int[numR];
            for (int i = 0; i < numP; i++)
                for (int j =0; j < numR; j++)
                {
                    needC[i, j] = need[i, j];
                    allotC[i, j] = allotC[i, j];
                }
            for (int i =0; i < numR; i++)
            {
                availC[i] = avail[i];
            }
            for (int r = 0; r < numR; r++)
            {
                if (need[p, r] >= Request[r] && avail[r] >= Request[r])
                {
                    need[p, r] -= Request[r];
                    allot[p, r] += Request[r];
                    avail[r] -= Request[r];
                }
                else
                {
                    Console.Write("\n Yeu cau khong hop le!!!");
                    return false;
                }
            }
            bool flag = isSafe(numP, numR, processes, avail, need, allot);
            Console.Write("\nYeu cau cua P1 : ");
            for (int i =0; i < numR; i++){
                Console.Write(Request[i] + " ");
            }
            if (flag)
                Console.Write("Cap phat duoc.");
            else
                Console.Write("Phai doi!!!");
            return flag;
        }
        static void Main(string[] args)
        {
            int numP = 5, numR = 3;
            int[] processes = { 0, 1, 2, 3, 4 };
            int[] avail = { 3, 3, 2 };
            int[,] maxm = {
                { 7, 5, 3 },
                { 3, 2, 2 },
                { 9, 0, 2 },
                { 2, 2, 2 },
                { 4, 3, 3 } };

            int[,] allot = {
                { 0, 1, 0 },
                { 2, 0, 0 },
                { 3, 0, 2 },
                { 2, 1, 1 },
                { 0, 0, 2 } };

            int[,] need = new int[numP, numR];
            CalculateNeed(numP, numR, need, maxm, allot);

            int[] Request = { 1, 0, 2 };
            Banker(1, Request, numP, numR, processes, avail, need, allot);
            Console.ReadLine();
        }
       
    }
}
