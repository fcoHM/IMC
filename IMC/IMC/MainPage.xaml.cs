using IMC.ViewModel;

namespace IMC
{
    public partial class MainPage : ContentPage
    {
        private readonly VMMainPage _vm;
        public MainPage()
        {

            InitializeComponent();
            _vm = App.Current.Services.GetRequiredService<VMMainPage>();
            BindingContext = _vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is VMMainPage vm)
                await vm.CargarPacientesAsync();
        }
    }
}
