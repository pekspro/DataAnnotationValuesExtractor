using System;
using System.Collections.Immutable;

namespace Pekspro.DataAnnotationValuesExtractor;

/// <summary>
/// Represents information about a type without holding symbol references.
/// This enables proper incremental generator caching.
/// </summary>
public readonly struct TypeInformation : IEquatable<TypeInformation>
{
    public readonly string TypeName;
    public readonly string Namespace;
    public readonly ImmutableArray<PropertyInformation> Properties;

    public TypeInformation(string typeName, string namespaceName, ImmutableArray<PropertyInformation> properties)
    {
        TypeName = typeName;
        Namespace = namespaceName;
        Properties = properties;
    }

    public bool Equals(TypeInformation other)
    {
        return TypeName == other.TypeName
            && Namespace == other.Namespace
            && PropertiesEqual(Properties, other.Properties);
    }

    private static bool PropertiesEqual(ImmutableArray<PropertyInformation> props1, ImmutableArray<PropertyInformation> props2)
    {
        if (props1.IsDefault && props2.IsDefault)
        {
            return true;
        }

        if (props1.IsDefault || props2.IsDefault)
        {
            return false;
        }

        if (props1.Length != props2.Length)
        {
            return false;
        }

        for (int i = 0; i < props1.Length; i++)
        {
            if (!props1[i].Equals(props2[i]))
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is TypeInformation other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (TypeName?.GetHashCode() ?? 0);
            hash = hash * 23 + (Namespace?.GetHashCode() ?? 0);

            if (!Properties.IsDefault)
            {
                foreach (var prop in Properties)
                {
                    hash = hash * 23 + prop.GetHashCode();
                }
            }

            return hash;
        }
    }

    public static bool operator ==(TypeInformation left, TypeInformation right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(TypeInformation left, TypeInformation right)
    {
        return !left.Equals(right);
    }
}

/// <summary>
/// Represents information about a property and its data annotation attributes.
/// </summary>
public readonly struct PropertyInformation : IEquatable<PropertyInformation>
{
    public readonly string PropertyName;
    public readonly StringLengthInfo? StringLength;
    public readonly RangeInfo? Range;
    public readonly bool IsRequired;
    public readonly DisplayInfo? Display;

    public PropertyInformation(string propertyName, StringLengthInfo? stringLength, RangeInfo? range, bool isRequired, DisplayInfo? display = null)
    {
        PropertyName = propertyName;
        StringLength = stringLength;
        Range = range;
        IsRequired = isRequired;
        Display = display;
    }

    public bool Equals(PropertyInformation other)
    {
        return PropertyName == other.PropertyName
            && Nullable.Equals(StringLength, other.StringLength)
            && Nullable.Equals(Range, other.Range)
            && IsRequired == other.IsRequired
            && Nullable.Equals(Display, other.Display);
    }

    public override bool Equals(object? obj)
    {
        return obj is PropertyInformation other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (PropertyName?.GetHashCode() ?? 0);
            hash = hash * 23 + (StringLength?.GetHashCode() ?? 0);
            hash = hash * 23 + (Range?.GetHashCode() ?? 0);
            hash = hash * 23 + IsRequired.GetHashCode();
            hash = hash * 23 + (Display?.GetHashCode() ?? 0);
            return hash;
        }
    }

    public static bool operator ==(PropertyInformation left, PropertyInformation right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(PropertyInformation left, PropertyInformation right)
    {
        return !left.Equals(right);
    }
}

/// <summary>
/// Represents StringLength attribute information.
/// </summary>
public readonly struct StringLengthInfo : IEquatable<StringLengthInfo>
{
    public readonly int MaximumLength;
    public readonly int MinimumLength;

    public StringLengthInfo(int maximumLength, int minimumLength)
    {
        MaximumLength = maximumLength;
        MinimumLength = minimumLength;
    }

    public bool Equals(StringLengthInfo other)
    {
        return MaximumLength == other.MaximumLength && MinimumLength == other.MinimumLength;
    }

    public override bool Equals(object? obj)
    {
        return obj is StringLengthInfo other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + MaximumLength.GetHashCode();
            hash = hash * 23 + MinimumLength.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(StringLengthInfo left, StringLengthInfo right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(StringLengthInfo left, StringLengthInfo right)
    {
        return !left.Equals(right);
    }
}

/// <summary>
/// Represents Range attribute information.
/// </summary>
public readonly struct RangeInfo : IEquatable<RangeInfo>
{
    public readonly string Minimum;
    public readonly string Maximum;
    public readonly string TypeName;
    public readonly bool MinimumIsExclusive;
    public readonly bool MaximumIsExclusive;

    public RangeInfo(string minimum, string maximum, string typeName, bool minimumIsExclusive, bool maximumIsExclusive)
    {
        Minimum = minimum;
        Maximum = maximum;
        TypeName = typeName;
        MinimumIsExclusive = minimumIsExclusive;
        MaximumIsExclusive = maximumIsExclusive;
    }

    public bool Equals(RangeInfo other)
    {
        return Minimum == other.Minimum
            && Maximum == other.Maximum
            && TypeName == other.TypeName
            && MinimumIsExclusive == other.MinimumIsExclusive
            && MaximumIsExclusive == other.MaximumIsExclusive;
    }

    public override bool Equals(object? obj)
    {
        return obj is RangeInfo other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (Minimum?.GetHashCode() ?? 0);
            hash = hash * 23 + (Maximum?.GetHashCode() ?? 0);
            hash = hash * 23 + (TypeName?.GetHashCode() ?? 0);
            hash = hash * 23 + MinimumIsExclusive.GetHashCode();
            hash = hash * 23 + MaximumIsExclusive.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(RangeInfo left, RangeInfo right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(RangeInfo left, RangeInfo right)
    {
        return !left.Equals(right);
    }
}

/// <summary>
/// Represents Display attribute information.
/// </summary>
public readonly struct DisplayInfo : IEquatable<DisplayInfo>
{
    public readonly string? Name;
    public readonly string? ShortName;
    public readonly string? Description;

    public DisplayInfo(string? name, string? shortName, string? description)
    {
        Name = name;
        ShortName = shortName;
        Description = description;
    }

    public bool Equals(DisplayInfo other)
    {
        return Name == other.Name
            && ShortName == other.ShortName
            && Description == other.Description;
    }

    public override bool Equals(object? obj)
    {
        return obj is DisplayInfo other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (Name?.GetHashCode() ?? 0);
            hash = hash * 23 + (ShortName?.GetHashCode() ?? 0);
            hash = hash * 23 + (Description?.GetHashCode() ?? 0);
            return hash;
        }
    }

    public static bool operator ==(DisplayInfo left, DisplayInfo right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(DisplayInfo left, DisplayInfo right)
    {
        return !left.Equals(right);
    }
}

