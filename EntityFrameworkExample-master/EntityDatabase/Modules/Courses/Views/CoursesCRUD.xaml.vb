Imports Modules.Courses.ViewModels
Public Class CoursesCRUD
    Public Sub New()
        InitializeComponent()
        Me.DataContext = New CoursesCRUDViewModel(Me)
    End Sub
End Class
