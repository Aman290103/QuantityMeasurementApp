using System;

namespace QuantityMeasurementApp.Service
{
    public class QuantityMeasurementException : Exception
    {
        public QuantityMeasurementException() : base() { }

        public QuantityMeasurementException(string message) : base(message) { }

        public QuantityMeasurementException(string message, Exception innerException) : base(message, innerException) { }
    }
}
