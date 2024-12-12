using _3_laba.src.TextClassificationTF.classes;

namespace _3_laba.src.services
{
    public class MainService
    {
        public string GetResult()
        {
            return "Hello from MyService!";
        }

        public void StartTraining()
        {
            PredictModel.initialize();
        }

        public string Predict(MovieReview review)
        {
            var score = PredictModel.PredictSentiment(review);
            return $"Is sentiment/review positive? {(score > 0.5 ? "Yes." : "No.")} with score {score}" ;
        }

        public string GetModelStatus()
        {
            return PredictModel.getIsModelTrained() ? "Model has finished training" : "Model has been training";
        }
    }
}
