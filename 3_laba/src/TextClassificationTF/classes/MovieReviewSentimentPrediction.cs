using Microsoft.ML.Data;

namespace _3_laba.src.TextClassificationTF.classes
{
    public class MovieReviewSentimentPrediction
    {
        [VectorType(2)]
        public float[]? Prediction { get; set; }
    }
}
