using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

// ===== Display Tests - Direct Approach =====

[DataAnnotationValues(Display = true)]
public partial class DisplayDirectClass
{
    [Display(Name = "User Name", ShortName = "Name", Description = "The user's full name")]
    public string FullName { get; set; } = string.Empty;

    [Display(Name = "Email Address", ShortName = "Email", Description = "The user's email with \"quotes\"")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Phone Number", ShortName = null, Description = null)]
    public string Phone { get; set; } = string.Empty;
}

// ===== Display Tests - Centralized Approach =====

public partial class DisplayCentralizedClass1
{
    [Display(Name = "First Name", ShortName = "First", Description = "User's first name")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name", ShortName = "Last", Description = "User's last name with \"special\" characters")]
    public string LastName { get; set; } = string.Empty;
}

public partial class DisplayCentralizedClass2
{
  [Display(Name = "Address Line", ShortName = "Addr", Description = null)]
    public string Address { get; set; } = string.Empty;

    [Display(Name = "City", ShortName = null, Description = "The city name")]
    public string City { get; set; } = string.Empty;

    [Display(Name = "Postal Code", ShortName = null, Description = null)]
    public string PostalCode { get; set; } = string.Empty;
}

[DataAnnotationValuesOptions(Display = true)]
[DataAnnotationValuesToGenerate(typeof(DisplayCentralizedClass1))]
[DataAnnotationValuesToGenerate(typeof(DisplayCentralizedClass2))]
partial class DisplayCentralizedValues
{
}


// ===== All Annotations - Direct Approach =====

[DataAnnotationValues(StringLength = true, Range = true, Required = true, Display = true)]
public partial class AllAnnotationsDirectClass
{
    [Required]
    [StringLength(50)]
    [Display(Name = "Product Name", ShortName = "Name", Description = "The product's display name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Range(18, 120)]
    public int Rank { get; set; }

    [Required]
    [Range(0, 100)]
    public int Score { get; set; }

    [StringLength(500)]
    [Display(Name = "Product Description", ShortName = "Desc", Description = "Detailed description with \"quotes\" and 'apostrophes'")]
    public string Description { get; set; } = string.Empty;

    [Range(0, 1000)]
    [Display(Name = "Price", ShortName = "Cost", Description = null)]
    public decimal Price { get; set; }

    [Required]
    [Display(Name = "In Stock", ShortName = null, Description = null)]
    public bool InStock { get; set; }
}

// ===== Description Tests - Direct Approach =====

[DataAnnotationValues(Description = true)]
public partial class DescriptionDirectClass
{
    [System.ComponentModel.Description("The user's full name")]
    public string FullName { get; set; } = string.Empty;

    [System.ComponentModel.Description("The user's email with \"quotes\"")]
    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;
}

// ===== Description Tests - Centralized Approach =====

public partial class DescriptionCentralizedClass1
{
    [System.ComponentModel.Description("User's first name")]
    public string FirstName { get; set; } = string.Empty;

    [System.ComponentModel.Description("User's last name with \"special\" characters")]
    public string LastName { get; set; } = string.Empty;
}

public partial class DescriptionCentralizedClass2
{
    [System.ComponentModel.Description("The address line")]
    public string Address { get; set; } = string.Empty;

    [System.ComponentModel.Description("The city name")]
    public string City { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;
}

[DataAnnotationValuesOptions(Description = true)]
[DataAnnotationValuesToGenerate(typeof(DescriptionCentralizedClass1))]
[DataAnnotationValuesToGenerate(typeof(DescriptionCentralizedClass2))]
partial class DescriptionCentralizedValues
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

    [Required]
    [StringLength(100)]
    [Display(Name = "Author Name", ShortName = "Author", Description = "Name of the author")]
    [Description("This is the mame of the author")]
    public string AuthorName { get; set; } = string.Empty;

    [Range(1900, 2100)]
    [Display(Name = "Publication Year", ShortName = "Year", Description = "Year \"published\" in")]
    [Description("This is the year when the product was published")]
    public int PublicationYear { get; set; }
}

public partial class AllAnnotationsCentralizedClass2
{
    [Required]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(1, 5)]
    public int Priority { get; set; }

    [Required]
    [StringLength(200)]
    [Display(Name = "Review Text", ShortName = "Review", Description = null)]
    [Description("Name of the product")]
    public string ReviewText { get; set; } = string.Empty;

    [Range(1, 5)]
    [Display(Name = "Rating", ShortName = null, Description = "Rating from 1 to 5 stars")]
    [Description("Rating of the product")]
    public int Rating { get; set; }
}


[DataAnnotationValuesOptions(StringLength = true, Range = true, Required = true, Display = true, Description = true)]
[DataAnnotationValuesToGenerate(typeof(AllAnnotationsCentralizedClass1))]
[DataAnnotationValuesToGenerate(typeof(AllAnnotationsCentralizedClass2))]
partial class AllAnnotationsCentralizedValues
{
}

#pragma warning restore CS1591

