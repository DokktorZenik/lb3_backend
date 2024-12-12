using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;

namespace _3_laba.src.TextClassificationTF.classes
{
    public class PredictModel
    {

        private static string _modelPath = Path.Combine(Environment.CurrentDirectory, "src\\TextClassificationTF");

        private static MLContext mlContext = new MLContext();

        private static ITransformer model;

        private static bool isModelTrained = false;

        public static void initialize()
        {
            Console.WriteLine("Start training");

            var lookupMap = mlContext.Data.LoadFromTextFile(Path.Combine(_modelPath, "imdb_word_index.csv"),
    columns: new[]
        {
            new TextLoader.Column("Words", DataKind.String, 0),
            new TextLoader.Column("Ids", DataKind.Int32, 1),
        },
    separatorChar: ','
    );

            Action<VariableLength, FixedLength> ResizeFeaturesAction = (s, f) =>
            {
                var features = s.VariableLengthFeatures;
                Array.Resize(ref features, Config.FeatureLength);
                f.Features = features;
            };

            TensorFlowModel tensorFlowModel = mlContext.Model.LoadTensorFlowModel(_modelPath);

            DataViewSchema schema = tensorFlowModel.GetModelSchema();
            Console.WriteLine(" =============== TensorFlow Model Schema =============== ");
            var featuresType = (VectorDataViewType)schema["Features"].Type;
            Console.WriteLine($"Name: Features, Type: {featuresType.ItemType.RawType}, Size: ({featuresType.Dimensions[0]})");
            var predictionType = (VectorDataViewType)schema["Prediction/Softmax"].Type;
            Console.WriteLine($"Name: Prediction/Softmax, Type: {predictionType.ItemType.RawType}, Size: ({predictionType.Dimensions[0]})");

            IEstimator<ITransformer> pipeline =

                mlContext.Transforms.Text.TokenizeIntoWords("TokenizedWords", "ReviewText")
            .Append(mlContext.Transforms.Conversion.MapValue("VariableLengthFeatures", lookupMap,
                lookupMap.Schema["Words"], lookupMap.Schema["Ids"], "TokenizedWords"))
            .Append(mlContext.Transforms.CustomMapping(ResizeFeaturesAction, "Resize"))
            .Append(tensorFlowModel.ScoreTensorFlowModel("Prediction/Softmax", "Features"))
            .Append(mlContext.Transforms.CopyColumns("Prediction", "Prediction/Softmax"));


            IDataView dataView = mlContext.Data.LoadFromEnumerable(new List<MovieReview>());
            model = pipeline.Fit(dataView);

            Console.WriteLine("Finish trainig");
            isModelTrained = true;
        }

        public static float? PredictSentiment(MovieReview review)
        {
            var engine = mlContext.Model.CreatePredictionEngine<MovieReview, MovieReviewSentimentPrediction>(model);

            var sentimentPrediction = engine.Predict(review);

            return sentimentPrediction.Prediction?[1];
        }

        public static bool getIsModelTrained()
        {
            return isModelTrained;
        }
    }
}
