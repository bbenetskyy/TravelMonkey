using System;
using System.Threading.Tasks;

namespace TravelMonkey.Services
{
    public interface ICognitiveService
    {
        bool IsTranscribing { get; }
        event EventHandler<string> TextRecognized;
        Task ListenAndTranscribe();
    }
}
