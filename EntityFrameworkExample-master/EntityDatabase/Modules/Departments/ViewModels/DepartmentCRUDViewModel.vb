Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessObjects.Helpers
Imports Modules.Departments.Views

Namespace Modules.Departments.ViewModels
    Public Class DepartmentCRUDViewModel
        Inherits ViewModelBase

        Private datos As IDepartmentService
        Private crear As ICommand
        Private windowCRUD As DepartmentsCRUDView
        Private nombre As String
        Private bud As String

        Public Sub New(window As DepartmentsCRUDView)
            ServiceLocator.RegisterService(Of IDepartmentService)(New DepartmentService)
            Me.datos = GetService(Of IDepartmentService)()
            windowCRUD = window
        End Sub

        Public Property Nombres As String
            Get
                Return nombre
            End Get
            Set(value As String)
                nombre = value
                OnPropertyChanged("Nombres")
            End Set
        End Property

        Public Property Presupuesto As String
            Get
                Return bud
            End Get
            Set(value As String)
                bud = value
                OnPropertyChanged("Presupuesto")
            End Set
        End Property

        Public ReadOnly Property ButtonCrear
            Get
                If crear Is Nothing Then
                    crear = New RelayCommand(AddressOf Creando)
                End If
                Return crear
            End Get
        End Property


        Public Sub Creando()
            Dim department As New Department
            Dim departments As IQueryable(Of Department) = DataContext.DBEntities.Departments
            department.Name = Nombres
            department.Budget = Presupuesto
            department.StartDate = DateTime.Today
            datos.CrearDepartment(department)
            MsgBox("Departement creado", MsgBoxStyle.Information, "School")
            windowCRUD.Close()
        End Sub
    End Class
End Namespace