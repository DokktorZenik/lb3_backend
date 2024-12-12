using Microsoft.ML.Data;

namespace _3_laba.src.TextClassificationTF.classes
{
    public class FixedLength
    {

        [VectorType(Config.FeatureLength)]
        public int[]? Features { get; set; }
    }
}
