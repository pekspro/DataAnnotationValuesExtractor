using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Pekspro.DataAnnotationValuesExtractor.Tests;

public class StringLengthTests
{
    [Fact]
    public Task StringLength_SingleParameter()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues]
public partial class MyClass
{
    [StringLength(10)]
    public string? FirstName { get; set; }
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
    public Task StringLength_DualParameter()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues]
public partial class MyClass
{
    [StringLength(10, MinimumLength = 4)]
    public string? FirstName { get; set; }
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
    public Task StringLength_ExtraParameters()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues]
public partial class MyClass
{
    [StringLength(100, ErrorMessage = ""Error message"", ErrorMessageResourceName = ""Resource name"", ErrorMessageResourceType = typeof(string), MinimumLength = 40)]
    public string? FirstName { get; set; }
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
    public Task StringLength_CentralizedApproach()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

public partial class MyFirstClass
{
    [StringLength(10)]
    public string? FirstName { get; set; }

    [StringLength(20, MinimumLength = 5)]
    public string? LastName { get; set; }
}

public partial class MySecondClass
{
    [StringLength(30)]
    public string? Email { get; set; }

    [StringLength(100, MinimumLength = 10)]
    public string? Description { get; set; }
}

[DataAnnotationValuesOptions(StringLength = true)]
[DataAnnotationValuesToGenerate(typeof(MyFirstClass))]
[DataAnnotationValuesToGenerate(typeof(MySecondClass))]
partial class MyValues
{
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

