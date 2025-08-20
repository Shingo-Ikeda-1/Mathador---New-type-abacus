public interface ITotalNumberHandler
{
    int AggregateNumber { get; set; }

    void DistributeTotalToChildren();

    void ReflectDigitChangeToTotal(int digitNumber, int digitPosition);

}
