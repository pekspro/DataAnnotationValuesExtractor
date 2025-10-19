using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Pekspro.DataAnnotationValuesExtractor.Tests;

public class NamespaceTests
{
    [Fact]
    public Task InGlobalNamespace()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues]
public partial class MyClass
{
    [StringLength(10, MinimumLength = 2)]
    public string? FirstName { get; set; }

    [StringLength(20)]
    public string? LastName { get; set; }
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
    public Task InNamespace()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

namespace MyNamespace
{
    [DataAnnotationValues]
    public partial class MyClass
    {
        [StringLength(10, MinimumLength = 2)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }
    }
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
    public Task InSubNamespace()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

namespace MyNamespace
{
    namespace MySubNamespace
    {
        [DataAnnotationValues]
        public partial class MyClass
        {
            [StringLength(10, MinimumLength = 2)]
            public string? FirstName { get; set; }

            [StringLength(20)]
            public string? LastName { get; set; }
        }
    }
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
    public Task MultipleNamespace()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues]
public partial class MyClass
{
    [StringLength(10, MinimumLength = 2)]
    public string? FirstName { get; set; }

    [StringLength(20)]
    public string? LastName { get; set; }
}

namespace MyNamespace
{
    [DataAnnotationValues]
    public partial class MySecondClass
    {
        [StringLength(10, MinimumLength = 2)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }
    }
}


namespace MyNamespace
{
    namespace MySubNamespace
    {
        [DataAnnotationValues]
        public partial class MyThirdClass
        {
            [StringLength(10, MinimumLength = 2)]
            public string? FirstName { get; set; }

            [StringLength(20)]
            public string? LastName { get; set; }
        }
    }
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
    public Task Centralized()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel.DataAnnotations;

public partial class MyGlobalClass
{
    [Required]
    [StringLength(10, MinimumLength = 40)]
    public string? FirstName { get; set; }

    [StringLength(20)]
    public string? LastName { get; set; }
}

namespace MyNamespace
{
    public partial class MyNamespaceClass
    {
        [Required]
        [StringLength(10, MinimumLength = 40)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }
    }

    namespace MySubNamespace
    {
        public partial class MySubNamespaceClass
        {
            [Required]
            [StringLength(10, MinimumLength = 40)]
            public string? FirstName { get; set; }

            [StringLength(20)]
            public string? LastName { get; set; }
        }
    }
}


[DataAnnotationValuesOptions(StringLength = true, Range = true, Required = true)]
[DataAnnotationValuesToGenerate(typeof(MyGlobalClass))]
[DataAnnotationValuesToGenerate(typeof(MyNamespace.MyNamespaceClass))]
[DataAnnotationValuesToGenerate(typeof(MyNamespace.MySubNamespace.MySubNamespaceClass))]
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

