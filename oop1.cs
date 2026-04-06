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

   
}