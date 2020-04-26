using System.Threading.Tasks;
using TravelMonkey.Models;
using TravelMonkey.Services;
using Xamarin.Forms;

namespace TravelMonkey.ViewModels
{
    public class PlanTripPageViewModel : BaseViewModel
    {
        private readonly ICognitiveService _cognitiveService;


        private ListenStatus _listenStatus;
        public ListenStatus ListenStatus
        {
            get => _listenStatus;
            set => Set(ref _listenStatus, value);
        }

        public Command SpeakCommand { get; }

        public PlanTripPageViewModel()
        {
            SpeakCommand = new Command(async () => await SpeakAction());
            _cognitiveService = DependencyService.Get<ICognitiveService>();
            _cognitiveService.TextRecognized += CognitiveService_TextRecognized;
        }

        private void CognitiveService_TextRecognized(object sender, string e)
        {

        }

        private Task SpeakAction()
        {
            return _cognitiveService.ListenAndTranscribe();
        }
    }
}
