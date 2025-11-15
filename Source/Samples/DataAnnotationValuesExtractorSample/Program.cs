using Pekspro.DataAnnotationValuesExtractor;
using System;
using System.ComponentModel.DataAnnotations;

/*
Output:

Name length should be between 0 and 50 characters. Is required: True
Email length should be between 6 and 100 characters. Is required: False
Score should be in range: 1-100. Is required: False

Name display name: Player Name
Email display name: Player E-mail
Score display name: Player Score
 */

Console.WriteLine($"Name length should be between {Player.Annotations.Name.MinimumLength} and {Player.Annotations.Name.MaximumLength} characters. Is required: {Player.Annotations.Name.IsRequired}");
Console.WriteLine($"Email length should be between {Player.Annotations.Email.MinimumLength} and {Player.Annotations.Email.MaximumLength} characters. Is required: {Player.Annotations.Email.IsRequired}");
Console.WriteLine($"Score should be in range: {Player.Annotations.Score.Minimum}-{Player.Annotations.Score.Maximum}. Is required: {Player.Annotations.Score.IsRequired}");
Console.WriteLine();
Console.WriteLine($"Name display name: {Player.Annotations.Name.Display.Name}");
Console.WriteLine($"Email display name: {Player.Annotations.Email.Display.Name}");
Console.WriteLine($"Score display name: {Player.Annotations.Score.Display.Name}");


[DataAnnotationValues(StringLength = true, Range = true, Required = true, Display = true)]
public partial class Player
{
    [Display(Name = "Player Name", ShortName = "Name", Description = "Name of player")]
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }

    [Display(Name = "Player E-mail")]
    [StringLength(100, MinimumLength = 6)]
    public string? Email { get; set; }

    [Display(Name = "Player Score")]
    [Range(1, 100)]
    public int Score { get; set; }
}
