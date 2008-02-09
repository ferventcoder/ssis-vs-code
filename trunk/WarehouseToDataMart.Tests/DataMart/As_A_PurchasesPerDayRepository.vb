Imports System.Collections.Generic
Imports WarehouseToDataMart.DataMart
Imports MbUnit.Framework
Imports WarehouseToDataMart.Repositories

Namespace DataMart

    <TestFixture()> _
    Public Class As_A_PurchasesPerDayRepository

        Private _repository As IRepository(Of PurchasesPerDayAggregateFact)

        <SetUp()> _
        Public Sub I_want_to()
            _repository = New Repository(Of PurchasesPerDayAggregateFact)(HibernateDataMartMapperFactory.SessionFactory)
        End Sub

        <Test()> _
        Public Sub Save_a_New_PurchaseAggregateFact()
            Dim list As IList(Of PurchasesPerDayAggregateFact) = New List(Of PurchasesPerDayAggregateFact) ' = _repository.GetAll()

            Dim purchaseAggregateFact As New PurchasesPerDayAggregateFact(Nothing, CDec(30000.23), 1, #1/1/1900#)
            list.Add(purchaseAggregateFact)
            _repository.Save(list)

            Assert.IsNotNull(purchaseAggregateFact.Id)
        End Sub

    End Class

End Namespace