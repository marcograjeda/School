Namespace BusinessLogic.Services.Interfaces
    Public Interface IDepartmentService

        Function GetAllDepartments() As IQueryable(Of Department)

        Sub CrearDepartment(department As Department)
    End Interface
End Namespace

