
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRUDQ32024.Models;
using CRUDQ32024.Services;

namespace CRUDQ32024.ViewModels
{
    public partial class AddEmpleadoFormViewModel : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string direccion;

        [ObservableProperty]
        private string email;

        private readonly EmpleadoService EmpleadoService;

        /// <summary>
        /// Constructor que se utiliza al momenton de crear un nuevo registro
        /// </summary>
        public AddEmpleadoFormViewModel()
        {
            EmpleadoService = new EmpleadoService();
        }

        /// <summary>
        /// Constructor que se utiliza al editar un empleado
        /// </summary>
        /// <param name="Empleado">Objeto con datos a editar</param>
        public AddEmpleadoFormViewModel(Empleado Empleado)
        {
            EmpleadoService = new EmpleadoService();
            Id = Empleado.Id;
            Nombre = Empleado.Nombre;
            Direccion = Empleado.Direccion;
            Email = Empleado.Email;
        }

        /// <summary>
        /// Muestra un mensaje de alerta personalizado
        /// </summary>
        /// <param name="Titulo">Titulo de la alerta, por ejemplo, ERROR, ADVERTENCIA, etc</param>
        /// <param name="Mensaje">Mensaje a mostrar en la alerta</param>
        private void Alerta(string Titulo, string Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert(Titulo, Mensaje, "Aceptar"));
        }

        [RelayCommand]
        private async Task AddUpdate()
        {
            try
            {
                //Izquierda: Atributo del objeto
                //Derecha: Atributo del view Model
                Empleado Empleado = new Empleado
                {
                    Id = Id,
                    Nombre = Nombre,
                    Direccion = Direccion,
                    Email = Email
                };

                if (Validar(Empleado))
                {
                    if (Id == 0)
                    {
                        EmpleadoService.Insert(Empleado);
                    }
                    else
                    {
                        EmpleadoService.Update(Empleado);
                    }
                    await App.Current!.MainPage!.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }
        }

        private bool Validar(Empleado Empleado)
        {
            if (Empleado.Nombre == null || Empleado.Nombre == "")
            {
                Alerta("ADVERTENCIA", "Ingrese el nombre completo");
                return false;
            } else if (Empleado.Email == null || Empleado.Email == "")
            {
                Alerta("ADVERTENCIA", "INgrese el correo electrónico");
                return false;
            } else
            {
                return true;
            }
        }
    }
}
