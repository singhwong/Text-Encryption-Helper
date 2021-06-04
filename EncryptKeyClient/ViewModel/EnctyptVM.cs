using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EncryptKeyClient.EncryptHelper;
using EncryptKeyClient.Services;
using Plugin.StoreReview;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EncryptKeyClient.ViewModel
{
    public class EnctyptVM:BindableBase
    {
        IEncryptService _encryptService;
        GetRandomStr rdStr;
        public EnctyptVM(IEncryptService encryptService)
        {
            _encryptService = encryptService;
            RandomSecretCommand = new Command(()=>
            {
                rdStr = new GetRandomStr();
                SecretText = rdStr.GetRandoms();
            });
            EncryptCommand = new Command(async()=>
            {
                if (await SetKeyTextInputErrorToast())
                    return;
                ResultText = _encryptService.Encrypt(KeyText,SecretText);
            });
            DecryptCommand = new Command(async()=>
            {
                if (await SetKeyTextInputErrorToast())
                    return;
                try
                {
                    ResultText = _encryptService.Decrypt(KeyText, SecretText);

                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
                }
            });
            ToClipboardCommand = new Command(async()=>
            {
                if (string.IsNullOrEmpty(ResultText))
                {
                    return;
                }
                await Clipboard.SetTextAsync(ResultText);
            });
            async Task<bool> SetKeyTextInputErrorToast()
            {
                if (string.IsNullOrEmpty(SecretText)||SecretText.Length != 4)
                {
                    await App.Current.MainPage.DisplayAlert("Remind", "The key cannot be empty and the length must be 4!", "Ok");
                    return true;
                }
                if (string.IsNullOrEmpty(KeyText))
                {
                    await App.Current.MainPage.DisplayAlert("Remind", "The content to be encrypted cannot be empty!", "Ok");
                    return true;
                }
                return false;
            }
            MoreAppsCommand = new Command<string>(async(url)=>
            {
                await Launcher.OpenAsync(url);
            });
            PrivacyPolicyCommand = new Command<string>(async(url)=>
            {
                await Launcher.OpenAsync(url);
            });
            CommentCommand = new Command<string>((url)=>
            {
                //await CrossStoreReview.Current.RequestReview(true);
                //CrossStoreReview.Current.OpenStoreListing(url);
                CrossStoreReview.Current.OpenStoreReviewPage(url);
            });
        }
        string _secretText;
        // 4位特定字符
        public string SecretText
        {
            get => _secretText;
            set => Set(ref _secretText,value);
        }
        string _keyText;
        // 待加密的字符串
        public string KeyText
        {
            get => _keyText;
            set => Set(ref _keyText,value);
        }
        string _resultText;
        public string ResultText
        {
            get => _resultText;
            set => Set(ref _resultText,value);
        }
        public ICommand RandomSecretCommand { get; private set; }
        public ICommand EncryptCommand { get; private set; }
        public ICommand DecryptCommand { get; private set; }
        public ICommand ToClipboardCommand { get; private set; }
        public ICommand MoreAppsCommand { get; private set; }
        public ICommand PrivacyPolicyCommand { get; private set; }
        public ICommand CommentCommand { get; set; }
    }
}
