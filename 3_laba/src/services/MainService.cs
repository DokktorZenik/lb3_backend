using _3_laba.src.TextClassificationTF.classes;
using _3_laba.src.TextClassificationTF.classes.db;
using System.Security.Cryptography;

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
            return $"Is sentiment/review positive? {(score > 0.5 ? "Yes." : "No.")}";
        }

        public string GetModelStatus()
        {
            return PredictModel.getIsModelTrained() ? "Model has finished training" : "Model has been training";
        }

        public void AddReview(ReviewForm review)
        {
            using (var context = new MovieDbContext())
            {
                byte[] randomBytes = new byte[10];
                RandomNumberGenerator.Fill(randomBytes);

                int randomNumber = BitConverter.ToInt32(randomBytes, 0);

                review.Id = randomNumber;
                context.ReviewForms.Add(review);
                context.SaveChanges();

            }
        }

        public List<ReviewForm> GetAllReviews()
        {
            using (var context = new MovieDbContext())
            {

                var reviews = context.ReviewForms.ToList();
                return reviews;
            }
        }
    }
}
