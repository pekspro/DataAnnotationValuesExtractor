using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace Pekspro.DataAnnotationValuesExtractor.Tests;

public class DescriptionTests
{
    [Fact]
    public Task Description_BasicText()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel;

[DataAnnotationValues(Description = true)]
public partial class MyClass
{
    [Description(""The first name of the user"")]
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
    public Task Description_SpecialCharacters_Quotes()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel;

[DataAnnotationValues(Description = true)]
public partial class MyClass
{
    [Description(""Description with \""quotes\"" and 'apostrophes'"")]
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
    public Task Description_SpecialCharacters_Backslashes()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel;

[DataAnnotationValues(Description = true)]
public partial class MyClass
{
    [Description(""Path: C:\\Users\\Admin"")]
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
    public Task Description_MultiLine()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel;

[DataAnnotationValues(Description = true)]
public partial class MyClass
{
    [Description(@""Description with
newline"")]
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
    public Task Description_MultipleProperties()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel;

[DataAnnotationValues(Description = true)]
public partial class MyClass
{
    [Description(""The first name"")]
    public string? FirstName { get; set; }

    [Description(""The last name"")]
    public string? LastName { get; set; }

    public string? NoDescription { get; set; }
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
    public Task Description_CentralizedApproach()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel;

public partial class MyFirstClass
{
    [Description(""First name of the person"")]
    public string? FirstName { get; set; }

    [Description(""Last name of the person"")]
    public string? LastName { get; set; }
}

public partial class MySecondClass
{
    [Description(""Contact email address"")]
    public string? Email { get; set; }

    [Description(""Contact phone number"")]
    public string? Phone { get; set; }
}

[DataAnnotationValuesOptions(Description = true)]
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
    public Task Description_EmptyString()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel;

[DataAnnotationValues(Description = true)]
public partial class MyClass
{
    [Description("""")]
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
    public Task Description_WithDisplayCombined()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[DataAnnotationValues(Display = true, Description = true)]
public partial class MyClass
{
    [Display(Name = ""First Name"")]
    [Description(""The first name of the user"")]
    public string? FirstName { get; set; }

    [Display(Name = ""Last Name"", ShortName = ""LName"")]
    public string? LastName { get; set; }

    [Description(""Email only"")]
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
    public Task Description_OnClass()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel;

[DataAnnotationValues(Description = true)]
[Description(""A class representing a person"")]
public partial class MyClass
{
    [Description(""The first name of the user"")]
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
    public Task Description_OnClassOnly()
    {
        const string input = @"
using Pekspro.DataAnnotationValuesExtractor;
using System.ComponentModel;

[DataAnnotationValues(Description = true)]
[Description(""A class with no annotated properties"")]
public partial class MyClass
{
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
}
