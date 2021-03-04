using SafetyBP.Domain.Constants;
using SafetyBP.Domain.Enums;
using SafetyBP.Domain.Interfaces;
using SafetyBP.Domain.Models;
using SafetyBP.Views.Modules.SpontaneousDiversions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SafetyBP.ViewModels.SpontaneousDiversions
{
    public class SpontaneousDiversionsViewModel : BaseViewModel
    {
        private readonly ISafetySectorBusiness _safetySectorBusiness;
        private readonly ISpontaneousDiversionBusiness _spontaneousDiversionBusiness;

        public SafetySpontaneousDiversion SpontaneousDiversion { get; set; }
        public ObservableCollection<SafetySector> SafetySectors { get; set; }

        public string LabelSector { get; private set; }
        public string LabelRisk { get; private set; }

        public string TitlePickerSector { get; private set; }
        public string LabelButtonLow { get; private set; }
        public string LabelButtonMedium { get; private set; }
        public string LabelButtonHigh { get; private set; }
        public string EntryPlaceHolderText { get; private set; }

        public string LabelButtonContinueAdd { get; private set; }


        public ICommand RiskButtonCommand { get; }
        public ICommand OnLoad { get; set; }

        public ICommand FinalizeCommand { get; set; }
        public ICommand FollowingCommand { get; set; }
        private ICommand SendToServerCallbackCommand;
        private ICommand SendToServerCallbackPostCommand;

        public Color ButtonColorRiskLow { get; set; }
        public Color ButtonColorRiskMedium { get; set; }
        public Color ButtonColorRiskHigh { get; set; }
        public SpontaneousDiversionsViewModel() : base(Data.ApplicationWordsEnum.PageTitleSpontaneousDiversion)
        {
            _safetySectorBusiness = DependencyService.Get<ISafetySectorBusiness>();
            _spontaneousDiversionBusiness = DependencyService.Get<ISpontaneousDiversionBusiness>();

            TitlePickerSector = GetTranslateValue(Data.ApplicationWordsEnum.LabelSectorPicker);
            LabelSector = GetTranslateValue(Data.ApplicationWordsEnum.LabelSector);
            LabelRisk = GetTranslateValue(Data.ApplicationWordsEnum.LabelRisk);

            LabelButtonLow = GetTranslateValue(Data.ApplicationWordsEnum.LabelButtonRiskLow);
            LabelButtonMedium = GetTranslateValue(Data.ApplicationWordsEnum.LabelButtonRiskMedium);
            LabelButtonHigh = GetTranslateValue(Data.ApplicationWordsEnum.LabelButtonRiskHigh);

            LabelButtonContinueAdd = GetTranslateValue(Data.ApplicationWordsEnum.TextButtonContinueAdding);
            EntryPlaceHolderText = GetTranslateValue(Data.ApplicationWordsEnum.EntryPlaceHolderTextSpontaneousDiversion);

            ResetRiskButtonMark();
            
            OnMenuCommand = new Command(async parameter =>
            {
                await PopupNavigation.PushAsync(new SpontaneousDiversionPopupMenuPage(SpontaneousDiversion));
            });

            RiskButtonCommand = new Command<string>(parameter =>
            {
                OnRiskCommand(byte.Parse(parameter));
            });

            FinalizeCommand = new Command(async () => await OnFinalizeCommand());

            FollowingCommand = new Command(async () => await AddNew());

            OnLoad = new Command(async () =>
            {
                SafetySectors = new ObservableCollection<SafetySector>(await _safetySectorBusiness.GetAllSectorsAsync());
                OnPropertyChanged(nameof(SafetySectors));
            });

            SendToServerCallbackPostCommand = new Command(async () =>
            {
                await Navigation.PopToRootAsync();
                await FinalizateLoaderPage();
            });


            SendToServerCallbackCommand = new Command(async () =>
            {
                await SendToServerCallback();
            });

            SpontaneousDiversion = new SafetySpontaneousDiversion();
            OnPropertyChanged(nameof(SpontaneousDiversion));
            OnLoad.Execute(null);
            BeginOperationCommand.Execute(ModuleNameConstants.SPONTANEOUSDIVERSION);
        }

        private void ResetPage() 
        {
            SpontaneousDiversion = new SafetySpontaneousDiversion();
            OnPropertyChanged(nameof(SpontaneousDiversion));
            ResetRiskButtonMark();
        }
        private async Task AddNew() 
        {
            if (IsValid())
            {
                await _spontaneousDiversionBusiness.AddSpontaneousDiversionAsync(SpontaneousDiversion);
                Toaster.Short(GetTranslateValue(Data.ApplicationWordsEnum.ToastMessageSpontaneousDiversionSaveProperly));
                ResetPage();
            }
            else
            {
                Toaster.Short(GetTranslateValue(Data.ApplicationWordsEnum.ToastMessageThereAreQuestionsWithoutAnswer));
            }
        }

        private bool IsValid() 
        {
            if (SpontaneousDiversion.Reason.Trim().Length == 0) return false;
            if (SpontaneousDiversion.Sector == null) return false;
            if (SpontaneousDiversion.Risk == SpontaneousDiversionRisk.Undefined) return false;

            return true;
        }

        
        private async Task SendToServerCallback() 
        {
            await SpontaneousDiversionRestClient.CommitOperation(OperationId, result =>
            {
                Toaster.Short(GetTranslateValue(Data.ApplicationWordsEnum.ToastMessageSpontaneousDiversionSaveProperly));
                ResetPage();
                SendToServerCallbackPostCommand.Execute(null);
            });
        }

        private async Task OnFinalizeCommand() 
        {
            // Si ingreso uno, y es valido, lo agrego a la lista a enviar
            if (IsValid()) 
            {
                await CatchLoadingFor(GetTranslateValue(Data.ApplicationWordsEnum.PopUpFinalizeSpontaneousDiversionLabel), async () =>
                {

                    await _spontaneousDiversionBusiness.AddSpontaneousDiversionAsync(SpontaneousDiversion);

                    if (await _spontaneousDiversionBusiness.AnyPendingToFinalizeAsync())
                    {
                        await _spontaneousDiversionBusiness.SendToServer(() => SendToServerCallbackCommand.Execute(null));
                    }
                    else
                    {
                        Toaster.Short(GetTranslateValue(Data.ApplicationWordsEnum.ToastMessageSpontaneousDiversionThereAreNotItems));
                    }
                });
            }
            else
            {
                    Toaster.Short(GetTranslateValue(Data.ApplicationWordsEnum.ToastMessageThereAreQuestionsWithoutAnswer));
            }
        }

        private void OnRiskCommand(byte risk)
        {
            switch ((SpontaneousDiversionRisk)risk)
            {
                case SpontaneousDiversionRisk.Low: {
                        ButtonColorRiskLow = Color.FromHex("#28b34b");
                        ButtonColorRiskMedium = ButtonColorRiskHigh = GrayColor;
                    } break;
                case SpontaneousDiversionRisk.Medium: {
                        ButtonColorRiskMedium = Color.FromHex("#f2bf56");
                        ButtonColorRiskLow = ButtonColorRiskHigh = GrayColor;
                    } break;
                case SpontaneousDiversionRisk.High: {
                        ButtonColorRiskHigh = Color.FromHex("#fc0025");
                        ButtonColorRiskLow = ButtonColorRiskMedium = GrayColor;
                    } break;
            }

            RefressRiskButtonColor();
            SpontaneousDiversion.Risk = (SpontaneousDiversionRisk)risk;
        }

        public void ResetRiskButtonMark() 
        {
            ButtonColorRiskLow = ButtonColorRiskMedium = ButtonColorRiskHigh = GrayColor;
            RefressRiskButtonColor();
        }

        private void RefressRiskButtonColor() 
        {
            OnPropertyChanged(nameof(ButtonColorRiskLow));
            OnPropertyChanged(nameof(ButtonColorRiskMedium));
            OnPropertyChanged(nameof(ButtonColorRiskHigh));
        }
    }
}
