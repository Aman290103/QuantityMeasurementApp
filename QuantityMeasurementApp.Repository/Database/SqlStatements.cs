namespace QuantityMeasurementApp.Repository
{
    public static class SqlStatements
    {
        public const string Insert =
            "INSERT INTO dbo.QuantityMeasurements " +
            "(FirstOperand, SecondOperand, OperationType, MeasurementType, FinalResult, HasError, ErrorMessage, RecordedAt) " +
            "VALUES (@FirstOperand, @SecondOperand, @OperationType, @MeasurementType, @FinalResult, @HasError, @ErrorMessage, @RecordedAt)";

        public const string SelectAll =
            "SELECT Id, FirstOperand, SecondOperand, OperationType, MeasurementType, " +
            "       FinalResult, HasError, ErrorMessage, RecordedAt " +
            "FROM dbo.QuantityMeasurements " +
            "ORDER BY RecordedAt ASC";

        public const string Count =
            "SELECT COUNT(*) FROM dbo.QuantityMeasurements";
    }
}
