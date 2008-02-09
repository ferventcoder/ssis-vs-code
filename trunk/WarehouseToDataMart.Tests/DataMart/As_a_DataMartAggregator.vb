Imports System.Collections.Generic
Imports WarehouseToDataMart.Repositories
Imports WarehouseToDataMart.DataMart
Imports MbUnit.Framework

Namespace DataMart

    <TestFixture()> _
    Public Class As_a_DataMartAggregator

        Private _repository As IRepository(Of PurchasesPerDayAggregateFact)

        <SetUp()> _
        Public Sub I_want_to()
            _repository = New Repository(Of PurchasesPerDayAggregateFact)(HibernateDataMartMapperFactory.SessionFactory)
        End Sub

        <Test()> _
        Public Sub Get_an_Aggregate_of_Purchases_Per_Day()
            Dim purchasesDayAgg As New DataMartAggregator()
            Dim list As IList(Of PurchasesPerDayAggregateFact) = purchasesDayAgg.AggregatePurchasesPerDay()
            Assert.IsNotNull(list)
            Assert.IsTrue(list.Count > 0)
        End Sub

        <Test()> _
        Public Sub Get_an_Aggregate_of_Purchases_per_Person_per_Month()
            Dim purchasesDayAgg As New DataMartAggregator()
            Dim list As IList(Of PurchasesPerPersonPerMonthAggregateFact) = purchasesDayAgg.AggregatePurchasesPerPersonPerMonth()
            Assert.IsNotNull(list)
            Assert.IsTrue(list.Count > 0)
        End Sub

    End Class

End Namespace