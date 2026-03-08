using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Text;

namespace Pekspro.DataAnnotationValuesExtractor;

[Generator]
public class DataAnnotationValuesExtractor : IIncrementalGenerator
{
    private const string ConfigurationExtensionsAttribute = "Pekspro.DataAnnotationValuesExtractor.DataAnnotationValuesOptionsAttribute";

    private const string TypeExtensionsAttribute = "Pekspro.DataAnnotationValuesExtractor.DataAnnotationValuesToGenerateAttribute";

    private const string DirectAttribute = "Pekspro.DataAnnotationValuesExtractor.DataAnnotationValuesAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Handle classes with DataAnnotationValuesToGenerateAttribute (which may also have DataAnnotationValuesOptionsAttribute)
        IncrementalValuesProvider<DataAnnotationValuesDetailedOptions> valuesProvider = context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    TypeExtensionsAttribute,
                    predicate: (node, _) => node is ClassDeclarationSyntax,
                    transform: GetDataAnntationsValuesForCentralizedAttribute)
            .WithTrackingName(TrackingNames.InitialExtraction)
            .Where(static m => m is not null)
            .Select(static (m, _) => m!.Value)
            .WithTrackingName(TrackingNames.RemovingNulls);
        
        context.RegisterSourceOutput(valuesProvider,
             static (spc, toGenerate) => Execute(in toGenerate, spc));

        // Handle classes with DataAnnotationValuesAttribute directly on the type
        IncrementalValuesProvider<DataAnnotationValuesDetailedOptions> directValuesProvider = context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    DirectAttribute,
                    predicate: (node, _) => node is ClassDeclarationSyntax,
                    transform: GetDataAnntationsValuesForDirectAttribute)
            .WithTrackingName(TrackingNames.InitialExtractionDirect)
            .Where(static m => m is not null)
            .Select(static (m, _) => m!.Value)
            .WithTrackingName(TrackingNames.RemovingNullsDirect);

        context.RegisterSourceOutput(directValuesProvider,
             static (spc, toGenerate) => Execute(in toGenerate, spc));
    }

    static void Execute(in DataAnnotationValuesDetailedOptions toGenerate, SourceProductionContext context)
    {
        var result = SourceGenerationHelper.GenerateExtensionClass(toGenerate);

        context.AddSource(toGenerate.Name + ".g.cs", SourceText.From(result, Encoding.UTF8));
    }

    static DataAnnotationValuesDetailedOptions? GetDataAnntationsValuesForCentralizedAttribute(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        INamedTypeSymbol? symbol = context.TargetSymbol as INamedTypeSymbol;
        if (symbol is null)
        {
            // nothing to do if this type isn't available
            return null;
        }

        ct.ThrowIfCancellationRequested();

        // Get the path to the source file
        string filePath = context.SemanticModel.SyntaxTree.FilePath;

        string name = symbol.Name;
        string? nameSpace = null;
        if (!symbol.ContainingNamespace.IsGlobalNamespace)
        {
            nameSpace = symbol.ContainingNamespace?.ToDisplayString();
        }

        bool addStringLength = true;
        bool addRange = true;
        bool addRequired = false;
        bool addDisplay = false;
        bool addDescription = false;
        var typesBuilder = ImmutableArray.CreateBuilder<ITypeSymbol>();

        foreach (AttributeData attributeData in symbol.GetAttributes())
        {
            if (attributeData.AttributeClass?.Name == "DataAnnotationValuesOptionsAttribute" &&
                attributeData.AttributeClass.ToDisplayString() == ConfigurationExtensionsAttribute)
            {
                foreach (KeyValuePair<string, TypedConstant> namedArgument in attributeData.NamedArguments)
                {
                    if (namedArgument.Key == nameof(DataAnnotationValuesOptionsAttribute.StringLength)
                        && namedArgument.Value.Value is bool stringLength)
                    {
                        addStringLength = stringLength;
                    }

                    if (namedArgument.Key == nameof(DataAnnotationValuesOptionsAttribute.Range)
                        && namedArgument.Value.Value is bool range)
                    {
                        addRange = range;
                    }

                    if (namedArgument.Key == nameof(DataAnnotationValuesOptionsAttribute.Required)
                        && namedArgument.Value.Value is bool required)
                    {
                        addRequired = required;
                    }

                    if (namedArgument.Key == nameof(DataAnnotationValuesOptionsAttribute.Display)
                        && namedArgument.Value.Value is bool display)
                    {
                        addDisplay = display;
                    }

                    if (namedArgument.Key == nameof(DataAnnotationValuesOptionsAttribute.Description)
                        && namedArgument.Value.Value is bool description)
                    {
                        addDescription = description;
                    }
                }
            }
            else if (attributeData.AttributeClass?.Name == "DataAnnotationValuesToGenerateAttribute" &&
                attributeData.AttributeClass.ToDisplayString() == TypeExtensionsAttribute)
            {
                // Check constructor arguments first (new preferred syntax)
                if (attributeData.ConstructorArguments.Length > 0)
                {
                    if (attributeData.ConstructorArguments[0].Value is ITypeSymbol typeSymbol)
                    {
                        if (!typesBuilder.Any(a => SymbolEqualityComparer.Default.Equals(a, typeSymbol)))
                        {
                            typesBuilder.Add(typeSymbol);
                        }
                    }
                }
            }
        }

        return new DataAnnotationValuesDetailedOptions
            (
                name,
                nameSpace,
                filePath,
                ExtractTypeInformation(typesBuilder.ToImmutable(), addStringLength, addRange, addRequired, addDisplay, addDescription),
                addStringLength,
                addRange,
                addRequired,
                addDisplay,
                addDescription
            );
    }

    static DataAnnotationValuesDetailedOptions? GetDataAnntationsValuesForDirectAttribute(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        INamedTypeSymbol? symbol = context.TargetSymbol as INamedTypeSymbol;
        if (symbol is null)
        {
            // nothing to do if this type isn't available
            return null;
        }

        ct.ThrowIfCancellationRequested();

        // Get the path to the source file
        string filePath = context.SemanticModel.SyntaxTree.FilePath;

        string name = symbol.Name;
        string? nameSpace = null;
        if (!symbol.ContainingNamespace.IsGlobalNamespace)
        {
            nameSpace = symbol.ContainingNamespace?.ToDisplayString();
        }

        bool addStringLength = true;
        bool addRange = true;
        bool addRequired = false;
        bool addDisplay = false;
        bool addDescription = false;

        // Get configuration from the DataAnnotationValuesAttribute
        foreach (AttributeData attributeData in symbol.GetAttributes())
        {
            if (attributeData.AttributeClass?.Name == "DataAnnotationValuesAttribute" &&
                attributeData.AttributeClass.ToDisplayString() == DirectAttribute)
            {
                foreach (KeyValuePair<string, TypedConstant> namedArgument in attributeData.NamedArguments)
                {
                    if (namedArgument.Key == nameof(DataAnnotationValuesAttribute.StringLength)
                        && namedArgument.Value.Value is bool stringLength)
                    {
                        addStringLength = stringLength;
                    }

                    if (namedArgument.Key == nameof(DataAnnotationValuesAttribute.Range)
                        && namedArgument.Value.Value is bool range)
                    {
                        addRange = range;
                    }

                    if (namedArgument.Key == nameof(DataAnnotationValuesAttribute.Required)
                        && namedArgument.Value.Value is bool required)
                    {
                        addRequired = required;
                    }

                    if (namedArgument.Key == nameof(DataAnnotationValuesAttribute.Display)
                        && namedArgument.Value.Value is bool display)
                    {
                        addDisplay = display;
                    }

                    if (namedArgument.Key == nameof(DataAnnotationValuesAttribute.Description)
                        && namedArgument.Value.Value is bool description)
                    {
                        addDescription = description;
                    }
                }
            }
        }

        // For direct attribute, we generate for the type itself
        var types = ImmutableArray.Create<ITypeSymbol>(symbol);

        return new DataAnnotationValuesDetailedOptions
            (
                name,
                nameSpace,
                filePath,
                ExtractTypeInformation(types, addStringLength, addRange, addRequired, addDisplay, addDescription),
                addStringLength,
                addRange,
                addRequired,
                addDisplay,
                addDescription
            );
    }

    static ImmutableArray<TypeInformation> ExtractTypeInformation(
        ImmutableArray<ITypeSymbol> typeSymbols,
        bool includeStringLength,
        bool includeRange,
        bool includeRequired,
        bool includeDisplay,
        bool includeDescription)
    {
        var builder = ImmutableArray.CreateBuilder<TypeInformation>();

        foreach (var typeSymbol in typeSymbols)
        {
            var properties = typeSymbol.GetMembers().OfType<IPropertySymbol>().ToList();
            var propertyInfos = ImmutableArray.CreateBuilder<PropertyInformation>();

            foreach (var property in properties)
            {
                StringLengthInfo? stringLength = null;
                RangeInfo? range = null;
                bool isRequired = false;
                DisplayInfo? display = null;
                DescriptionInfo? description = null;

                // Check for StringLength attribute
                if (includeStringLength)
                {
                    var stringLengthAttr = property.GetAttributes()
                        .FirstOrDefault(a => a.AttributeClass?.Name == "StringLengthAttribute" &&
                                           a.AttributeClass?.ContainingNamespace?.ToDisplayString() == "System.ComponentModel.DataAnnotations");

                    if (stringLengthAttr != null && stringLengthAttr.ConstructorArguments.Length > 0)
                    {
                        var maxLength = (int)stringLengthAttr.ConstructorArguments[0].Value!;

                        // Check for MinimumLength named argument
                        var minLengthArg = stringLengthAttr.NamedArguments.FirstOrDefault(na => na.Key == "MinimumLength");
                        int minLength = 0;
                        if (minLengthArg.Key != null && minLengthArg.Value.Value is int minValue)
                        {
                            minLength = minValue;
                        }

                        stringLength = new StringLengthInfo(maxLength, minLength);
                    }
                }

                // Check for Range attribute
                if (includeRange)
                {
                    var rangeAttr = property.GetAttributes()
                        .FirstOrDefault(a => a.AttributeClass?.Name == "RangeAttribute" &&
                                           a.AttributeClass?.ContainingNamespace?.ToDisplayString() == "System.ComponentModel.DataAnnotations");

                    if (rangeAttr != null && rangeAttr.ConstructorArguments.Length >= 2)
                    {
                        var minimumValue = rangeAttr.ConstructorArguments[0].Value;
                        var maximumValue = rangeAttr.ConstructorArguments[1].Value;
                        var minimum = FormatValue(minimumValue);
                        var maximum = FormatValue(maximumValue);
                        var typeName = rangeAttr.ConstructorArguments[0].Type?.ToDisplayString() ?? "int";

                        // Check for MinimumIsExclusive and MaximumIsExclusive named arguments
                        var minIsExclusiveArg = rangeAttr.NamedArguments.FirstOrDefault(na => na.Key == "MinimumIsExclusive");
                        bool minIsExclusive = false;
                        if (minIsExclusiveArg.Key != null && minIsExclusiveArg.Value.Value is bool minExclValue)
                        {
                            minIsExclusive = minExclValue;
                        }

                        var maxIsExclusiveArg = rangeAttr.NamedArguments.FirstOrDefault(na => na.Key == "MaximumIsExclusive");
                        bool maxIsExclusive = false;
                        if (maxIsExclusiveArg.Key != null && maxIsExclusiveArg.Value.Value is bool maxExclValue)
                        {
                            maxIsExclusive = maxExclValue;
                        }

                        range = new RangeInfo(minimum, maximum, typeName, minIsExclusive, maxIsExclusive);
                    }
                }

                // Check for Required attribute
                if (includeRequired)
                {
                    var requiredAttr = property.GetAttributes()
                        .FirstOrDefault(a => a.AttributeClass?.Name == "RequiredAttribute" &&
                                           a.AttributeClass?.ContainingNamespace?.ToDisplayString() == "System.ComponentModel.DataAnnotations");

                    isRequired = requiredAttr != null;
                }

                // Check for Display attribute
                if (includeDisplay)
                {
                    var displayAttr = property.GetAttributes()
                        .FirstOrDefault(a => a.AttributeClass?.Name == "DisplayAttribute" &&
                                           a.AttributeClass?.ContainingNamespace?.ToDisplayString() == "System.ComponentModel.DataAnnotations");

                    if (displayAttr != null)
                    {
                        string? name = null;
                        string? shortName = null;
                        string? displayDescription = null;

                        // Extract named arguments
                        foreach (var namedArg in displayAttr.NamedArguments)
                        {
                            if (namedArg.Key == "Name" && namedArg.Value.Value is string nameValue)
                            {
                                name = nameValue;
                            }
                            else if (namedArg.Key == "ShortName" && namedArg.Value.Value is string shortNameValue)
                            {
                                shortName = shortNameValue;
                            }
                            else if (namedArg.Key == "Description" && namedArg.Value.Value is string descriptionValue)
                            {
                                displayDescription = descriptionValue;
                            }
                        }

                        // Only create DisplayInfo if at least one value is present
                        if (name != null || shortName != null || displayDescription != null)
                        {
                            display = new DisplayInfo(name, shortName, displayDescription);
                        }
                    }
                }

                // Check for Description attribute
                if (includeDescription)
                {
                    var descriptionAttr = property.GetAttributes()
                        .FirstOrDefault(a => a.AttributeClass?.Name == "DescriptionAttribute" &&
                                           a.AttributeClass?.ContainingNamespace?.ToDisplayString() == "System.ComponentModel");

                    if (descriptionAttr != null && descriptionAttr.ConstructorArguments.Length > 0)
                    {
                        string? text = descriptionAttr.ConstructorArguments[0].Value as string;

                        if (text != null)
                        {
                            description = new DescriptionInfo(text);
                        }
                    }
                }

                // Only add property if it has any attributes we're tracking
                if (stringLength.HasValue || range.HasValue || includeRequired || display.HasValue || description.HasValue)
                {
                    propertyInfos.Add(new PropertyInformation(
                        property.Name,
                        stringLength,
                        range,
                        isRequired,
                        display,
                        description));
                }
            }

            // Only add type if it has properties with attributes
            if (propertyInfos.Count > 0)
            {
                var typeName = typeSymbol.Name;
                var namespaceName = typeSymbol.ContainingNamespace?.IsGlobalNamespace == true
                    ? string.Empty
                    : typeSymbol.ContainingNamespace?.ToDisplayString() ?? string.Empty;

                builder.Add(new TypeInformation(typeName, namespaceName, propertyInfos.ToImmutable()));
            }
        }

        return builder.ToImmutable();
    }

    private static string FormatValue(object? value)
    {
        if (value == null)
            return "null";

        if (value is string s)
            return $"\"{s}\"";

        if (value is double d)
            return d.ToString(System.Globalization.CultureInfo.InvariantCulture) + "d";

        if (value is float f)
            return f.ToString(System.Globalization.CultureInfo.InvariantCulture) + "f";

        if (value is decimal dec)
            return dec.ToString(System.Globalization.CultureInfo.InvariantCulture) + "m";

        if (value is long l)
            return l.ToString() + "L";

        return value.ToString() ?? "null";
    }
}

