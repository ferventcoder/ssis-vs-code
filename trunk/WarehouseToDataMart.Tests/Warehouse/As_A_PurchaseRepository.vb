Imports System.Collections.Generic
Imports MbUnit.Framework
Imports WarehouseToDataMart.Warehouse
Imports WarehouseToDataMart.Repositories

Namespace Warehouse

    <TestFixture()> _
    Public Class As_A_PurchaseRepository

        Private _repository As IRepository(Of Purchase)

        <SetUp()> _
        Public Sub I_want_to()
            _repository = New Repository(Of Purchase)(HibernateWarehouseMapperFactory.SessionFactory)
        End Sub

        <Test()> _
        Public Sub Get_a_list_of_purchases()
            Dim list As IList(Of Purchase) = _repository.GetAll()
            Assert.IsNotNull(list)
        End Sub

    End Class

End Namespace