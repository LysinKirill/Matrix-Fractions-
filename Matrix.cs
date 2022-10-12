namespace MatrixFractions;

public class Matrix
{
    Fraction[,] data;
    int m, n;

    public Matrix(Fraction[,] f)
    {
        m = f.GetLength(0);
        n = f.GetLength(1);
        data = f;
    }

    public Matrix()
    {
        m = 0;
        n = 0;
        data = null;
    }

    public static Matrix Parse(string s)
    {
        string[] lines = s.Split("\n");
        int m = lines.Length;
        int n = lines[0].Split(" ").Length;
        Matrix a = new Matrix();
        a.data = new Fraction[m, n];
        a.m = m;
        a.n = n;
        for (int i = 0; i < m; i++)
        {
            string[] arr = lines[i].Split(" ");
            for (int j = 0; j < n; j++)
            {
                a.data[i, j] = Fraction.Parse(arr[j]);
            }
        }

        return a;
    }

    public void Read(int m, int n)
    {
        (int, int) size = (m, n);
        this.data = new Fraction[size.Item1, size.Item2];
        (m, n) = size;
        string[] s;
        for (int i = 0; i < size.Item1; i++)
        {
            s = Console.ReadLine().Split(" ");
            for (int j = 0; j < size.Item2; j++)
                data[i, j] = Fraction.Parse(s[j]);
        }
        Console.WriteLine();
    }
    public void Read()
    {
        String[] s = Console.ReadLine().Split(" ");
        (int, int) size = (int.Parse(s[0]), int.Parse(s[1]));
        this.data = new Fraction[size.Item1, size.Item2];
        (m, n) = size;
        for (int i = 0; i < size.Item1; i++)
        {
            s = Console.ReadLine().Split(" ");
            for (int j = 0; j < size.Item2; j++)
                data[i, j] = Fraction.Parse(s[j]);
        }
        Console.WriteLine();
    }

    public void Write()
    {
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(data[i, j]);
                if (j != (n - 1))
                    Console.Write(" ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public static Matrix Transpose(Matrix p)
    {
        Matrix tr = new Matrix();
        (tr.m, tr.n) = (p.n, p.m);
        tr.data = new Fraction[tr.m, tr.n];
        for(int i = 0; i < tr.m; i++)
        for (int j = 0; j < tr.n; j++)
            tr.data[i, j] = p.data[j, i];
        return tr;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (!(a.n == b.m))
            throw new Exception();
        Matrix c = new Matrix();
        c.m = a.m;
        c.n = b.n;
        c.data = new Fraction[c.m, c.n];
        for (int i = 0; i < a.m; i++)
        for (int j = 0; j < b.n; j++)
        {
            Fraction s = new Fraction(0);
            for (int k = 0; k < a.n; k++)
                s += a.data[i, k] * b.data[k, j];
            c.data[i, j] = s;
        }

        return c;
    }

    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (!((a.m == b.m) & (a.n == b.n)))
            throw new Exception();
        Matrix c = new Matrix();
        (c.m, c.n) = (a.m, a.n);
        c.data = new Fraction[c.m, c.n];
        for (int i = 0; i < c.m; i++)
        for (int j = 0; j < c.n; j++)
            c.data[i, j] = a.data[i, j] + b.data[i, j];
        return c;
    }

    public static Matrix Minor(Matrix a, int i, int j)
    {
        Fraction[,] f = new Fraction[a.m - 1, a.n - 1];

        for (int p = 0; p < a.m; p++)
        {
            if (p == i)
                continue;
            int y = p;
            if (p >= i)
                y -= 1;
            for (int l = 0; l < a.n; l++)
            {
                if (l == j)
                    continue;
                int x = l;
                if (l >= j)
                    x -= 1;
                f[y, x] = a.data[p, l];

            }
        }

        return new Matrix(f);

    }

    public static Fraction Det(Matrix a)
    {
        if (a.m != a.n)
            throw new Exception();
        if (a.n == 2)
            return a.data[0, 0] * a.data[1, 1] - a.data[0, 1] * a.data[1, 0];
        Fraction sum = new Fraction(0);
        //  разложение по 1-ой строке
        for (int j = 0; j < a.n; j++)
        {
            sum += a.data[0, j] * IntPow(-1, 0 + j) * Det(Minor(a, 0, j));
        }

        return sum;
    }

    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (!((a.m == b.m) & (a.n == b.n)))
            throw new Exception();
        Matrix c = new Matrix();
        (c.m, c.n) = (a.m, a.n);
        c.data = new Fraction[c.m, c.n];
        for (int i = 0; i < c.m; i++)
        for (int j = 0; j < c.n; j++)
            c.data[i, j] = a.data[i, j] - b.data[i, j];
        return c;
    }


    public static Matrix operator *(Matrix a, int num)
    {
        
        Matrix c = new Matrix();
        c.m = a.m;
        c.n = a.n;
        c.data = new Fraction[a.m, a.n];
        for(int i = 0; i < c.m; i++)
            for (int j = 0; j < c.n; j++)
                c.data[i, j] = a.data[i, j] * num;
        
        return c;
    }

    public static Matrix operator *(int num, Matrix a) => a * num;

    public void AddToRow(int i, int j, Fraction alpha)
    {
        for (int k = 0; k < n; k++)
        {
            data[i, k] += data[j, k] * alpha;
        }
    }

    public void AddToRow(int i, int j) => AddToRow(i, j, new Fraction(1));
    public static int IntPow(int x, int y)
    {
        if (y == 0)
            return 1;
        int product = x;
        for (int i = 1; i < y; i++)
        {
            product *= x;
        }

        return product;
    }

    //public static Matrix Eij(int i, int j)
}