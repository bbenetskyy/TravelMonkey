using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TravelMonkey.Models;
using TravelMonkey.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelMonkey.ViewModels
{
    public class PlanTripPageViewModel : BaseViewModel
    {
        private readonly ICognitiveService _cognitiveService;
        private readonly BingSearchService _bingSearchService;


        private ListenStatus _listenStatus;
        public ListenStatus ListenStatus
        {
            get => _listenStatus;
            set => Set(ref _listenStatus, value);
        }

        private ObservableCollection<Destination> _sightseeing;
        public ObservableCollection<Destination> Sightseeing
        {
            get => _sightseeing;
            set => Set(ref _sightseeing, value);
        }

        private string _country = "No Where";
        public string Country
        {
            get => _country;
            set => Set(ref _country, value);
        }


        public Command SpeakCommand { get; }

        public PlanTripPageViewModel()
        {
            SpeakCommand = new Command(async () => await SpeakAction());
            _bingSearchService = new BingSearchService();
            _cognitiveService = DependencyService.Get<ICognitiveService>();
            _cognitiveService.TextRecognized += CognitiveService_TextRecognized;

            Sightseeing = new ObservableCollection<Destination>();
        }

        private async void CognitiveService_TextRecognized(object sender, string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                return;

            if (_cognitiveService.IsTranscribing)
                await _cognitiveService.ListenAndTranscribe();

            ListenStatus = ListenStatus.Processing;
            Country = country;

            Sightseeing.Clear();
            var sightseeing = await _bingSearchService.GetByCountry(country);
            foreach (var sightsee in sightseeing)
            {
                Sightseeing.Add(sightsee);
            }

            ListenStatus = ListenStatus.Default;
        }

        private async Task SpeakAction()
        {
            if (!await IsMicrophonePermissionGranted()) 
                return;
            await _cognitiveService.ListenAndTranscribe();
            ListenStatus = _cognitiveService.IsTranscribing
                ? ListenStatus.Listening
                : ListenStatus.Default;
        }

        private async Task<bool> IsMicrophonePermissionGranted()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Microphone>();
            }
            return status == PermissionStatus.Granted;
        }
    }
}
