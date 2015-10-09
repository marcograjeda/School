Imports Modules.OnlineCourses.ViewModels
Public Class OnlineCoursesCRUD
    Public Sub New()
        InitializeComponent()
        Me.DataContext = New OnlineCoursesCRUDViewModel(Me)
    End Sub
End Class
