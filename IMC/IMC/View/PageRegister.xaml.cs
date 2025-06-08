using IMC.ViewModel;

namespace IMC.View;

public partial class PageRegister : ContentPage
{
    public PageRegister()
    {
        InitializeComponent();
        BindingContext = App.Current.Services.GetRequiredService<VMPageRegister>();
    }
}