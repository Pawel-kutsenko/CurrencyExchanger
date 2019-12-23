namespace WinFormsMVP.Model
{
    public class Customer
    {

        public string Name { get; set; }

        public string Currency { get; set; }

        public string Cash { get; set; }

        public override bool Equals(object obj)
        {
            Customer other = obj as Customer;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode()
                ^ Currency.GetHashCode()
                ^ Cash.GetHashCode();
        }

        public bool Equals(Customer other)
        {
            if (other == null)
                return false;

            return this.Name == other.Name
                && this.Currency == other.Currency
                && this.Cash == other.Cash;
        }
    }
}