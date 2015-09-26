
Imports Modules.Person.ViewModels
Imports Modules.Courses.ViewModels
Imports Modules.Departments.ViewModels
Imports Modules.OnlineCourses.ViewModels
Imports Modules.OnsiteCourses.ViewModels
Imports Modules.StudentGrade.ViewModels
Class MainWindow
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Me.CoursesUserControl.MainGrid.DataContext = New CoursesViewModel()
        'Me.OnsiteCoursesUserControl.MainGrid.DataContext = New OnsiteCoursesViewModel()
        'Me.OnsiteCoursesUserControl.MainGrid.DataContext = New OnsiteCoursesViewModel()
        'Me.OnlineCoursesUserControl.MainGrid.DataContext = New OnlineCoursesViewModels()
        'Me.OnsiteCoursesUserControl.MainGrid.DataContext = New OnsiteCoursesViewModel()
        Me.PersonsUserControl.MainGrid.DataContext = New PersonViewModel()
    End Sub
End Class
