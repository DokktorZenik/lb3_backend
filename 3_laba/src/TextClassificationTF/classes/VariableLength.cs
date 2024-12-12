using Microsoft.ML.Data;

namespace _3_laba.src.TextClassificationTF.classes
{
    public class VariableLength
    {
        [VectorType]
        public int[]? VariableLengthFeatures { get; set; }
    }
}
