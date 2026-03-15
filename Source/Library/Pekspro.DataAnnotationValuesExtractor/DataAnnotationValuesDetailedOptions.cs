using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;

namespace Pekspro.DataAnnotationValuesExtractor;

public readonly struct DataAnnotationValuesDetailedOptions : IEquatable<DataAnnotationValuesDetailedOptions>
{
    public readonly string Name;
    public readonly string FullFileName;
    public readonly ImmutableArray<TypeInformation> Types;
    public readonly string FilePath;
    public readonly string? Namespace;
    public readonly bool AddStringLength;
    public readonly bool AddRange;
    public readonly bool AddRequired;
    public readonly bool AddDisplay;
    public readonly bool AddDescription;
    public readonly bool AddMaxLength;
    public readonly bool AddMinLength;

    public DataAnnotationValuesDetailedOptions(
        string name,
        string? ns,
        string fullFileName,
        ImmutableArray<TypeInformation> types,
        bool addStringLength,
        bool addRange,
        bool addRequired,
        bool addDisplay = false,
        bool addDescription = false,
        bool addMaxLength = true,
        bool addMinLength = true
        )
    {
        Name = name;
        Namespace = ns;
        FullFileName = fullFileName;
        Types = types;

        if (fullFileName != string.Empty)
        {
            FilePath = Path.GetDirectoryName(fullFileName);
        }
        else
        {
            FilePath = string.Empty;
        }

        AddStringLength = addStringLength;
        AddRange = addRange;
        AddRequired = addRequired;
        AddDisplay = addDisplay;
        AddDescription = addDescription;
        AddMaxLength = addMaxLength;
        AddMinLength = addMinLength;
    }

    public bool Equals(DataAnnotationValuesDetailedOptions other)
    {
        return Name == other.Name
            && FullFileName == other.FullFileName
            && FilePath == other.FilePath
            && Namespace == other.Namespace
            && AddStringLength == other.AddStringLength
            && AddRange == other.AddRange
            && AddRequired == other.AddRequired
            && AddDisplay == other.AddDisplay
            && AddDescription == other.AddDescription
            && AddMaxLength == other.AddMaxLength
            && AddMinLength == other.AddMinLength
            && TypesEqual(Types, other.Types);
    }

    private static bool TypesEqual(ImmutableArray<TypeInformation> types1, ImmutableArray<TypeInformation> types2)
    {
        if (types1.IsDefault && types2.IsDefault)
        {
            return true;
        }
        
        if (types1.IsDefault || types2.IsDefault)
        {
            return false;
        }
        
        if (types1.Length != types2.Length)
        {
            return false;
        }

        for (int i = 0; i < types1.Length; i++)
        {
            if (!types1[i].Equals(types2[i]))
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is DataAnnotationValuesDetailedOptions other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (Name?.GetHashCode() ?? 0);
            hash = hash * 23 + (FullFileName?.GetHashCode() ?? 0);
            hash = hash * 23 + (FilePath?.GetHashCode() ?? 0);
            hash = hash * 23 + (Namespace?.GetHashCode() ?? 0);
            hash = hash * 23 + AddStringLength.GetHashCode();
            hash = hash * 23 + AddRange.GetHashCode();
            hash = hash * 23 + AddRequired.GetHashCode();
            hash = hash * 23 + AddDisplay.GetHashCode();
            hash = hash * 23 + AddDescription.GetHashCode();
            hash = hash * 23 + AddMaxLength.GetHashCode();
            hash = hash * 23 + AddMinLength.GetHashCode();
            
            if (!Types.IsDefault)
            {
                foreach (var type in Types)
                {
                    hash = hash * 23 + type.GetHashCode();
                }
            }
            
            return hash;
        }
    }

    public static bool operator ==(DataAnnotationValuesDetailedOptions left, DataAnnotationValuesDetailedOptions right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(DataAnnotationValuesDetailedOptions left, DataAnnotationValuesDetailedOptions right)
    {
        return !left.Equals(right);
    }
}
