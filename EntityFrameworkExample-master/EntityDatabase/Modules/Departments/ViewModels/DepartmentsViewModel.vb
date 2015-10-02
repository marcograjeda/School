Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.Departments.ViewModels
    Public Class DepartmentsViewModel
        Inherits ViewModelBase

        Private _departments As ObservableCollection(Of Department)
        Private dataAccess As IDepartmentService
        Private crear As ICommand

        Public Property Departments As ObservableCollection(Of Department)
            Get
                Return Me._departments
            End Get
            Set(value As ObservableCollection(Of Department))
                Me._departments = value
                OnPropertyChanged("Departments")
            End Set
        End Property

        ' Function to get all departments from service
        Private Function GetAllDepartments() As IQueryable(Of Department)
            Return Me.dataAccess.GetAllDepartments
        End Function

        Sub New()
            'Initialize property variable of departments
            Me._departments = New ObservableCollection(Of Department)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IDepartmentService)(New DepartmentService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IDepartmentService)()
            ' Populate departments property variable 
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
    End Class
End Namespace

