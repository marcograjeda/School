Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Namespace BusinessLogic.Services.Implementations
    Public Class DepartmentService
        Implements IDepartmentService

        Public Function GetAllDepartments() As IQueryable(Of Department) Implements IDepartmentService.GetAllDepartments
            Return DataContext.DBEntities.Departments
        End Function

        Public Sub CrearDepartment(department As Department) Implements IDepartmentService.CrearDepartment
            DataContext.DBEntities.Departments.Add(department)
            DataContext.DBEntities.SaveChanges()
        End Sub
    End Class
End Namespace

