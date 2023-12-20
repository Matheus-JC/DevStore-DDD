using System.Text.RegularExpressions;

namespace DevStore.Common.DomainObjects;

public class AssertionConcern
{

    public static void AssertArgumentEquals(object object1, object object2, string message)
    {
        if(!object1.Equals(object2))
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentFalse(bool boolValue, string message)
    {
        if (boolValue)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentLength(string stringValue, int maximum, string message)
    {
        int length = stringValue.Trim().Length;

        if (length > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentLength(string stringValue, int minimum, int maximum, string message)
    {
        int length = stringValue.Trim().Length;

        if (length < minimum || length > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentMinimumValue(int value, int minimumValue, string message)
    {
        if (value < minimumValue)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentMaximumValue(int value, int maximumValue, string message)
    {
        if (value > maximumValue)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentGreaterThanZero(int value, string message)
    {
        if(value <= 0)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentIsPositive(int value, string message)
    {
        if (value < 0)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentGreaterThanZero(float value, string message)
    {
        if (value <= 0)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentGreaterThanZero(decimal value, string message)
    {
        if (value <= 0)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentMatches(string pattern, string stringValue, string message)
    {
        var regex = new Regex(pattern);

        if (!regex.IsMatch(stringValue))
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentNotEmpty(string stringValue, string message)
    {
        if (string.IsNullOrEmpty(stringValue))
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentNotEquals(object object1, object object2, string message)
    {
        if (object1.Equals(object2))
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentNotNull(object object1, string message)
    {
        if (object1 == null)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentRange(double value, double minimum, double maximum, string message)
    {
        if (value < minimum || value > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentRange(float value, float minimum, float maximum, string message)
    {
        if (value < minimum || value > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentRange(int value, int minimum, int maximum, string message)
    {
        if (value < minimum || value > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentRange(long value, long minimum, long maximum, string message)
    {
        if (value < minimum || value > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentTrue(bool boolValue, string message)
    {
        if (!boolValue)
        {
            throw new DomainException(message);
        }
    }
}
