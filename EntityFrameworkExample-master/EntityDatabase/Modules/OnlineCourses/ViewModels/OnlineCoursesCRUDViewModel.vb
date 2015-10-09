Imports Modules.OnlineCourses.Views
Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers
Imports System.Collections.ObjectModel
Namespace Modules.OnlineCourses.ViewModels
    Public Class OnlineCoursesCRUDViewModel
        Inherits ViewModelBase

        Private _newView As OnlineCoursesCRUD
        Private _Online As New OnlineCourse
        Private onlineEdit As OnlineCourse
        Private _Courses As ObservableCollection(Of Course)
        Private _selectItem As Course
        Private _Url As String
        Private aceptCommand As ICommand
        Private cancelCommand As ICommand

        Public Property Courses As ObservableCollection(Of Course)
            Get
                Return _Courses
            End Get
            Set(value As ObservableCollection(Of Course))
                _Courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        Public Property SelectCourse As Course
            Get
                Return _selectItem
            End Get
            Set(value As Course)
                _Online.Course = value
                _selectItem = value
                OnPropertyChanged("SelectCourse")
            End Set
        End Property

        Public Property Url As String
            Get
                Return Me._Url
            End Get
            Set(value As String)
                Me._Url = value
                _Online.URL = _Url
                OnPropertyChanged("Url")
            End Set
        End Property

        Public ReadOnly Property AcceptButton As ICommand
            Get
                If Me.aceptCommand Is Nothing Then
                    Me.aceptCommand = New RelayCommand(AddressOf AceptExecute)
                End If
                Return Me.aceptCommand
            End Get
        End Property

        Sub AceptExecute()
            Try
                If onlineEdit Is Nothing Then
                    DataContext.DBEntities.OnlineCourses.Add(_Online)
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                Else
                    Dim Online As OnlineCourse = (From item In DataContext.DBEntities.OnlineCourses Where item.CourseID = onlineEdit.CourseID
                                   Select item).FirstOrDefault()
                    Online.Course = _selectItem
                    Online.URL = _Url
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("No se ha podido agregar")
            End Try
        End Sub

        Sub New(ByRef _newView As OnlineCoursesCRUD)
            Me._newView = _newView
            _Courses = New ObservableCollection(Of Course)
            Dim Courses As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In Courses
                _Courses.Add(element)
            Next
        End Sub
    End Class
End Namespace
