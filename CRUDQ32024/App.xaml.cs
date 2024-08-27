using CRUDQ32024.Views;

namespace CRUDQ32024
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new EmpleadoMain());
        }
    }
}
