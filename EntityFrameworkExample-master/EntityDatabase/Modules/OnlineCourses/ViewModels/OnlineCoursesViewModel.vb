Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Namespace Modules.OnlineCourses.ViewModels
    Public Class OnlineCoursesViewModels
        Inherits ViewModelBase

        Private _Courses As ObservableCollection(Of OnlineCourse)
        Private dataAccess As IOnlineCourse
        Private crear As ICommand
        Private eliminar As ICommand
        Private _seleccion As OnlineCourse

        Public Property Courses As ObservableCollection(Of OnlineCourse)
            Get
                Return Me._Courses
            End Get
            Set(value As ObservableCollection(Of OnlineCourse))
                Me._Courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        Public Property Seleccionado As OnlineCourse
            Get
                Return _seleccion
            End Get
            Set(value As OnlineCourse)
                _seleccion = value
            End Set
        End Property

        Private Function GetCourses() As IQueryable(Of OnlineCourse)
            Return Me.dataAccess.GetOnlineCourses
        End Function

        Sub New()
            Me._Courses = New ObservableCollection(Of OnlineCourse)
            ServiceLocator.RegisterService(Of IOnlineCourse)(New OnlineCourseService)
            Me.dataAccess = GetService(Of IOnlineCourse)()
            For Each element In Me.GetCourses
                Me._Courses.Add(element)
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
            Dim crear As New OnlineCoursesCRUD
            crear.ShowDialog()
            _courses.Clear()
            For Each element In Me.GetCourses
                Me._courses.Add(element)
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
                Try
                    DataContext.DBEntities.OnlineCourses.Remove((From item In DataContext.DBEntities.OnlineCourses
                                 Where Seleccionado.CourseID = item.CourseID
                                 Select item).FirstOrDefault)
                    DataContext.DBEntities.SaveChanges()
                    Actualizar()
                Catch ex As Exception
                    MessageBox.Show("No se puede eliminar el curso", MsgBoxStyle.Critical)
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
