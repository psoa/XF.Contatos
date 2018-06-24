using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Contatos.API;
using XF.Contatos.Contact;

namespace XF.Contatos.ViewModel
{
    public class ContatoViewModel : INotifyPropertyChanged
    {

        public PhoneContact PhoneContactModel { get; set; }
        public IEnumerable<PhoneContact> Contatos { get; private set; }



        public OnCallCMD CallCMD { get; }
        public OnEditCMD EditCMD { get; }

        public ICommand OnNovoCMD { get; private set; }

        public ContatoViewModel()
        {
            IContactList contactList = DependencyService.Get<IContactList>();
            try {
                Contatos = contactList.GetAllContacts();    
            } catch {
                App.Current.MainPage.DisplayAlert("Não foi possível carregar os contatos ",
                         "Verifique as permissões do aplicativo", "Ok");
            }

            EditCMD = new OnEditCMD(this);
            CallCMD = new OnCallCMD(this);
        }
        public async void OnCall(PhoneContact phoneContact)
        {
            var answer = await App.Current.MainPage.DisplayAlert("Ligar para ",
                         $"tel: {phoneContact.PhoneNumber}", "Sim", "Não");
            if (answer)
            {
                var phone = DependencyService.Get<ILigar>();
                if (phone != null) phone.Discar(phoneContact.PhoneNumber);
            }

        }

        public async void OnEdit(PhoneContact phoneContact)
        {
            App.ContatoVM.PhoneContactModel = phoneContact;
            await App.Current.MainPage.Navigation.PushAsync(new ContactView() { BindingContext = App.ContatoVM });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void EventPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }


    public class OnCallCMD : ICommand
    {
        private ContatoViewModel contatoVM;



        public OnCallCMD(ContatoViewModel paramVM)
        {
            contatoVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;
        public void LigarCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter) => ((parameter != null) && (!string.IsNullOrWhiteSpace(((PhoneContact)parameter).PhoneNumber)));
        public void Execute(object parameter)
        {
            contatoVM.OnCall(parameter as PhoneContact);
        }
    }

    public class OnEditCMD : ICommand
    {
        private ContatoViewModel contatoVM;

        public OnEditCMD(ContatoViewModel paramVM)
        {
            contatoVM = paramVM;
        }
        public event EventHandler CanExecuteChanged;
        public void EditCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter) => ((parameter != null) && (!string.IsNullOrWhiteSpace(((PhoneContact)parameter).PhoneNumber)));
        public void Execute(object parameter)
        {
            contatoVM.OnEdit(parameter as PhoneContact);
        }
    }


}
