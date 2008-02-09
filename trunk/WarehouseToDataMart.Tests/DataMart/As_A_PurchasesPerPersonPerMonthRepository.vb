Imports System.Collections.Generic
Imports WarehouseToDataMart.Repositories
Imports WarehouseToDataMart.DataMart
Imports MbUnit.Framework

Namespace DataMart

    <TestFixture()> _
    Public Class As_A_PurchasesPerPersonPerMonthRepository

        Private _repository As IRepository(Of PurchasesPerPersonPerMonthAggregateFact)

        <SetUp()> _
        Public Sub I_want_to()
            _repository = New Repository(Of PurchasesPerPersonPerMonthAggregateFact)(HibernateDataMartMapperFactory.SessionFactory)
        End Sub

        <Test()> _
        Public Sub Save_a_New_PurchaseAggregateFact()
            Dim list As IList(Of PurchasesPerPersonPerMonthAggregateFact) = New List(Of PurchasesPerPersonPerMonthAggregateFact) ' = _repository.GetAll()

            Dim purchaseAggregateFact As New PurchasesPerPersonPerMonthAggregateFact(Nothing, 2, 1, "January", CDec(30000.23))
            list.Add(purchaseAggregateFact)

            _repository.Save(list)

            Assert.IsNotNull(purchaseAggregateFact.Id)
        End Sub


    End Class

End Namespace