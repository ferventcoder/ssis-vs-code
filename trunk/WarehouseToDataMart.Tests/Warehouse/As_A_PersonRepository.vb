Imports System.Collections.Generic
Imports MbUnit.Framework
Imports WarehouseToDataMart.Warehouse
Imports WarehouseToDataMart.Repositories

Namespace Warehouse

    <TestFixture()> _
    Public Class As_A_PersonRepository

        Private _repository As IRepository(Of Person)

        <SetUp()> _
        Public Sub I_want_to()
            _repository = New Repository(Of Person)(HibernateWarehouseMapperFactory.SessionFactory)
        End Sub

        <Test()> _
        Public Sub Get_a_list_of_people()
            Dim list As IList(Of Person) = _repository.GetAll()
            Assert.IsNotNull(list)
        End Sub

    End Class

End Namespace