using Pekspro.DataAnnotationValuesExtractor;
using System;
using System.ComponentModel.DataAnnotations;

/*

Output:

Name length should be between 0 and 50 characters. Is required: True
Email length should be between 6 and 100 characters. Is required: False
Score should be in range: 1-100. Is required: False

*/

Console.WriteLine($"Name length should be between {Player.Annotations.Name.MinimumLength} and {Player.Annotations.Name.MaximumLength} characters. Is required: {Player.Annotations.Name.IsRequired}");
Console.WriteLine($"Email length should be between {Player.Annotations.Email.MinimumLength} and {Player.Annotations.Email.MaximumLength} characters. Is required: {Player.Annotations.Email.IsRequired}");
Console.WriteLine($"Score should be in range: {Player.Annotations.Score.Minimum}-{Player.Annotations.Score.Maximum}. Is required: {Player.Annotations.Score.IsRequired}");

[DataAnnotationValues(StringLength = true, Range = true, Required = true)]
public partial class Player
{
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(100, MinimumLength = 6)]
    public string? Email { get; set; }

    [Range(1, 100)]
    public int Score { get; set; }
}
