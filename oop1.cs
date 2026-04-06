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

        
}