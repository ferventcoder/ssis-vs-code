Imports System.Collections.Generic
Imports WarehouseToDataMart.Common
Imports MbUnit.Framework
Imports WarehouseToDataMart.Repositories

Namespace Warehouse

    <TestFixture()> _
    Public Class As_A_DateRepository

        Private _repository As IRepository(Of DateInformation)

        <SetUp()> _
        Public Sub I_want_to()
            _repository = New Repository(Of DateInformation)(HibernateWarehouseMapperFactory.SessionFactory)
        End Sub

        <Test()> _
        Public Sub Get_the_dates()
            Dim list As IList(Of DateInformation) = _repository.GetAll()
            Assert.IsNotNull(list)
        End Sub

    End Class

End Namespace