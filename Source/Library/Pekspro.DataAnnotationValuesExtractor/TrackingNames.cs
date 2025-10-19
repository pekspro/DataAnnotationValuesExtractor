namespace Pekspro.DataAnnotationValuesExtractor;

/// <summary>
/// Names that are attached to incremental generator stages for tracking
/// </summary>
public class TrackingNames
{
    public const string InitialExtraction = nameof(InitialExtraction);
    public const string RemovingNulls = nameof(RemovingNulls);
    public const string InitialExtractionTypeAttribute = nameof(InitialExtractionTypeAttribute);
    public const string RemovingNullsTypeAttribute = nameof(RemovingNullsTypeAttribute);
    public const string InitialExtractionDirect = nameof(InitialExtractionDirect);
    public const string RemovingNullsDirect = nameof(RemovingNullsDirect);
}
