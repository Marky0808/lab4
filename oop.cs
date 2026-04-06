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

     
        public void Print()
        {
            Console.WriteLine("[LOG] Викликано метод Print класу SimpleFraction.");
            Console.WriteLine($"Результат: {this.ToString()}");
        }

        
        public SimpleFraction Add(SimpleFraction other)
        {
            Console.WriteLine("[LOG] Викликано метод Add класу SimpleFraction.");
            int sign1 = this.FractionSign.IsPositive ? 1 : -1;
            int sign2 = other.FractionSign.IsPositive ? 1 : -1;

            int num1 = this.Numerator * sign1;
            int num2 = other.Numerator * sign2;

            int newNumerator = (num1 * other.Denominator) + (num2 * this.Denominator);
            int newDenominator = this.Denominator * other.Denominator;

            Sign newSign = new Sign(newNumerator >= 0);
            return new SimpleFraction(Math.Abs(newNumerator), newDenominator, newSign);
        }
        
        public SimpleFraction Subtract(SimpleFraction other)
        {
            Console.WriteLine("[LOG] Викликано метод Subtract класу SimpleFraction.");
            // Віднімання - це додавання дробу з протилежним знаком
            Sign invertedSign = new Sign(!other.FractionSign.IsPositive);
            SimpleFraction invertedOther = new SimpleFraction(other.Numerator, other.Denominator, invertedSign);
            return Add(invertedOther);
        }
        
        public SimpleFraction Multiply(SimpleFraction other)
        {
            Console.WriteLine("[LOG] Викликано метод Multiply класу SimpleFraction.");
            int newNumerator = this.Numerator * other.Numerator;
            int newDenominator = this.Denominator * other.Denominator;
            bool isPositive = (this.FractionSign.IsPositive == other.FractionSign.IsPositive);
            
            return new SimpleFraction(newNumerator, newDenominator, new Sign(isPositive));
        }
        
        public SimpleFraction Divide(SimpleFraction other)
        {
            Console.WriteLine("[LOG] Викликано метод Divide класу SimpleFraction.");
            if (other.Numerator == 0) throw new DivideByZeroException("Ділення на нуль.");
            
            int newNumerator = this.Numerator * other.Denominator;
            int newDenominator = this.Denominator * other.Numerator;
            bool isPositive = (this.FractionSign.IsPositive == other.FractionSign.IsPositive);
            
            return new SimpleFraction(newNumerator, newDenominator, new Sign(isPositive));
        }
        
        public override bool Equals(object obj)
        {
            Console.WriteLine("[LOG] Викликано метод Equals класу SimpleFraction.");
            if (obj is SimpleFraction other)
            {
                return this.Numerator == other.Numerator &&
                       this.Denominator == other.Denominator &&
                       this.FractionSign.Equals(other.FractionSign);
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            Console.WriteLine("[LOG] Викликано метод GetHashCode класу SimpleFraction.");
            return HashCode.Combine(Numerator, Denominator, FractionSign);
        }
        
        public override string ToString()
        {
            Console.WriteLine("[LOG] Викликано метод ToString класу SimpleFraction.");
            string signStr = FractionSign.IsPositive ? "" : "-"; // Плюс не пишемо для краси
            if (Numerator == 0) return "0";
            if (Denominator == 1) return $"{signStr}{Numerator}";
            return $"{signStr}{Numerator}/{Denominator}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== СТВОРЕННЯ ДРОБІВ ===");
            // Створюємо 1/2
            Sign signPositive = new Sign(true);
            SimpleFraction fraction1 = new SimpleFraction(1, 2, signPositive);

            // Створюємо -1/4
            Sign signNegative = new Sign(false);
            SimpleFraction fraction2 = new SimpleFraction(1, 4, signNegative);

            Console.WriteLine("\n=== ВИВЕДЕННЯ НА ЕКРАН ===");
            fraction1.Print();
            fraction2.Print();

            Console.WriteLine("\n=== ОПЕРАЦІЯ ДОДАВАННЯ (1/2 + -1/4) ===");
            SimpleFraction sum = fraction1.Add(fraction2);
            sum.Print();

            Console.WriteLine("\n=== ОПЕРАЦІЯ МНОЖЕННЯ (1/2 * -1/4) ===");
            SimpleFraction product = fraction1.Multiply(fraction2);
            product.Print();

            Console.WriteLine("\n=== ПЕРЕВІРКА EQUALS ===");
            SimpleFraction fraction3 = new SimpleFraction(2, 4, new Sign(true)); // Скоротиться до 1/2
            bool areEqual = fraction1.Equals(fraction3);
            Console.WriteLine($"Дроби рівні? {areEqual}");

            Console.ReadLine();
        }
    }
}