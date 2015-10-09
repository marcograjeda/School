Imports Modules.Person.ViewModels
Imports Modules.Courses.ViewModels
Imports Modules.Departments.ViewModels
Imports Modules.OnlineCourses.ViewModels
Imports Modules.OnsiteCourses.ViewModels
Imports Modules.StudentGrade.ViewModels
Imports Modules.OfficeAsignament.ViewModels

Class MainWindow
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.CoursesUserControl.MainGrid.DataContext = New CoursesViewModel()
        Me.DepartmentUserControl.MainGrid.DataContext = New DepartmentsViewModel()
        Me.OnlineCoursesUserControl.DataContext = New OnlineCoursesViewModels()
        Me.OnsiteCoursesUserControl.DataContext = New OnsiteCoursesViewModel()
        Me.PersonsUserControl.MainGrid.DataContext = New PersonViewModel()
        Me.OfficeAssignmentsUserControl.MainGrid.DataContext = New OfficeAsignamentViewModel()
        Me.StudentGradesUserControl.MainGrid.DataContext = New StudentGradeViewModel()
    End Sub
End Class
