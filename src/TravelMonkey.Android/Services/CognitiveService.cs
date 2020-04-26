using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using TravelMonkey.Services;

namespace TravelMonkey.Droid.Services
{
    public class CognitiveService : ICognitiveService
    {
        private readonly SpeechRecognizer _recognizer;

        public bool IsTranscribing { get; private set; }

        public event EventHandler<string> TextRecognized;

        public CognitiveService()
        {
            var config = SpeechConfig.FromSubscription(ApiKeys.CongnitiveServiceApiKey, ApiKeys.CognitiveServicesRegion);
            config.SpeechRecognitionLanguage = "en-us";
            _recognizer = new SpeechRecognizer(config);
            _recognizer.Recognized += (s, e) => { OnTextRecognized(e.Result.Text); };
        }

        private void OnTextRecognized(string text) => TextRecognized?.Invoke(this, text);

        public async Task ListenAndTranscribe()
        {
            try
            {
                if (IsTranscribing)
                    await _recognizer.StopContinuousRecognitionAsync();
                else
                    await _recognizer.StartContinuousRecognitionAsync();
                IsTranscribing = !IsTranscribing;
            }
            catch
            {
                IsTranscribing = false;
            }
        }
    }
}
