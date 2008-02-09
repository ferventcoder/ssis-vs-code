Imports System.Collections.Generic
Imports WarehouseToDataMart.Common
Imports WarehouseToDataMart.DataMart
Imports MbUnit.Framework
Imports WarehouseToDataMart.Repositories

Namespace DataMart

    <TestFixture()> _
    Public Class As_A_PurchaseFactRepository


        Private _repository As IRepository(Of PurchaseFact)

        <SetUp()> _
        Public Sub I_want_to()
            _repository = New Repository(Of PurchaseFact)(HibernateDataMartMapperFactory.SessionFactory)

        End Sub

        <Test()> _
        Public Sub Get_a_list_of_PurchaseFacts()
            Dim list As IList(Of PurchaseFact) = _repository.GetAll()
            Assert.IsNotNull(list)
            Assert.IsTrue(list.Count > 0)
        End Sub

        <Test()> _
        Public Sub Save_a_New_PurchaseFact()
            Dim list As IList(Of PurchaseFact) = New List(Of PurchaseFact) ' = _repository.GetAll()

            Dim purchase As New PurchaseFact(99999993, CDec(3000.25), 2, New DateInformation(12, #1/12/1900#, 1, "January"), 32)
            list.Add(purchase)
            _repository.Save(list)

            Assert.IsNotNull(purchase.Id)
        End Sub

    End Class

End Namespace