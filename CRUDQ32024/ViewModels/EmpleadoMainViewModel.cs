
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRUDQ32024.Models;
using CRUDQ32024.Services;
using CRUDQ32024.Views;
using System.Collections.ObjectModel;

namespace CRUDQ32024.ViewModels
{
    public partial class EmpleadoMainViewModel : ObservableObject
    {
        // Coleccion de datos para interactuar con la vista
        [ObservableProperty]
        private ObservableCollection<Empleado> empleadoCollection = new ObservableCollection<Empleado>();

        // Llamamos a la clase en donde configuramos la DB
        private readonly EmpleadoService EmpleadoService;

        // Constructor
        public EmpleadoMainViewModel()
        {
            EmpleadoService = new EmpleadoService();
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

        /// <summary>
        /// Muestra el listado de empleados
        /// </summary>
        public void GetAll()
        {
            var getAll = EmpleadoService.GetAll();

            // Validar si la lista contiene registros
            if (getAll.Count > 0)
            {
                EmpleadoCollection.Clear();
                foreach (var empleado in getAll)
                {
                    EmpleadoCollection.Add(empleado);
                }
            }
        }

        /// <summary>
        /// Redirecciona a la vista de agregar/editar empleado
        /// </summary>
        /// <returns>Vista de agregar/editar empleado</returns>
        [RelayCommand]
        private async Task goToAddEmpleadoForm()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new AddEmpleadoForm());
        }

        /// <summary>
        /// Muestra las opciones al seleccionar un registro de empleado
        /// </summary>
        /// <param name="empleado">Objeto seleccionado para actualizar o elimiar el registro</param>
        /// <returns>Opciones al seleccionar el registro de empleado</returns>
        [RelayCommand]
        private async Task SelectEmpleado(Empleado empleado)
        {
            try
            {
                string actualizar = "Actualizar";
                string eliminar = "ELiminar";

                // Alerta de consulta y dicha alerta trae una respuesta
                string res = await App.Current!.MainPage!.DisplayActionSheet("OPCIONES", "Cancelar", null, actualizar, eliminar);

                if (res == actualizar)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new AddEmpleadoForm(empleado));
                }
                else if (res == eliminar)
                {
                    bool respuesta = await App.Current!.MainPage!.DisplayAlert("ELIMINAR EMPLEADO", "¿Desea Eliminar el registro de empleado?", "Si", "No");

                    if (respuesta)
                    {
                        int del = EmpleadoService.Delete(empleado);

                        if (del > 0)
                        {
                            EmpleadoCollection.Remove(empleado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }
        }

    }
}
