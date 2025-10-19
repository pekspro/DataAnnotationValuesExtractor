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
        Assert.Equal(4, typeof(AllAnnotationsDirectClass.Annotations).GetNestedTypes().Length);
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

    // ===== All Annotations Centralized Approach Tests =====

    [Fact]
    public void AllAnnotationsCentralized_Class1_VerifyAllAnnotations()
    {
        // StringLength
        Assert.Equal(50, AllAnnotationsCentralizedClass1.Annotations.FullName.MaximumLength);
        Assert.Equal(0, AllAnnotationsCentralizedClass1.Annotations.FullName.MinimumLength);
        
        // Range
        Assert.Equal(18, AllAnnotationsCentralizedClass1.Annotations.Rank.Minimum);
        Assert.Equal(100, AllAnnotationsCentralizedClass1.Annotations.Rank.Maximum);
        
        // Required
        Assert.True(AllAnnotationsCentralizedClass1.Annotations.FullName.IsRequired);
        Assert.False(AllAnnotationsCentralizedClass1.Annotations.Rank.IsRequired);
    }

    [Fact]
    public void AllAnnotationsCentralized_Class2_VerifyAllAnnotations()
    {
        // StringLength
        Assert.Equal(100, AllAnnotationsCentralizedClass2.Annotations.Email.MaximumLength);
        Assert.Equal(0, AllAnnotationsCentralizedClass2.Annotations.Email.MinimumLength);
        
        // Range
        Assert.Equal(1, AllAnnotationsCentralizedClass2.Annotations.Priority.Minimum);
        Assert.Equal(5, AllAnnotationsCentralizedClass2.Annotations.Priority.Maximum);
        
        // Required
        Assert.True(AllAnnotationsCentralizedClass2.Annotations.Email.IsRequired);
        Assert.True(AllAnnotationsCentralizedClass2.Annotations.Priority.IsRequired);
    }

    [Fact]
    public void AllAnnotationsCentralized_VerifyNestedTypeCounts()
    {
        Assert.Equal(2, typeof(AllAnnotationsCentralizedClass1.Annotations).GetNestedTypes().Length);
        Assert.Equal(2, typeof(AllAnnotationsCentralizedClass2.Annotations).GetNestedTypes().Length);
    }
}



