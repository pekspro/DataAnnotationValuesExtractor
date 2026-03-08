using Xunit;
using System.ComponentModel.DataAnnotations;
using Pekspro.DataAnnotationValuesExtractor;

namespace Pekspro.DataAnnotationValuesExtractor.IntegrationTests;

public class DataAnnotationValuesTest
{
    // ===== StringLength Direct Approach Tests =====

    [Fact]
    public void StringLengthDirect_VerifyMaximumLength()
    {
        Assert.Equal(50, StringLengthDirectClass.Annotations.Name.MaximumLength);
        Assert.Equal(100, StringLengthDirectClass.Annotations.Description.MaximumLength);
        Assert.Equal(20, StringLengthDirectClass.Annotations.Email.MaximumLength);
    }

    [Fact]
    public void StringLengthDirect_VerifyMinimumLength()
    {
        Assert.Equal(0, StringLengthDirectClass.Annotations.Name.MinimumLength);
        Assert.Equal(10, StringLengthDirectClass.Annotations.Description.MinimumLength);
        Assert.Equal(0, StringLengthDirectClass.Annotations.Email.MinimumLength);
    }

    [Fact]
    public void StringLengthDirect_VerifyNestedTypeCount()
    {
        Assert.Equal(3, typeof(StringLengthDirectClass.Annotations).GetNestedTypes().Length);
    }

    // ===== Range Direct Approach Tests =====

    [Fact]
    public void RangeDirect_VerifyMinimumValues()
    {
        Assert.Equal(1, RangeDirectClass.Annotations.Score.Minimum);
        Assert.Equal(18, RangeDirectClass.Annotations.Rank.Minimum);
        Assert.Equal(0.0, RangeDirectClass.Annotations.Percentage.Minimum);
    }

    [Fact]
    public void RangeDirect_VerifyMaximumValues()
    {
        Assert.Equal(100, RangeDirectClass.Annotations.Score.Maximum);
        Assert.Equal(120, RangeDirectClass.Annotations.Rank.Maximum);
        Assert.Equal(100.0, RangeDirectClass.Annotations.Percentage.Maximum);
    }

    [Fact]
    public void RangeDirect_VerifyExclusiveFlags()
    {
        Assert.False(RangeDirectClass.Annotations.Score.MinimumIsExclusive);
        Assert.False(RangeDirectClass.Annotations.Score.MaximumIsExclusive);
        Assert.False(RangeDirectClass.Annotations.Rank.MinimumIsExclusive);
        Assert.False(RangeDirectClass.Annotations.Rank.MaximumIsExclusive);
    }

    [Fact]
    public void RangeDirect_VerifyNestedTypeCount()
    {
        Assert.Equal(3, typeof(RangeDirectClass.Annotations).GetNestedTypes().Length);
    }

    // ===== Required Direct Approach Tests =====

    [Fact]
    public void RequiredDirect_VerifyIsRequired()
    {
        Assert.True(RequiredDirectClass.Annotations.FirstName.IsRequired);
        Assert.True(RequiredDirectClass.Annotations.LastName.IsRequired);
    }

    [Fact]
    public void RequiredDirect_VerifyNestedTypeCount()
    {
        // Only 2 nested types: FirstName and LastName (MiddleName has no annotations)
        Assert.Equal(3, typeof(RequiredDirectClass.Annotations).GetNestedTypes().Length);
    }

    // ===== All Annotations Direct Approach Tests =====

    [Fact]
    public void AllAnnotationsDirect_VerifyStringLength()
    {
        Assert.Equal(50, AllAnnotationsDirectClass.Annotations.Name.MaximumLength);
        Assert.Equal(0, AllAnnotationsDirectClass.Annotations.Name.MinimumLength);
        Assert.Equal(100, AllAnnotationsDirectClass.Annotations.Email.MaximumLength);
        Assert.Equal(0, AllAnnotationsDirectClass.Annotations.Email.MinimumLength);
    }

    [Fact]
    public void AllAnnotationsDirect_VerifyRange()
    {
        Assert.Equal(18, AllAnnotationsDirectClass.Annotations.Rank.Minimum);
        Assert.Equal(120, AllAnnotationsDirectClass.Annotations.Rank.Maximum);
        Assert.Equal(0, AllAnnotationsDirectClass.Annotations.Score.Minimum);
        Assert.Equal(100, AllAnnotationsDirectClass.Annotations.Score.Maximum);
    }

    [Fact]
    public void AllAnnotationsDirect_VerifyRequired()
    {
        Assert.True(AllAnnotationsDirectClass.Annotations.Name.IsRequired);
        Assert.False(AllAnnotationsDirectClass.Annotations.Email.IsRequired);
        Assert.False(AllAnnotationsDirectClass.Annotations.Rank.IsRequired);
        Assert.True(AllAnnotationsDirectClass.Annotations.Score.IsRequired);
    }

    [Fact]
    public void AllAnnotationsDirect_VerifyNestedTypeCount()
    {
        Assert.Equal(8, typeof(AllAnnotationsDirectClass.Annotations).GetNestedTypes().Length);
    }

    // ===== StringLength Centralized Approach Tests =====

    [Fact]
    public void StringLengthCentralized_Class1_VerifyMaximumLength()
    {
        Assert.Equal(30, StringLengthCentralizedClass1.Annotations.Title.MaximumLength);
        Assert.Equal(200, StringLengthCentralizedClass1.Annotations.Content.MaximumLength);
    }

    [Fact]
    public void StringLengthCentralized_Class1_VerifyMinimumLength()
    {
        Assert.Equal(0, StringLengthCentralizedClass1.Annotations.Title.MinimumLength);
        Assert.Equal(20, StringLengthCentralizedClass1.Annotations.Content.MinimumLength);
    }

    [Fact]
    public void StringLengthCentralized_Class2_VerifyMaximumLength()
    {
        Assert.Equal(15, StringLengthCentralizedClass2.Annotations.Username.MaximumLength);
        Assert.Equal(50, StringLengthCentralizedClass2.Annotations.Password.MaximumLength);
    }

    [Fact]
    public void StringLengthCentralized_Class2_VerifyMinimumLength()
    {
        Assert.Equal(0, StringLengthCentralizedClass2.Annotations.Username.MinimumLength);
        Assert.Equal(8, StringLengthCentralizedClass2.Annotations.Password.MinimumLength);
    }

    // ===== Range Centralized Approach Tests =====

    [Fact]
    public void RangeCentralized_Class1_VerifyRanges()
    {
        Assert.Equal(1, RangeCentralizedClass1.Annotations.Rating.Minimum);
        Assert.Equal(10, RangeCentralizedClass1.Annotations.Rating.Maximum);
        Assert.Equal(0, RangeCentralizedClass1.Annotations.Points.Minimum);
        Assert.Equal(1000, RangeCentralizedClass1.Annotations.Points.Maximum);
    }

    [Fact]
    public void RangeCentralized_Class2_VerifyRanges()
    {
        Assert.Equal(0.0, RangeCentralizedClass2.Annotations.StarRating.Minimum);
        Assert.Equal(5.0, RangeCentralizedClass2.Annotations.StarRating.Maximum);
        Assert.Equal(-100, RangeCentralizedClass2.Annotations.Temperature.Minimum);
        Assert.Equal(100, RangeCentralizedClass2.Annotations.Temperature.Maximum);
    }

    [Fact]
    public void RangeCentralized_VerifyExclusiveFlags()
    {
        Assert.False(RangeCentralizedClass1.Annotations.Rating.MinimumIsExclusive);
        Assert.False(RangeCentralizedClass1.Annotations.Rating.MaximumIsExclusive);
        Assert.False(RangeCentralizedClass2.Annotations.Temperature.MinimumIsExclusive);
        Assert.False(RangeCentralizedClass2.Annotations.Temperature.MaximumIsExclusive);
    }

    // ===== Required Centralized Approach Tests =====

    [Fact]
    public void RequiredCentralized_Class1_VerifyRequired()
    {
        Assert.True(RequiredCentralizedClass1.Annotations.Email.IsRequired);
        Assert.True(RequiredCentralizedClass1.Annotations.Address.IsRequired);

        // Phone has no annotation, so it won't be in Annotations
        Assert.Equal(3, typeof(RequiredCentralizedClass1.Annotations).GetNestedTypes().Length);
    }

    [Fact]
    public void RequiredCentralized_Class2_VerifyRequired()
    {
        Assert.True(RequiredCentralizedClass2.Annotations.City.IsRequired);
        Assert.True(RequiredCentralizedClass2.Annotations.Country.IsRequired);

        // PostalCode has no annotation, so it won't be in Annotations
        Assert.Equal(3, typeof(RequiredCentralizedClass2.Annotations).GetNestedTypes().Length);
    }

    // ===== Display Direct Approach Tests =====

    [Fact]
    public void DisplayDirect_VerifyDisplayName()
    {
        Assert.Equal("User Name", DisplayDirectClass.Annotations.FullName.Display.Name);
        Assert.Equal("Email Address", DisplayDirectClass.Annotations.Email.Display.Name);
        Assert.Equal("Phone Number", DisplayDirectClass.Annotations.Phone.Display.Name);
    }

    [Fact]
    public void DisplayDirect_VerifyDisplayShortName()
    {
        Assert.Equal("Name", DisplayDirectClass.Annotations.FullName.Display.ShortName);
        Assert.Equal("Email", DisplayDirectClass.Annotations.Email.Display.ShortName);
        Assert.Null(DisplayDirectClass.Annotations.Phone.Display.ShortName);
    }

    [Fact]
    public void DisplayDirect_VerifyDisplayDescription()
    {
        Assert.Equal("The user's full name", DisplayDirectClass.Annotations.FullName.Display.Description);
        Assert.Equal("The user's email with \"quotes\"", DisplayDirectClass.Annotations.Email.Display.Description);
        Assert.Null(DisplayDirectClass.Annotations.Phone.Display.Description);
    }

    [Fact]
    public void DisplayDirect_VerifyNestedTypeCount()
    {
        Assert.Equal(3, typeof(DisplayDirectClass.Annotations).GetNestedTypes().Length);
    }

    // ===== Display Centralized Approach Tests =====

    [Fact]
    public void DisplayCentralized_Class1_VerifyDisplayNames()
    {
        Assert.Equal("First Name", DisplayCentralizedClass1.Annotations.FirstName.Display.Name);
        Assert.Equal("Last Name", DisplayCentralizedClass1.Annotations.LastName.Display.Name);
    }

    [Fact]
    public void DisplayCentralized_Class1_VerifyDisplayShortNames()
    {
        Assert.Equal("First", DisplayCentralizedClass1.Annotations.FirstName.Display.ShortName);
        Assert.Equal("Last", DisplayCentralizedClass1.Annotations.LastName.Display.ShortName);
    }

    [Fact]
    public void DisplayCentralized_Class1_VerifyDisplayDescriptions()
    {
        Assert.Equal("User's first name", DisplayCentralizedClass1.Annotations.FirstName.Display.Description);
        Assert.Equal("User's last name with \"special\" characters", DisplayCentralizedClass1.Annotations.LastName.Display.Description);
    }

    [Fact]
    public void DisplayCentralized_Class2_VerifyDisplayNames()
    {
        Assert.Equal("Address Line", DisplayCentralizedClass2.Annotations.Address.Display.Name);
        Assert.Equal("City", DisplayCentralizedClass2.Annotations.City.Display.Name);
        Assert.Equal("Postal Code", DisplayCentralizedClass2.Annotations.PostalCode.Display.Name);
    }

    [Fact]
    public void DisplayCentralized_Class2_VerifyDisplayShortNames()
    {
        Assert.Equal("Addr", DisplayCentralizedClass2.Annotations.Address.Display.ShortName);
        Assert.Null(DisplayCentralizedClass2.Annotations.City.Display.ShortName);
        Assert.Null(DisplayCentralizedClass2.Annotations.PostalCode.Display.ShortName);
    }

    [Fact]
    public void DisplayCentralized_Class2_VerifyDisplayDescriptions()
    {
        Assert.Null(DisplayCentralizedClass2.Annotations.Address.Display.Description);
        Assert.Equal("The city name", DisplayCentralizedClass2.Annotations.City.Display.Description);
        Assert.Null(DisplayCentralizedClass2.Annotations.PostalCode.Display.Description);
    }

    [Fact]
    public void DisplayCentralized_VerifyNestedTypeCounts()
    {
        Assert.Equal(2, typeof(DisplayCentralizedClass1.Annotations).GetNestedTypes().Length);
        Assert.Equal(3, typeof(DisplayCentralizedClass2.Annotations).GetNestedTypes().Length);
    }


    // ===== Description Direct Approach Tests =====

    [Fact]
    public void DescriptionDirect_VerifyDescriptionText()
    {
        Assert.Equal("The user's full name", DescriptionDirectClass.Annotations.FullName.Description.Text);
        Assert.Equal("The user's email with \"quotes\"", DescriptionDirectClass.Annotations.Email.Description.Text);
    }

    [Fact]
    public void DescriptionDirect_VerifyNestedTypeCount()
    {
        // Only 2 nested types: FullName and Email (Phone has no annotations)
        Assert.Equal(2, typeof(DescriptionDirectClass.Annotations).GetNestedTypes().Length);
    }

    // ===== Description Centralized Approach Tests =====

    [Fact]
    public void DescriptionCentralized_Class1_VerifyDescriptionText()
    {
        Assert.Equal("User's first name", DescriptionCentralizedClass1.Annotations.FirstName.Description.Text);
        Assert.Equal("User's last name with \"special\" characters", DescriptionCentralizedClass1.Annotations.LastName.Description.Text);
    }

    [Fact]
    public void DescriptionCentralized_Class2_VerifyDescriptionText()
    {
        Assert.Equal("The address line", DescriptionCentralizedClass2.Annotations.Address.Description.Text);
        Assert.Equal("The city name", DescriptionCentralizedClass2.Annotations.City.Description.Text);
    }

    [Fact]
    public void DescriptionCentralized_VerifyNestedTypeCounts()
    {
        Assert.Equal(2, typeof(DescriptionCentralizedClass1.Annotations).GetNestedTypes().Length);
        // Only 2 nested types: Address and City (PostalCode has no Description)
        Assert.Equal(2, typeof(DescriptionCentralizedClass2.Annotations).GetNestedTypes().Length);
    }

    // ===== MaxLength Direct Approach Tests =====

    [Fact]
    public void MaxLengthDirect_VerifyMaxLengthValues()
    {
        Assert.Equal(10, MaxLengthDirectClass.Annotations.Tags.MaxLength);
        Assert.Equal(50, MaxLengthDirectClass.Annotations.Categories.MaxLength);
        Assert.Equal(100, MaxLengthDirectClass.Annotations.Scores.MaxLength);
    }

    [Fact]
    public void MaxLengthDirect_VerifyNestedTypeCount()
    {
        Assert.Equal(3, typeof(MaxLengthDirectClass.Annotations).GetNestedTypes().Length);
    }

    // ===== MinLength Direct Approach Tests =====

    [Fact]
    public void MinLengthDirect_VerifyMinLengthValues()
    {
        Assert.Equal(1, MinLengthDirectClass.Annotations.Tags.MinLength);
        Assert.Equal(2, MinLengthDirectClass.Annotations.Categories.MinLength);
        Assert.Equal(5, MinLengthDirectClass.Annotations.Scores.MinLength);
    }

    [Fact]
    public void MinLengthDirect_VerifyNestedTypeCount()
    {
        Assert.Equal(3, typeof(MinLengthDirectClass.Annotations).GetNestedTypes().Length);
    }

    // ===== MaxLength Centralized Approach Tests =====

    [Fact]
    public void MaxLengthCentralized_Class1_VerifyMaxLengthValues()
    {
        Assert.Equal(25, MaxLengthCentralizedClass1.Annotations.Items.MaxLength);
        Assert.Equal(100, MaxLengthCentralizedClass1.Annotations.Data.MaxLength);
    }

    [Fact]
    public void MaxLengthCentralized_Class2_VerifyMaxLengthValues()
    {
        Assert.Equal(10, MaxLengthCentralizedClass2.Annotations.Names.MaxLength);
        Assert.Equal(50, MaxLengthCentralizedClass2.Annotations.Values.MaxLength);
    }

    [Fact]
    public void MaxLengthCentralized_VerifyNestedTypeCounts()
    {
        Assert.Equal(2, typeof(MaxLengthCentralizedClass1.Annotations).GetNestedTypes().Length);
        Assert.Equal(2, typeof(MaxLengthCentralizedClass2.Annotations).GetNestedTypes().Length);
    }

    // ===== MinLength Centralized Approach Tests =====

    [Fact]
    public void MinLengthCentralized_Class1_VerifyMinLengthValues()
    {
        Assert.Equal(1, MinLengthCentralizedClass1.Annotations.Items.MinLength);
        Assert.Equal(5, MinLengthCentralizedClass1.Annotations.Data.MinLength);
    }

    [Fact]
    public void MinLengthCentralized_Class2_VerifyMinLengthValues()
    {
        Assert.Equal(2, MinLengthCentralizedClass2.Annotations.Names.MinLength);
        Assert.Equal(3, MinLengthCentralizedClass2.Annotations.Values.MinLength);
    }

    [Fact]
    public void MinLengthCentralized_VerifyNestedTypeCounts()
    {
        Assert.Equal(2, typeof(MinLengthCentralizedClass1.Annotations).GetNestedTypes().Length);
        Assert.Equal(2, typeof(MinLengthCentralizedClass2.Annotations).GetNestedTypes().Length);
    }

    // ===== All Annotations With Direct Approach Tests =====

    [Fact]
    public void AllAnnotationsWithDisplayDirect_VerifyStringLength()
    {
        Assert.Equal(50, AllAnnotationsDirectClass.Annotations.Name.MaximumLength);
        Assert.Equal(0, AllAnnotationsDirectClass.Annotations.Name.MinimumLength);
        Assert.Equal(500, AllAnnotationsDirectClass.Annotations.Description.MaximumLength);
        Assert.Equal(0, AllAnnotationsDirectClass.Annotations.Description.MinimumLength);
    }

    [Fact]
    public void AllAnnotationsWithDisplayDirect_VerifyRange()
    {
        Assert.Equal(0, AllAnnotationsDirectClass.Annotations.Price.Minimum);
        Assert.Equal(1000, AllAnnotationsDirectClass.Annotations.Price.Maximum);
    }

    [Fact]
    public void AllAnnotationsWithDisplayDirect_VerifyRequired()
    {
        Assert.True(AllAnnotationsDirectClass.Annotations.Name.IsRequired);
        Assert.False(AllAnnotationsDirectClass.Annotations.Description.IsRequired);
        Assert.False(AllAnnotationsDirectClass.Annotations.Price.IsRequired);
        Assert.True(AllAnnotationsDirectClass.Annotations.InStock.IsRequired);
    }

    [Fact]
    public void AllAnnotationsWithDisplayDirect_VerifyDisplayNames()
    {
        Assert.Equal("Product Name", AllAnnotationsDirectClass.Annotations.Name.Display.Name);
        Assert.Equal("Product Description", AllAnnotationsDirectClass.Annotations.Description.Display.Name);
        Assert.Equal("Price", AllAnnotationsDirectClass.Annotations.Price.Display.Name);
        Assert.Equal("In Stock", AllAnnotationsDirectClass.Annotations.InStock.Display.Name);
    }

    [Fact]
    public void AllAnnotationsWithDisplayDirect_VerifyDisplayShortNames()
    {
        Assert.Equal("Name", AllAnnotationsDirectClass.Annotations.Name.Display.ShortName);
        Assert.Equal("Desc", AllAnnotationsDirectClass.Annotations.Description.Display.ShortName);
        Assert.Equal("Cost", AllAnnotationsDirectClass.Annotations.Price.Display.ShortName);
        Assert.Null(AllAnnotationsDirectClass.Annotations.InStock.Display.ShortName);
    }

    [Fact]
    public void AllAnnotationsWithDisplayDirect_VerifyDisplayDescriptions()
    {
        Assert.Equal("The product's display name", AllAnnotationsDirectClass.Annotations.Name.Display.Description);
        Assert.Equal("Detailed description with \"quotes\" and 'apostrophes'", AllAnnotationsDirectClass.Annotations.Description.Display.Description);
        Assert.Null(AllAnnotationsDirectClass.Annotations.Price.Display.Description);
        Assert.Null(AllAnnotationsDirectClass.Annotations.InStock.Display.Description);
    }

    [Fact]
    public void AllAnnotationsWithDisplayDirect_VerifyMaxLengthMinLength()
    {
        Assert.Equal(20, AllAnnotationsDirectClass.Annotations.Tags.MaxLength);
        Assert.Equal(1, AllAnnotationsDirectClass.Annotations.Tags.MinLength);
    }

    [Fact]
    public void AllAnnotationsWithDisplayDirect_VerifyNestedTypeCount()
    {
        Assert.Equal(8, typeof(AllAnnotationsDirectClass.Annotations).GetNestedTypes().Length);
    }

    // ===== All Annotations Centralized Approach Tests =====

    [Fact]
    public void AllAnnotationsCentralized_Class1_VerifyAllAnnotations()
    {
        // StringLength
        Assert.Equal(50, AllAnnotationsCentralizedClass1.Annotations.FullName.MaximumLength);
        Assert.Equal(0, AllAnnotationsCentralizedClass1.Annotations.FullName.MinimumLength);
        Assert.Equal(100, AllAnnotationsCentralizedClass1.Annotations.AuthorName.MaximumLength);
        Assert.Equal(0, AllAnnotationsCentralizedClass1.Annotations.AuthorName.MinimumLength);

        // Range
        Assert.Equal(18, AllAnnotationsCentralizedClass1.Annotations.Rank.Minimum);
        Assert.Equal(100, AllAnnotationsCentralizedClass1.Annotations.Rank.Maximum);
        Assert.Equal(1900, AllAnnotationsCentralizedClass1.Annotations.PublicationYear.Minimum);
        Assert.Equal(2100, AllAnnotationsCentralizedClass1.Annotations.PublicationYear.Maximum);

        // Required
        Assert.True(AllAnnotationsCentralizedClass1.Annotations.FullName.IsRequired);
        Assert.False(AllAnnotationsCentralizedClass1.Annotations.Rank.IsRequired);
        Assert.True(AllAnnotationsCentralizedClass1.Annotations.AuthorName.IsRequired);
        Assert.False(AllAnnotationsCentralizedClass1.Annotations.PublicationYear.IsRequired);

        // Display
        Assert.Equal("Author Name", AllAnnotationsCentralizedClass1.Annotations.AuthorName.Display.Name);
        Assert.Equal("Author", AllAnnotationsCentralizedClass1.Annotations.AuthorName.Display.ShortName);
        Assert.Equal("Name of the author", AllAnnotationsCentralizedClass1.Annotations.AuthorName.Display.Description);
        Assert.Equal("This is the mame of the author", AllAnnotationsCentralizedClass1.Annotations.AuthorName.Description.Text);

        Assert.Equal("Publication Year", AllAnnotationsCentralizedClass1.Annotations.PublicationYear.Display.Name);
        Assert.Equal("Year", AllAnnotationsCentralizedClass1.Annotations.PublicationYear.Display.ShortName);
        Assert.Equal("Year \"published\" in", AllAnnotationsCentralizedClass1.Annotations.PublicationYear.Display.Description);
        Assert.Equal("This is the year when the product was published", AllAnnotationsCentralizedClass1.Annotations.PublicationYear.Description.Text);

        // MaxLength / MinLength
        Assert.Equal(20, AllAnnotationsCentralizedClass1.Annotations.Tags.MaxLength);
        Assert.Equal(1, AllAnnotationsCentralizedClass1.Annotations.Tags.MinLength);
    }

    [Fact]
    public void AllAnnotationsCentralized_Class2_VerifyAllAnnotations()
    {
        // StringLength
        Assert.Equal(100, AllAnnotationsCentralizedClass2.Annotations.Email.MaximumLength);
        Assert.Equal(0, AllAnnotationsCentralizedClass2.Annotations.Email.MinimumLength);
        Assert.Equal(200, AllAnnotationsCentralizedClass2.Annotations.ReviewText.MaximumLength);
        Assert.Equal(0, AllAnnotationsCentralizedClass2.Annotations.ReviewText.MinimumLength);

        // Range
        Assert.Equal(1, AllAnnotationsCentralizedClass2.Annotations.Priority.Minimum);
        Assert.Equal(5, AllAnnotationsCentralizedClass2.Annotations.Priority.Maximum);
        Assert.Equal(1, AllAnnotationsCentralizedClass2.Annotations.Rating.Minimum);
        Assert.Equal(5, AllAnnotationsCentralizedClass2.Annotations.Rating.Maximum);

        // Required
        Assert.True(AllAnnotationsCentralizedClass2.Annotations.Email.IsRequired);
        Assert.True(AllAnnotationsCentralizedClass2.Annotations.Priority.IsRequired);
        Assert.True(AllAnnotationsCentralizedClass2.Annotations.ReviewText.IsRequired);
        Assert.False(AllAnnotationsCentralizedClass2.Annotations.Rating.IsRequired);

        // Display
        Assert.Equal("Review Text", AllAnnotationsCentralizedClass2.Annotations.ReviewText.Display.Name);
        Assert.Equal("Review", AllAnnotationsCentralizedClass2.Annotations.ReviewText.Display.ShortName);
        Assert.Null(AllAnnotationsCentralizedClass2.Annotations.ReviewText.Display.Description);
        Assert.Equal("Name of the product", AllAnnotationsCentralizedClass2.Annotations.ReviewText.Description.Text);

        Assert.Equal("Rating", AllAnnotationsCentralizedClass2.Annotations.Rating.Display.Name);
        Assert.Null(AllAnnotationsCentralizedClass2.Annotations.Rating.Display.ShortName);
        Assert.Equal("Rating from 1 to 5 stars", AllAnnotationsCentralizedClass2.Annotations.Rating.Display.Description);
        Assert.Equal("Rating of the product", AllAnnotationsCentralizedClass2.Annotations.Rating.Description.Text);

        // MaxLength
        Assert.Equal(10, AllAnnotationsCentralizedClass2.Annotations.Labels.MaxLength);
    }

    [Fact]
    public void AllAnnotationsCentralized_VerifyNestedTypeCounts()
    {
        Assert.Equal(5, typeof(AllAnnotationsCentralizedClass1.Annotations).GetNestedTypes().Length);
        Assert.Equal(5, typeof(AllAnnotationsCentralizedClass2.Annotations).GetNestedTypes().Length);
    }
}







