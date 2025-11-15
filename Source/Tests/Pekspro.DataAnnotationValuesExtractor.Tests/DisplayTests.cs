using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Pekspro.DataAnnotationValuesExtractor.Tests;

public class DisplayTests
{
    [Fact]
    public Task Display_NameOnly()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(Display = true)]
public partial class MyClass
{
    [Display(Name = ""First Name"")]
    public string? FirstName { get; set; }
}
";
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task Display_AllProperties()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(Display = true)]
public partial class MyClass
{
    [Display(Name = ""First Name"", ShortName = ""FName"", Description = ""The user's first name"")]
    public string? FirstName { get; set; }
}
";
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task Display_ShortNameAndDescription()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(Display = true)]
public partial class MyClass
{
    [Display(ShortName = ""Email"", Description = ""Contact email address"")]
    public string? EmailAddress { get; set; }
}
";
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task Display_SpecialCharacters_Quotes()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(Display = true)]
public partial class MyClass
{
    [Display(Name = ""Name with \""quotes\"""", ShortName = ""Short \""name\"""", Description = ""Description with \""quotes\"" and 'apostrophes'"")]
    public string? Property { get; set; }
}
";
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task Display_SpecialCharacters_Backslashes()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(Display = true)]
public partial class MyClass
{
    [Display(Name = ""Path: C:\\Users\\Admin"", Description = ""Use \\ for backslash"")]
    public string? FilePath { get; set; }
}
";
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task Display_MultiLine()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(Display = true)]
public partial class MyClass
{
    [Display(Name = @""Name with
newline"", Description = ""Tab\there"")]
    public string? Property { get; set; }
}
";
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .ScrubLineBreaks()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task Display_MultipleProperties()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(Display = true)]
public partial class MyClass
{
    [Display(Name = ""First Name"")]
    public string? FirstName { get; set; }

    [Display(Name = ""Last Name"", ShortName = ""LName"")]
    public string? LastName { get; set; }

    [Display(Description = ""Email only"")]
    public string? Email { get; set; }

    public string? NoDisplay { get; set; }
}
";
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task Display_ExtraParameters()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(Display = true)]
public partial class MyClass
{
    [Display(Name = ""Product Name"", ShortName = ""Name"", Description = ""Name of the product"", Prompt = ""Enter name"", GroupName = ""General"", Order = 1)]
    public string? ProductName { get; set; }
}
";
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task Display_CentralizedApproach()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

public partial class MyFirstClass
{
    [Display(Name = ""First Name"")]
    public string? FirstName { get; set; }

    [Display(Name = ""Last Name"", ShortName = ""LName"")]
    public string? LastName { get; set; }
}

public partial class MySecondClass
{
    [Display(Name = ""Email Address"", Description = ""Contact email"")]
    public string? Email { get; set; }

    [Display(Name = ""Phone Number"", ShortName = ""Phone"", Description = ""Contact phone number"")]
    public string? Phone { get; set; }
}

[DataAnnotationValuesOptions(Display = true)]
[DataAnnotationValuesToGenerate(typeof(MyFirstClass))]
[DataAnnotationValuesToGenerate(typeof(MySecondClass))]
partial class MyValues
{
}
";
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task Display_EmptyStrings()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(Display = true)]
public partial class MyClass
{
    [Display(Name = """", ShortName = """", Description = """")]
    public string? Property { get; set; }
}
";
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .ScrubLineBreaks()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }
}
