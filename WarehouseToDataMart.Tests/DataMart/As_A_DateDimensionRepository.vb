Imports System.Collections.Generic
Imports WarehouseToDataMart.Common
Imports MbUnit.Framework
Imports WarehouseToDataMart.Repositories

Namespace DataMart

    <TestFixture()> _
    Public Class As_A_DateDimensionRepository

        Private _repository As IRepository(Of DateInformation)

        <SetUp()> _
      Public Sub I_want_to()
            _repository = New Repository(Of DateInformation)(HibernateDataMartMapperFactory.SessionFactory)

        End Sub

        <Test()> _
        Public Sub Save_a_New_DateDimension()
            Dim list As IList(Of DateInformation) = New List(Of DateInformation) ' = _repository.GetAll()

            Dim dateInformation As New DateInformation(999991, #8/25/2896#, 8, "August")
            list.Add(dateInformation)
            _repository.Save(list)

            'Assert.IsNotNull(person.Id)
        End Sub


    End Class

End Namespace