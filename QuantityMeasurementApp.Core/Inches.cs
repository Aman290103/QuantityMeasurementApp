namespace QuantityMeasurementApp.Core
{
    public class Inches
    {
        private readonly double value;

        public Inches(double value)
        {
            this.value = value;
        }

        public double Value
        {
            get { return value; }
        }

        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(this, obj)) return true;

            if(obj == null) return false;

            if(obj.GetType() != typeof(Inches)) return false;

            Inches other = (Inches)obj;

            return this.value.CompareTo(other.value) == 0;
        }
        
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}