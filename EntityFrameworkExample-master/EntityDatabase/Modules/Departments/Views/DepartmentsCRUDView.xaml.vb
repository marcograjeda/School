Imports Modules.Departments.ViewModels
Public Class DepartmentsCRUDView
    Public Sub New()
        InitializeComponent()
        Me.DataContext = New DepartmentCRUDViewModel(Me)
    End Sub
End Class
