using SafetyBP.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SafetyBP.ViewModels
{
    public class UserSettingViewModel : BaseViewModel
    {
        private bool _isPasswordShow;
        
        private Dictionary<LanguagesEnum, string> _languages;

        public KeyValuePair<LanguagesEnum, string> SelectedLanguage { get; set; }

        public ObservableCollection<KeyValuePair<LanguagesEnum, string>> Languages { get; set; }
        public bool ChangingLanguage { get; internal set; }

        // Properties related to the labels
        public string TranslateText { get; set; }
        public string DisplayAlertTitle { get; set; }
        public string DisplayAlertMessage { get; set; }
        public string DisplayAlertButtonCancel { get; set; }
        public string DisplayAlertButtonOk { get; set; }

        public string FamilyUserName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string CloseSessionButtonLabel { get; set; }

        public bool IsPasswordShow
        {
            get { return _isPasswordShow; }
            set
            {
                _isPasswordShow = value;
                OnPropertyChanged();
                OnPropertyChanged("ShowHideIcon");
            }
        }

        public string ShowHideIcon
        {
            get
            {
                return IsPasswordShow ? "ballot.png" : "camara.png";
            }
        }


        public ICommand CloseSessionButtonCommand { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand ShowHideTapCommand { get; }


        public UserSettingViewModel():base(ApplicationWordsEnum.TitleConfiguration)
        {
            LoadingLanguageList();
            SetUserProfileInformation();
            ChangeApplicationLanguage();

            ShowHideTapCommand = new Command(() =>
            {
                IsPasswordShow = !IsPasswordShow;
            });
        }

        private void ChangeApplicationLanguage() 
        {
            TranslateText = GetTranslateValue(ApplicationWordsEnum.LabelSelectLanguage);
            LabelButtonSave = GetTranslateValue(ApplicationWordsEnum.LabelButtonSave);
            
            DisplayAlertTitle = GetTranslateValue(ApplicationWordsEnum.DisplayAlertUserSettingTitle);
            DisplayAlertMessage = GetTranslateValue(ApplicationWordsEnum.DisplayAlertUserSettingMessage);
            DisplayAlertButtonCancel = GetTranslateValue(ApplicationWordsEnum.DisplayAlertUserSettingButtonCancel);
            DisplayAlertButtonOk = GetTranslateValue(ApplicationWordsEnum.DisplayAlertUserSettingButtonOk);

            CloseSessionButtonLabel = GetTranslateValue(ApplicationWordsEnum.UserSettingCloseSessionButton);

            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(TranslateText));
            OnPropertyChanged(nameof(DisplayAlertTitle));
            OnPropertyChanged(nameof(DisplayAlertMessage));
            OnPropertyChanged(nameof(DisplayAlertButtonCancel));
            OnPropertyChanged(nameof(DisplayAlertButtonOk));
            OnPropertyChanged(nameof(CloseSessionButtonLabel));
        }

        private void SetUserProfileInformation() 
        {
            FamilyUserName = "";
            CloseSessionButtonCommand = new Command(() =>
            {
                LoginAgain();
            });
        }

        private void LoadingLanguageList() 
        {
            ChangingLanguage = true;
            _languages = new Dictionary<LanguagesEnum, string>(Translate.GetLanguageList().Result);
            Languages = new ObservableCollection<KeyValuePair<LanguagesEnum, string>>(_languages);
            OnPropertyChanged(nameof(Languages));
            ChangingLanguage = false;
        }

        public void ChangeLanguage()
        {
            if (!ChangingLanguage)
            {
                Translate.SetLanguage(SelectedLanguage.Key);
                ChangeApplicationLanguage();
                //LoadingLanguageList();
            }
        }
    }
}
