Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.Departments.ViewModels
    Public Class DepartmentsViewModel
        Inherits ViewModelBase

        Private _departments As ObservableCollection(Of Department)
        Private department As Department
        Private _seleccion As Department
        Private dataAccess As IDepartmentService
        Private crear As ICommand
        Private modificar As ICommand
        Private eliminar As ICommand

        Public Property Departments As ObservableCollection(Of Department)
            Get
                Return Me._departments
            End Get
            Set(value As ObservableCollection(Of Department))
                Me._departments = value
                OnPropertyChanged("Departments")
            End Set
        End Property

        Public Property Departmento As Department
            Get
                Return department
            End Get
            Set(value As Department)
                department = value
                OnPropertyChanged("Departmento")
            End Set
        End Property

        Public Property Seleccionado As Department
            Get
                Return _seleccion
            End Get
            Set(value As Department)
                _seleccion = value
            End Set
        End Property

        ' Function to get all departments from service
        Private Function GetAllDepartments() As IQueryable(Of Department)
            Return Me.dataAccess.GetAllDepartments
        End Function

        Sub New()
            Me._departments = New ObservableCollection(Of Department)
            ServiceLocator.RegisterService(Of IDepartmentService)(New DepartmentService)
            Me.dataAccess = GetService(Of IDepartmentService)()
            For Each element In Me.GetAllDepartments
                Me._departments.Add(element)
            Next
        End Sub

        Public ReadOnly Property ButtonCrear
            Get
                If crear Is Nothing Then
                    crear = New RelayCommand(AddressOf Creando)
                End If
                Return crear
            End Get
        End Property

        Public Sub Creando()
            Dim crear As New DepartmentsCRUDView
            crear.ShowDialog()
            _departments.Clear()
            For Each element In Me.GetAllDepartments
                Me._departments.Add(element)
            Next
        End Sub
        Sub Actualizar()
            Me.Departments.Clear()
            ServiceLocator.RegisterService(Of IDepartmentService)(New DepartmentService)
            Me.dataAccess = GetService(Of IDepartmentService)()
            For Each element In Me.GetAllDepartments
                Me._departments.Add(element)
            Next
        End Sub

        Public ReadOnly Property ButtonEliminar
            Get
                If eliminar Is Nothing Then
                    eliminar = New RelayCommand(AddressOf Eliminando)
                End If
                Return eliminar
            End Get
        End Property

        Public Sub Eliminando()
            If Seleccionado IsNot Nothing Then
                DataContext.DBEntities.Departments.Remove((From item In DataContext.DBEntities.Departments
                                 Where Seleccionado.DepartmentID = item.DepartmentID
                                 Select item).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                Actualizar()
            End If
        End Sub
    End Class
End Namespace

