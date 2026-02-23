using System;

namespace QuantityMeasurementApp.Core
{
    public class Feet
    {
        private readonly double value;

        public Feet(double value)
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

            if(obj.GetType() != typeof(Feet)) return false;

            Feet other = (Feet)obj;

            return this.value.CompareTo(other.value) == 0;
        }
        
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Feet feet1 = new Feet(1.0);
            Feet feet2 = new Feet(2.0);

            bool result = feet1.Equals(feet2);

            Console.WriteLine($"Equal : {result}");
        }
    }
}