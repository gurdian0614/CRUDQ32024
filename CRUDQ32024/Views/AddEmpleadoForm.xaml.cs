using CRUDQ32024.Models;
using CRUDQ32024.ViewModels;

namespace CRUDQ32024.Views;

public partial class AddEmpleadoForm : ContentPage
{
	private AddEmpleadoFormViewModel ViewModel;
	public AddEmpleadoForm()
	{
		InitializeComponent();
		ViewModel = new AddEmpleadoFormViewModel();
		BindingContext = ViewModel;
	}

	public AddEmpleadoForm(Empleado empleado)
	{
		InitializeComponent();
		ViewModel = new AddEmpleadoFormViewModel(empleado);
		BindingContext = ViewModel;
	}
}