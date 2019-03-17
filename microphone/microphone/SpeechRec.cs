using System;
using Google.Cloud.Speech.V1;


namespace microphone
{
    public class SpeechRec
    {
        private const string voicePath = "e:\\voice\\mic.wav";

        public string speech()
        {
            string text = string.Empty;
            try
            {
                var speech = SpeechClient.Create();
                var response = speech.Recognize(new RecognitionConfig()
                {
                    Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                    SampleRateHertz = 16000,
                    LanguageCode = "en",
                }, RecognitionAudio.FromFile(voicePath));
                foreach (var result in response.Results)
                {
                    foreach (var alternative in result.Alternatives)
                    {
                        text += alternative.Transcript;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return text;
        }
    }
}
