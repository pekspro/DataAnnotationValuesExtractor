using System;

namespace Pekspro.DataAnnotationValuesExtractor
{
    /// <summary>
    /// Indicates that build information should be generated in the class.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    [System.Diagnostics.Conditional("PEKSPRO_DATAANNOTATIONSVALUESGENERATOR_PRESERVE_ATTRIBUTES")]
    public class DataAnnotationValuesOptionsAttribute : System.Attribute
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
    }
}

