using Pekspro.DataAnnotationValuesExtractor;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/*
Output:

Name length should be between 0 and 50 characters. Is required: True
E-mail length should be between 6 and 100 characters. Is required: False
Score should be in range: 1-100. Is required: False
Tags min length: 1, max length: 10

Name display name: Player Name
E-mail display name: Player E-mail
Score display name: Player Score

Name description: Name of the player
E-mail description: E-mail of the player
Score description: Score of the player
 */

Console.WriteLine($"Name length should be between {Player.Annotations.Name.MinimumLength} and {Player.Annotations.Name.MaximumLength} characters. Is required: {Player.Annotations.Name.IsRequired}");
Console.WriteLine($"E-mail length should be between {Player.Annotations.Email.MinimumLength} and {Player.Annotations.Email.MaximumLength} characters. Is required: {Player.Annotations.Email.IsRequired}");
Console.WriteLine($"Score should be in range: {Player.Annotations.Score.Minimum}-{Player.Annotations.Score.Maximum}. Is required: {Player.Annotations.Score.IsRequired}");
Console.WriteLine($"Tags min length: {Player.Annotations.Tags.MinLength}, max length: {Player.Annotations.Tags.MaxLength}");
Console.WriteLine();
Console.WriteLine($"Name display name: {Player.Annotations.Name.Display.Name}");
Console.WriteLine($"E-mail display name: {Player.Annotations.Email.Display.Name}");
Console.WriteLine($"Score display name: {Player.Annotations.Score.Display.Name}");
Console.WriteLine();
Console.WriteLine($"Name description: {Player.Annotations.Name.Description.Text}");
Console.WriteLine($"E-mail description: {Player.Annotations.Email.Description.Text}");
Console.WriteLine($"Score description: {Player.Annotations.Score.Description.Text}");


[DataAnnotationValues(StringLength = true, MinLength = true, MaxLength = true, Range = true, Required = true, Display = true, Description = true)]
public partial class Player
{
    [Display(Name = "Player Name", ShortName = "Name", Description = "Name of player")]
    [Description("Name of the player")]
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }

    [Display(Name = "Player E-mail")]
    [Description("E-mail of the player")]
    [StringLength(100, MinimumLength = 6)]
    public string? Email { get; set; }

    [Display(Name = "Player Score")]
    [Description("Score of the player")]
    [Range(1, 100)]
    public int Score { get; set; }

    [MinLength(1)]
    [MaxLength(10)]
    public string[]? Tags { get; set; }
}
