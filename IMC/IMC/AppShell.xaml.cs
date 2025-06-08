using IMC.View;

namespace IMC
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Vista
            Routing.RegisterRoute(nameof(PageRegister), typeof(PageRegister)); //registramos la clase
            Routing.RegisterRoute(nameof(Home), typeof(Home));


            //view model
            //models
            Routing.RegisterRoute(nameof(Paciente), typeof(Paciente)); //registramos la clase

        }
    }
}

