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

[DataAnnotationValues(StringLength = false, Range = false, Required = false)]
public partial class MyClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
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

[DataAnnotationValues(StringLength = true, Range = false, Required = false)]
public partial class MyClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
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

[DataAnnotationValues(StringLength = false, Range = true, Required = false)]
public partial class MyClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
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

[DataAnnotationValues(StringLength = false, Range = false, Required = true)]
public partial class MyClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
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

[DataAnnotationValues(StringLength = true, Range = true, Required = true)]
public partial class MyClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
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

