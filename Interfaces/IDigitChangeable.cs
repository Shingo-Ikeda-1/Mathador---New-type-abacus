using System;

public interface IDigitChangeable
{
    int Digit { get; set; }

    bool IsFiveOn { get; }

    int DigitPosition { get; }

    event Action<int, int> OnDigitChanged;
}
