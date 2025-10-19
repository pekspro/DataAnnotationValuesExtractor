using System;

namespace Pekspro.DataAnnotationValuesExtractor
{
    /// <summary>
    /// Indicates that build information should be generated in the class.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
    [System.Diagnostics.Conditional("PEKSPRO_DATAANNOTATIONSVALUESGENERATOR_PRESERVE_ATTRIBUTES")]
    public class DataAnnotationValuesToGenerateAttribute : System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataAnnotationValuesToGenerateAttribute"/> class.
        /// </summary>
        /// <param name="type">The type where the data annotations values should be generated.</param>
        public DataAnnotationValuesToGenerateAttribute(Type type)
        {
            Type = type;
        }

        /// <summary>
        /// The type where the data annotations values should be generated.
        /// </summary>
        public Type Type { get; set; }
    }
}

