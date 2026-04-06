namespace Lab4;

public class oop1
{
    public class Membership
    {
        private readonly string _code;
        private string _ownerName;
        private decimal _cost;
        
        private static int _totalMembershipsCreated = 0;
        
        public Membership(string code, string ownerName, decimal cost)
        {
            _code = code;
            _ownerName = ownerName;
            _cost = cost;
            
            _totalMembershipsCreated++;
        }
        
        public decimal Cost => _cost;
        public static int TotalMembershipsCreated => _totalMembershipsCreated;
        
        public virtual string GetInfo()
        {
            return $"Код: {_code}, Власник: {_ownerName}, Вартість: {_cost} грн";
        }
        
        public bool IsExpensive()
        {
            return _cost > 1500;
        }
    }
    
    public class SingleVisit : Membership
    {
        private DateTime _visitDate;

        public SingleVisit(string code, string ownerName, decimal cost, DateTime visitDate) 
            : base(code, ownerName, cost)
        {
            _visitDate = visitDate;
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + $", Дата відвідування: {_visitDate.ToShortDateString()}";
        }
    }
    
    public class MonthlyPass : Membership
    {
        private int _validityDays;

        public MonthlyPass(string code, string ownerName, decimal cost, int validityDays) 
            : base(code, ownerName, cost)
        {
            _validityDays = validityDays;
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + $", Днів дії: {_validityDays}";
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            List<Membership> clubMemberships = new List<Membership>();
            
            clubMemberships.Add(new SingleVisit("SV-001", "Олександр", 350, DateTime.Now));
            clubMemberships.Add(new MonthlyPass("MP-001", "Марія", 1800, 30));
            clubMemberships.Add(new SingleVisit("SV-002", "Іван", 400, DateTime.Now.AddDays(1)));
            clubMemberships.Add(new MonthlyPass("MP-002", "Олена", 1200, 15));
            clubMemberships.Add(new MonthlyPass("MP-003", "Дмитро", 3500, 90));

            decimal totalCost = 0;
            int expensiveCount = 0;

            Console.WriteLine("=== СПИСОК АБОНЕМЕНТІВ ===");
            
            foreach (Membership membership in clubMemberships)
            {
                Console.WriteLine(membership.GetInfo());
                
                totalCost += membership.Cost;
                
                if (membership.IsExpensive())
                {
                    expensiveCount++;
                }
            }

            Console.WriteLine("\n=== СТАТИСТИКА КЛУБУ ===");
            Console.WriteLine($"Загальна кількість створених абонементів (через static): {Membership.TotalMembershipsCreated}");
            Console.WriteLine($"Загальна сума вартості всіх абонементів: {totalCost} грн");
            Console.WriteLine($"Кількість 'дорогих' абонементів (> 1500 грн): {expensiveCount}");
            
            Console.ReadLine();
        }
    }
}