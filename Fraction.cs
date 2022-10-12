namespace MatrixFractions;

public class Fraction
{
    private int numerator, denominator;




    public Fraction(int a, int b)
    {
        this.numerator = a;
        this.denominator = b;
        Simplify();
    }

    public Fraction(int a)
    {
        this.numerator = a;
        this.denominator = 1;
    }

    private void Simplify()
    {
        int n = Gcd(numerator, denominator);
        numerator /= n;
        denominator /= n;
    }

    public static double GetDouble(Fraction f) => (double)f.numerator / f.denominator;

    public override string ToString()
    {
        if (denominator == 1)
            return numerator.ToString();
        else
            return $"{numerator}/{denominator}";
    }

    private static int Gcd(int a, int b)
    {
        (a, b) = (Math.Abs(a), Math.Abs(b));
        while (b > 0)
        {
            (a, b) = (b, a % b);
        }
        return a;
    }

    public static Fraction Parse(String s)
    {
        if (s.Contains('/'))
        {
            string[] a = s.Split("/");
            return new Fraction(int.Parse(a[0]), int.Parse(a[1]));
        }

        return new Fraction(int.Parse(s));
    }

    public static Fraction operator *(Fraction a, Fraction b)
    {
        Fraction f = new Fraction((a.numerator * b.numerator), (a.denominator * b.denominator));
        f.Simplify();
        return f;
    }

    public static Fraction operator +(Fraction a, Fraction b)
    {
        Fraction f = new Fraction(a.numerator * b.denominator + b.numerator * a.denominator,
            a.denominator * b.denominator);
        f.Simplify();
        return f;
    }

    public static Fraction operator *(int a, Fraction b)
    {
        Fraction f = new Fraction(a * b.numerator, b.denominator);
        f.Simplify();
        return f;
    }

    public static Fraction operator +(int a, Fraction b)
    {
        return new Fraction(a * b.denominator + b.numerator, b.denominator);
    }

    public static Fraction operator -(int a, Fraction b) => a + (b * (-1));
    public static Fraction operator -(Fraction b, int a) => b + (-a);
    public static Fraction operator -(Fraction a, Fraction b) => a + (b * (-1));
    public static Fraction operator +(Fraction b, int a) => (a + b);
    public static Fraction operator *(Fraction b, int a) => (a * b);

    public static Fraction operator /(Fraction a, int b)
    {
        Fraction f = new Fraction(a.numerator, a.numerator * b);
        f.Simplify();
        return f;
    }

    public static Fraction operator /(int b, Fraction a)
    {
        Fraction f = new Fraction(b * a.denominator, a.numerator);
        f.Simplify();
        return f;
    }

    public static Fraction operator /(Fraction a, Fraction b)
    {
        Fraction f = new Fraction(a.numerator * b.denominator, a.denominator * b.numerator);
        f.Simplify();
        return f;
    }
}