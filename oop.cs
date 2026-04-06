namespace Lab4;

public class oop
{
    public class Sign
    {
        public bool IsPositive { get; private set; }

        public Sign(bool isPositive)
        {
            Console.WriteLine("[LOG] Викликано конструктор класу Sign.");
            IsPositive = isPositive;
        }

        public override bool Equals(object obj)
        {
            Console.WriteLine("[LOG] Викликано метод Equals класу Sign.");
            if (obj is Sign other)
                return IsPositive == other.IsPositive;
            return false;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("[LOG] Викликано метод GetHashCode класу Sign.");
            return IsPositive.GetHashCode();
        }

        public override string ToString()
        {
            Console.WriteLine("[LOG] Викликано метод ToString класу Sign.");
            return IsPositive ? "+" : "-";
        }
    }
    
    public abstract class Number
    {
        protected Number()
        {
            Console.WriteLine("[LOG] Викликано конструктор абстрактного класу Number.");
        }
        
        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();
        public abstract override string ToString();
    }

    public class SimpleFraction : Number
    {
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }
        public Sign FractionSign { get; private set; }

        public SimpleFraction(int numerator, int denominator, Sign sign)
        {
            Console.WriteLine("[LOG] Викликано конструктор класу SimpleFraction.");
            if (denominator == 0)
            {
                Console.WriteLine("[LOG] Помилка: спроба ділення на нуль в конструкторі.");
                throw new ArgumentException("Знаменник не може дорівнювати нулю.");
            }

            Numerator = Math.Abs(numerator);
            Denominator = Math.Abs(denominator);
            FractionSign = sign;

            Simplify();
        }


        private void Simplify()
        {
            Console.WriteLine("[LOG] Викликано приватний метод Simplify класу SimpleFraction.");
            int gcd = GetGCD(Numerator, Denominator);
            if (gcd > 1)
            {
                Numerator /= gcd;
                Denominator /= gcd;
            }
        }


        private int GetGCD(int a, int b)
        {
            Console.WriteLine("[LOG] Викликано приватний метод GetGCD класу SimpleFraction.");
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

    }
}