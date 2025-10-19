using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Pekspro.DataAnnotationValuesExtractor.Tests;

public class EdgeCaseTests
{
    [Fact]
    public Task NoAnnotations_DirectApproach()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues]
public partial class MyClass
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public int Rank { get; set; }
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
    public Task NoAnnotations_CentralizedApproach()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

public partial class MyFirstClass
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
}

public partial class MySecondClass
{
    public int Rank { get; set; }
    
    public string? Email { get; set; }
}

[DataAnnotationValuesOptions(StringLength = true, Range = true, Required = true)]
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
    public Task NoProperties_DirectApproach()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues]
public partial class MyClass
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
    public Task NoProperties_CentralizedApproach()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

public partial class MyEmptyClass
{
}

[DataAnnotationValuesOptions(StringLength = true)]
[DataAnnotationValuesToGenerate(typeof(MyEmptyClass))]
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
    public Task MixedPropertiesWithAndWithoutAnnotations()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(StringLength = true, Range = true, Required = true)]
public partial class MyClass
{
    [StringLength(50)]
    public string? Name { get; set; }
    
    public string? NickName { get; set; }
    
    [Range(0, 100)]
    public int Score { get; set; }
    
    public int Rating { get; set; }
    
    [Required]
    public string? Email { get; set; }
    
    public string? Phone { get; set; }
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
    public Task OnlyFieldsNoProperties()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues]
public partial class MyClass
{
    [StringLength(50)]
    public string? name;
    
    [Range(0, 100)]
    public int score;
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
    public Task PrivateAndInternalProperties()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues]
public partial class MyClass
{
    [StringLength(50)]
    public string? PublicName { get; set; }
    
    [StringLength(30)]
    private string? PrivateName { get; set; }
    
    [Range(0, 100)]
    internal int InternalScore { get; set; }
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
    public Task AnnotationsDisabledByConfiguration()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(StringLength = false, Range = false, Required = false)]
public partial class MyClass
{
    [StringLength(50)]
    public string? Name { get; set; }
    
    [Range(0, 100)]
    public int Score { get; set; }
    
    [Required]
    public string? Email { get; set; }
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
    public Task ExistingContants()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues]
public partial class MyClass
{
    public const int PublicNameMaxLength = 100;

    public const int PrivateNameMaxLength = 30;
    
    public const int MinScore = 0;

    public const int MaxScore = 100;

    [StringLength(PublicNameMaxLength)]
    public string? PublicName { get; set; }
    
    [StringLength(PrivateNameMaxLength)]
    private string? PrivateName { get; set; }
    
    [Range(MinScore, MaxScore)]
    internal int InternalScore { get; set; }
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

