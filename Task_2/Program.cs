using System;
using System.IO;

namespace Task_2 {
    public class Program {
        private static void Main(string[] args) {
            StreamReader reader = new StreamReader("INPUT.txt");
            StreamWriter writer = new StreamWriter("OUTPUT.txt");
            
            string[] init = reader.ReadToEnd().Split();
            
            int n = Convert.ToInt32(init[0]);
            int m = Convert.ToInt32(init[1]);
            int r = m + n;
            int p = 0;
            while (n > 0) {
                p = m % n;
                m = n;
                n = p;
            }

            writer.WriteLine(r - m);
            reader.Close();
            writer.Close();
        }
    }
}
