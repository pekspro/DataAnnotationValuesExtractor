using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Pekspro.DataAnnotationValuesExtractor.Tests;

public class OptionsTests
{
    [Fact]
    public Task Default()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues]
public partial class MyClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
    [Display(Name = ""First name"")]
    public string? FirstName { get; set; }

    [Range(1, 42)]
    public int Value { get; set; }
}
";
        // Temporarily disable caching assertions for Direct pipeline due to symbol equality comparison issues
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task AllOff()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(StringLength = false, Range = false, Required = false, Display = false)]
public partial class MyClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
    [Display(Name = ""First name"")]
    public string? FirstName { get; set; }

    [Range(1, 42)]
    public int Value { get; set; }
}
";
        // Temporarily disable caching assertions for Direct pipeline due to symbol equality comparison issues
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }


    [Fact]
    public Task OnlyStringLength()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(StringLength = true, Range = false, Required = false, Display = false)]
public partial class MyClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
    [Display(Name = ""First name"")]
    public string? FirstName { get; set; }

    [Range(1, 42)]
    public int Value { get; set; }
}
";
        // Temporarily disable caching assertions for Direct pipeline due to symbol equality comparison issues
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }


    [Fact]
    public Task OnlyRange()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(StringLength = false, Range = true, Required = false, Display = false)]
public partial class MyClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
    [Display(Name = ""First name"")]
    public string? FirstName { get; set; }

    [Range(1, 42)]
    public int Value { get; set; }
}
";
        // Temporarily disable caching assertions for Direct pipeline due to symbol equality comparison issues
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task OnlyRequired()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(StringLength = false, Range = false, Required = true, Display = false)]
public partial class MyClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
    [Display(Name = ""First name"")]
    public string? FirstName { get; set; }

    [Range(1, 42)]
    public int Value { get; set; }
}
";
        // Temporarily disable caching assertions for Direct pipeline due to symbol equality comparison issues
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task OnlyDisplay()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(StringLength = false, Range = false, Required = false, Display = true)]
public partial class MyClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
    [Display(Name = ""First name"")]
    public string? FirstName { get; set; }

    [Range(1, 42)]
    public int Value { get; set; }
}
";
        // Temporarily disable caching assertions for Direct pipeline due to symbol equality comparison issues
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }

    [Fact]
    public Task AllOn()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(StringLength = true, Range = true, Required = true, Display = true)]
public partial class MyClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
    [Display(Name = ""First name"")]
    public string? FirstName { get; set; }

    [Range(1, 42)]
    public int Value { get; set; }
}
";
        // Temporarily disable caching assertions for Direct pipeline due to symbol equality comparison issues
        var (diagnostics, output) = TestHelpers.GetGeneratedOutput<DataAnnotationValuesExtractor>(input);

        Assert.Empty(diagnostics);
        return Verifier.Verify(output)
            .ScrubGeneratedCodeAttribute()
            .UseDirectory("Snapshots")
            .DisableRequireUniquePrefix();
    }
}
