namespace Pekspro.DataAnnotationValuesExtractor
{
    /// <summary>
    /// Indicates that data annotation values should be generated for this class.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    [System.Diagnostics.Conditional("PEKSPRO_DATAANNOTATIONSVALUESGENERATOR_PRESERVE_ATTRIBUTES")]
    public class DataAnnotationValuesAttribute : System.Attribute
    {
        /// <summary>
        /// Whether StringLength attribute values should be included.
        /// Is set to true by default.
        /// </summary>
        public bool StringLength { get; set; } = true;

        /// <summary>
        /// Whether Range attribute values should be included.
        /// Is set to true by default.
        /// </summary>
        public bool Range { get; set; } = true;

        /// <summary>
        /// Whether Required attribute values should be included.
        /// Is set to false by default.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Whether Display attribute values should be included.
        /// Is set to false by default.
        /// </summary>
        public bool Display { get; set; }

        /// <summary>
        /// Whether Description attribute values should be included.
        /// Is set to false by default.
        /// </summary>
        public bool Description { get; set; }

        /// <summary>
        /// Whether MaxLength attribute values should be included.
        /// Is set to false by default.
        /// </summary>
        public bool MaxLength { get; set; }

        /// <summary>
        /// Whether MinLength attribute values should be included.
        /// Is set to false by default.
        /// </summary>
        public bool MinLength { get; set; }
    }
}

