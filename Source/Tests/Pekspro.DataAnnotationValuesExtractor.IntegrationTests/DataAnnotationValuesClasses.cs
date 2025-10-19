using System.ComponentModel.DataAnnotations;
using Pekspro.DataAnnotationValuesExtractor;

namespace Pekspro.DataAnnotationValuesExtractor.IntegrationTests;

#pragma warning disable CS1591

// ===== StringLength Tests - Direct Approach =====

[DataAnnotationValues(StringLength = true)]
public partial class StringLengthDirectClass
{
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [StringLength(100, MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;

    [StringLength(20)]
    public string Email { get; set; } = string.Empty;
}

// ===== Range Tests - Direct Approach =====

[DataAnnotationValues(Range = true)]
public partial class RangeDirectClass
{
    [Range(1, 100)]
    public int Score { get; set; }

    [Range(18, 120)]
    public int Rank { get; set; }

    [Range(0.0, 100.0)]
    public double Percentage { get; set; }
}

// ===== Required Tests - Direct Approach =====

[DataAnnotationValues(Required = true)]
public partial class RequiredDirectClass
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;
}

// ===== Centralized Tests - Direct Approach =====

[DataAnnotationValues(StringLength = true, Range = true, Required = true)]
public partial class AllAnnotationsDirectClass
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Range(18, 120)]
    public int Rank { get; set; }

    [Required]
    [Range(0, 100)]
    public int Score { get; set; }
}

// ===== StringLength Tests - Centralized Approach =====

public partial class StringLengthCentralizedClass1
{
    [StringLength(30)]
    public string Title { get; set; } = string.Empty;

    [StringLength(200, MinimumLength = 20)]
    public string Content { get; set; } = string.Empty;
}

public partial class StringLengthCentralizedClass2
{
    [StringLength(15)]
    public string Username { get; set; } = string.Empty;

    [StringLength(50, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
}

[DataAnnotationValuesOptions(StringLength = true)]
[DataAnnotationValuesToGenerate(typeof(StringLengthCentralizedClass1))]
[DataAnnotationValuesToGenerate(typeof(StringLengthCentralizedClass2))]
partial class StringLengthCentralizedValues
{
}

// ===== Range Tests - Centralized Approach =====

public partial class RangeCentralizedClass1
{
    [Range(1, 10)]
    public int Rating { get; set; }

    [Range(0, 1000)]
    public int Points { get; set; }
}

public partial class RangeCentralizedClass2
{
    [Range(0.0, 5.0)]
    public double StarRating { get; set; }

    [Range(-100, 100)]
    public int Temperature { get; set; }
}

[DataAnnotationValuesOptions(Range = true)]
[DataAnnotationValuesToGenerate(typeof(RangeCentralizedClass1))]
[DataAnnotationValuesToGenerate(typeof(RangeCentralizedClass2))]
partial class RangeCentralizedValues
{
}

// ===== Required Tests - Centralized Approach =====

public partial class RequiredCentralizedClass1
{
    [Required]
    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;
}

public partial class RequiredCentralizedClass2
{
    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string Country { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;
}

[DataAnnotationValuesOptions(Required = true)]
[DataAnnotationValuesToGenerate(typeof(RequiredCentralizedClass1))]
[DataAnnotationValuesToGenerate(typeof(RequiredCentralizedClass2))]
partial class RequiredCentralizedValues
{
}

// ===== All Annotations - Centralized Approach =====

public partial class AllAnnotationsCentralizedClass1
{
    [Required]
    [StringLength(50)]
    public string FullName { get; set; } = string.Empty;

    [Range(18, 100)]
    public int Rank { get; set; }
}

public partial class AllAnnotationsCentralizedClass2
{
    [Required]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(1, 5)]
    public int Priority { get; set; }
}

[DataAnnotationValuesOptions(StringLength = true, Range = true, Required = true)]
[DataAnnotationValuesToGenerate(typeof(AllAnnotationsCentralizedClass1))]
[DataAnnotationValuesToGenerate(typeof(AllAnnotationsCentralizedClass2))]
partial class AllAnnotationsCentralizedValues
{
}

#pragma warning restore CS1591

