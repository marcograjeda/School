Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessObjects.Helpers
Imports Modules.Courses.Views
Namespace Modules.Courses.ViewModels
    Public Class CoursesCRUDViewModel
        Inherits ViewModelBase

        Private _course As New Course
        Public crear As ICommand
        Public _cancelCommand As ICommand
        Public windowCRUD As CoursesCRUD
        Private _departments As ObservableCollection(Of Department)
        Private _selectedDepartment As Department
        Private dataAccess As IDepartmentService
        Public Property Title As String
            Get
                Return Me._course.Title
            End Get
            Set(value As String)
                Me._course.Title = value
                OnPropertyChanged("Title")
            End Set
        End Property

        Public Property Credits As String
            Get
                Return Me._course.Credits
            End Get
            Set(value As String)
                Me._course.Credits = value
                OnPropertyChanged("Credits")
            End Set
        End Property

        Public Property Department As Department
            Get
                Return _selectedDepartment
            End Get
            Set(value As Department)
                _course.Department = value
                OnPropertyChanged("SelectedDepartment")
            End Set
        End Property

        Public Property Departments As ObservableCollection(Of Department)
            Get
                Return _departments
            End Get
            Set(value As ObservableCollection(Of Department))
                _departments = value
                OnPropertyChanged("Departments")
            End Set
        End Property

        Public ReadOnly Property ButtonAceptar As ICommand
            Get
                If Me.crear Is Nothing Then
                    Me.crear = New RelayCommand(AddressOf CrearCourse)
                End If
                Return Me.crear
            End Get
        End Property

        Sub CrearCourse()
            Try
                Dim courses As IQueryable(Of Course) = DataContext.DBEntities.Courses
                For Each element In courses
                    _course.CourseID = Integer.Parse(element.CourseID.ToString) + 1
                Next
                DataContext.DBEntities.Courses.Add(_course)
                DataContext.DBEntities.SaveChanges()
                windowCRUD.Close()
            Catch ex As Exception
                MessageBox.Show("No se ha podido crear el curso", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub New(ByRef view As CoursesCRUD)
            _departments = New ObservableCollection(Of Department)
            Dim departments As IQueryable(Of Department) = DataContext.DBEntities.Departments
            For Each elemet In departments
                _departments.Add(elemet)
            Next
            Me.windowCRUD = view
        End Sub
    End Class
End Namespace