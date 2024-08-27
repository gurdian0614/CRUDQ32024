using CRUDQ32024.ViewModels;

namespace CRUDQ32024.Views;

public partial class EmpleadoMain : ContentPage
{
	private EmpleadoMainViewModel ViewModel;
	public EmpleadoMain()
	{
		InitializeComponent();
		ViewModel = new EmpleadoMainViewModel();
		this.BindingContext = ViewModel;
	}

    /// <summary>
    /// Permite a los desarrolladores de aplicaciones personalizar el comportamiento inmediatamente antes de que la página sea visible
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
		ViewModel.GetAll();
    }
}