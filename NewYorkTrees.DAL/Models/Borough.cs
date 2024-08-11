namespace NewYorkTrees.DAL.Models;

public class Borough : IEquatable<Borough>
{
    private Borough(string value) { Value = value; }

    private string Value { get; }

    public static Borough Bronx => new Borough("Bronx");
    public static Borough Brooklyn => new("Brooklyn");
    public static Borough Manhattan => new("Manhattan");
    public static Borough Queens => new("Queens");
    public static Borough StatenIsland => new("Staten Island");

    public override string ToString()
    {
        return Value;
    }

    public static bool TryParse(string input, out Borough? output)
    {
        switch (input)
        {
            case "Bronx":
                output = new Borough("Bronx");
                return true;
            case "Brooklyn":
                output = new Borough("Brooklyn");
                return true;
            case "Manhattan":
                output = new Borough("Manhattan");
                return true;
            case "Queens":
                output = new Borough("Queens");
                return true;
            case "Staten Island":
            case "StatenIsland":
                output = new Borough("Staten Island");
                return true;
            default:
                output = default;
                return false;
        }
    }

    public bool Equals(Borough? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Borough)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}