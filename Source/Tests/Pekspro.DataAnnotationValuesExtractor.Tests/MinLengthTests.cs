using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Pekspro.DataAnnotationValuesExtractor.Tests;

public class MinLengthTests
{
    [Fact]
    public Task MinLength_SingleProperty()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(MinLength = true)]
public partial class MyClass
{
    [MinLength(3)]
    public string[]? Tags { get; set; }
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
    public Task MinLength_MultipleProperties()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(MinLength = true)]
public partial class MyClass
{
    [MinLength(1)]
    public string[]? Tags { get; set; }

    [MinLength(5)]
    public int[]? Scores { get; set; }

    public string? NoMinLength { get; set; }
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
    public Task MinLength_CentralizedApproach()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

public partial class MyFirstClass
{
    [MinLength(1)]
    public string[]? Tags { get; set; }

    [MinLength(2)]
    public byte[]? Data { get; set; }
}

public partial class MySecondClass
{
    [MinLength(3)]
    public string[]? Names { get; set; }

    [MinLength(10)]
    public int[]? Values { get; set; }
}

[DataAnnotationValuesOptions(MinLength = true)]
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
}
