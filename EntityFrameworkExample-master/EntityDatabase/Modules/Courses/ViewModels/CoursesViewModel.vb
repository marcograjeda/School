Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Namespace Modules.Courses.ViewModels
    Public Class CoursesViewModel
        Inherits ViewModelBase

        Private _courses As ObservableCollection(Of Course)
        Private dataAccess As ICourseService
        Private crear As ICommand
        Private eliminar As ICommand
        Private _seleccion As Course

        Public Property Courses As ObservableCollection(Of Course)
            Get
                Return Me._courses
            End Get
            Set(value As ObservableCollection(Of Course))
                Me._courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        Public Property Seleccionado As Course
            Get
                Return _seleccion
            End Get
            Set(value As Course)
                _seleccion = value
            End Set
        End Property

        Private Function GetCourses() As IQueryable(Of Course)
            Return Me.dataAccess.GetCourses
        End Function

        Sub New()
            Me._courses = New ObservableCollection(Of Course)
            ServiceLocator.RegisterService(Of ICourseService)(New CourseService)
            Me.dataAccess = GetService(Of ICourseService)()
            For Each elements In Me.GetCourses
                Me._courses.Add(elements)
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

        Public ReadOnly Property ButtonEliminar
            Get
                If eliminar Is Nothing Then
                    eliminar = New RelayCommand(AddressOf Eliminando)
                End If
                Return eliminar
            End Get
        End Property

        Public Sub Creando()
            Dim crear As New CoursesCRUD
            crear.ShowDialog()
            _courses.Clear()
            For Each element In Me.GetCourses
                Me._courses.Add(element)
            Next
        End Sub

        Public Sub Eliminando()
            If Seleccionado IsNot Nothing Then
                Try
                    DataContext.DBEntities.Courses.Remove((From item In DataContext.DBEntities.Courses
                                 Where Seleccionado.CourseID = item.CourseID
                                 Select item).FirstOrDefault)
                    DataContext.DBEntities.SaveChanges()
                    Actualizar()
                Catch ex As Exception
                    MessageBox.Show("No se puede eliminar el curso, tiene asignaciones", MsgBoxStyle.Critical)
                End Try
            End If
        End Sub

        Sub Actualizar()
            Me.Courses.Clear()
            ServiceLocator.RegisterService(Of IDepartmentService)(New CourseService)
            Me.dataAccess = GetService(Of ICourseService)()
            For Each element In Me.GetCourses
                Me._courses.Add(element)
            Next
        End Sub
    End Class
End Namespace
