# DataAnnotationValuesExtractor

A C# source generator that automatically extracts values from data annotation
attributes and exposes them as strongly-typed constants. Access your
`StringLength`, `Range`, `Required` and `Display` attribute values as constants
in your classes.

## Why Use This?

When working with data annotations, you often need to reference validation
constraints. A good way to solve it is to create constants. But it takes time to
do and makes your data models harder to read. And it's harder when your models
are auto generated like when you are scaffolding with Entity Framework.

This source generator creates the constants automatically for you. If you have
this model:

```csharp
public partial class Product
{
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }

    [Required]
    [Range(0.01, 999999.99)]
    public decimal Price { get; set; }
}
```

Constants will be generated that you can access like this:

```csharp
// Name length constraints
int maxNameLength = Product.Annotations.Name.MaximumLength; // 100
bool nameRequired = Product.Annotations.Name.IsRequired; // true

// Price constraints
double minPrice = Product.Annotations.Price.Minimum; // 0.01
double maxPrice = Product.Annotations.Price.Maximum; // 999999.99
```

## Usage Patterns

There are two ways to configure DataAnnotationValuesExtractor depending on your
needs:

### 1. Direct Approach

Apply `[DataAnnotationValues]` directly to each class you want to generate
constants for:

```csharp
[DataAnnotationValues(StringLength = true, Range = true, Required = true, Display = true)]
public partial class Product
{
    [Display(Name = "Product name")]
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }

    [Display(Name = "Product price")]
    [Required]
    [Range(0.01, 999999.99)]
    public decimal Price { get; set; }

    public string? Sku { get; set; }
}
```

### 2. Centralized Approach

Create a dummy class and use the `DataAnnotationValuesToGenerate` attribute for
each class you want to generate constants for. You can use the
`DataAnnotationValuesConfiguration` attribute to configure what to be generated.

```csharp
using Pekspro.DataAnnotationValuesExtractor;

[DataAnnotationValuesConfiguration(StringLength = true, Range = true, Required = true, Display = true)]
[DataAnnotationValuesToGenerate(typeof(Customer))]
[DataAnnotationValuesToGenerate(typeof(Order))]
[DataAnnotationValuesToGenerate(typeof(Product))]
partial class ValidationConstants
{
}
```

Your model classes remain unchanged.

This approach is especially useful when working with auto-generated models, such
as those created by Entity Framework scaffolding.

### Use the generated constants

No matter which approach your are using, you can access generated constants like
this:

```csharp
// Name
int maxNameLength = Product.Annotations.Name.MaximumLength; // 100
int minNameLength = Product.Annotations.Name.MinimumLength; // 0
bool nameRequired = Product.Annotations.Name.IsRequired; // true
string? nameDisplayName = Product.Annotations.Name.Display.Name; // Product name

// Price
double minPrice = Product.Annotations.Price.Minimum; // 0.01
double maxPrice = Product.Annotations.Price.Maximum; // 999999.99
bool priceRequired = Product.Annotations.Price.IsRequired;
string? priceDisplayName = Product.Annotations.Price.Display.Name; // Price name

// Sku
bool skuRequired = Product.Annotations.Sku.IsRequired; // false
```

## Installation

Add the package to your project:

```bash
dotnet add package Pekspro.DataAnnotationValuesExtractor
```

For optimal setup, configure the package reference in your `.csproj` to exclude
it from your output assembly:

```xml
<ItemGroup>
  <PackageReference Include="Pekspro.DataAnnotationValuesExtractor" Version="0.0.1" 
    PrivateAssets="all" ExcludeAssets="runtime" />
</ItemGroup>
```

## Links

You can find more information and can report issues on [GitHub](https://github.com/pekspro/DataAnnotationValuesExtractor).
