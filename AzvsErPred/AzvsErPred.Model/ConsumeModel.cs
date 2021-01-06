using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ML;
using AzvsErPred.Model;

namespace AzvsErPred.Model
{
    public class ConsumeModel
    {
        public static float[] divide(float[] x, float[] y)
        {
            List<float> result = new List<float>();
            int i = 0;
            for (i = 0; i < x.Length; i++)
            {
                if (y[i] == 0)
                    y[i] = 0.001f;
                result.Add(x[i] / y[i]);
            }
            return result.ToArray();
        }
        private static Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(CreatePredictionEngine);

        // For more info on consuming ML.NET models, visit https://aka.ms/mlnet-consume
        // Method for consuming model in your app
        public static ModelOutput Predict(ModelInput input)
        {
            string[] tags = { "E", "N", "A" };
            ModelOutput result = PredictionEngine.Value.Predict(input);
            ModelOutput nullHypothesis = PredictionEngine.Value.Predict(new ModelInput()
            {
                Coutry_tag = "",
                Header = "",
                Body = ""
            });
            float[] ratios = divide(result.Score, nullHypothesis.Score);
            result.Score = ratios;
            int index = ratios.ToList().IndexOf(ratios.Max());
            result.Prediction = tags[index];
            return result;
        }

        public static PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine()
        {
            // Create new MLContext
            MLContext mlContext = new MLContext();

            // Load model & create prediction engine
            string modelPath = @"C:\Users\MR1SP\AppData\Local\Temp\MLVSTools\AramaMotorlarıProjesiML\AramaMotorlarıProjesiML.Model\MLModel.zip";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            return predEngine;
        }
    }
}
