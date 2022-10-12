using System;


namespace MatrixFractions
{
    public class MainClass
    {
        public static void Main()
        {
            Matrix m = new Matrix();
            m.Read();
            m.Write();
            
            Console.WriteLine(Matrix.Det(m));

            int i, j;
            while (true)
            {
                string[] s = Console.ReadLine().Split();
                if (!(int.TryParse(s[0], out i) && int.TryParse(s[1], out j)))
                    break;
                m.AddToRow(i, j, new Fraction(-1));
                m.Write();
                Console.WriteLine(Matrix.Det(m));
            }
        }
    }
}
