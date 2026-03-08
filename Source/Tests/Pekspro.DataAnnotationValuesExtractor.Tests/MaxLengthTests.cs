using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Pekspro.DataAnnotationValuesExtractor.Tests;

public class MaxLengthTests
{
    [Fact]
    public Task MaxLength_SingleProperty()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(MaxLength = true)]
public partial class MyClass
{
    [MaxLength(10)]
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
    public Task MaxLength_MultipleProperties()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(MaxLength = true)]
public partial class MyClass
{
    [MaxLength(10)]
    public string[]? Tags { get; set; }

    [MaxLength(50)]
    public int[]? Scores { get; set; }

    public string? NoMaxLength { get; set; }
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
    public Task MaxLength_CentralizedApproach()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

public partial class MyFirstClass
{
    [MaxLength(10)]
    public string[]? Tags { get; set; }

    [MaxLength(20)]
    public byte[]? Data { get; set; }
}

public partial class MySecondClass
{
    [MaxLength(30)]
    public string[]? Names { get; set; }

    [MaxLength(100)]
    public int[]? Values { get; set; }
}

[DataAnnotationValuesOptions(MaxLength = true)]
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
