using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RadixSort
{
    class Program
    {
        static void Main(string[] args)
        {
             string datoteka = args[0];
             string fileContent = File.ReadAllText(datoteka);
             string[] integerStrings = fileContent.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
             float[] A = new float[integerStrings.Length];

             for (int i = 0; i < integerStrings.Length; i++)
             {
                A[i] = float.Parse(integerStrings[i], System.Globalization.CultureInfo.InvariantCulture);
             }
            int[] arr = new int[A.Length];
            for (int m = 0; m < A.Length; m++)
                arr[m] = BitConverter.ToInt32(BitConverter.GetBytes(A[m]), 0);

            RadixSort(arr);
            
            for (int i = 0; i < arr.Length; i++)
                A[i] = BitConverter.ToSingle(BitConverter.GetBytes(arr[i]), 0);
            
             zapisVDaoteko(A);
        }
        static void RadixSort(int[] A)
        {
            int x, y;
            int[] B = new int[A.Length];
            for (int k = 31; k > -1; --k)
            {
                y = 0;
                for (x = 0; x < A.Length; ++x)
                {
                    bool m = (A[x] << k) >= 0;
                     if (k == 0 ? !m : m)
                         A[x - y] = A[x];
                     else
                         B[y++] = A[x];
                }
                Array.Copy(B, 0, A, A.Length - y, y);
                
            }
        }
        private static void zapisVDaoteko(float[] B)
        {
            for (int i = 0; i < B.Length; i++)
            {
                using (FileStream fs = new FileStream("out.txt", FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(B[i] + " ");
                }
            }
        }
    }
}
