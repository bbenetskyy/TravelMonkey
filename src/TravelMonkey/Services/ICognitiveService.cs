using System;
using System.Threading.Tasks;

namespace TravelMonkey.Services
{
    public interface ICognitiveService
    {
        event EventHandler<string> TextRecognized;
        Task ListenAndTranscribe();
    }
}
