using System;

public enum CalculationType
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

[Serializable]
public class StageInfo
{
    public int Id { get; private set; }
    public CalculationType CalculationType { get; private set; }
    public Difficulty Difficulty { get; private set; }

    public static string GetSymbol(CalculationType CalculationType)
    {
        switch (CalculationType)
        {
            case CalculationType.Addition:
                return "+";
            case CalculationType.Subtraction:
                return "-";
            case CalculationType.Multiplication:
                return "Å~";
            case CalculationType.Division:
                return "ÅÄ";
            default:
                throw new ArgumentOutOfRangeException(nameof(CalculationType), CalculationType, null);
        }
    }

    public StageInfo(int id, CalculationType calculationType, Difficulty difficulty)
    {
        Id = id;
        CalculationType = calculationType;
        Difficulty = difficulty;
    }
}