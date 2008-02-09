Imports System.Collections.Generic
Imports WarehouseToDataMart.DataMart
Imports MbUnit.Framework
Imports WarehouseToDataMart.Repositories

Namespace DataMart

    <TestFixture()> _
    Public Class As_A_PersonDimensionRepository

        Private _repository As IRepository(Of PersonDimension)

        <SetUp()> _
        Public Sub I_want_to()
            _repository = New Repository(Of PersonDimension)(HibernateDataMartMapperFactory.SessionFactory)

        End Sub

        <Test()> _
        Public Sub Save_a_New_PersonDimension()
            Dim list As IList(Of PersonDimension) = New List(Of PersonDimension) ' = _repository.GetAll()

            Dim person As New PersonDimension(99999993, 23, "Bob", "Buckwheat", 30000, #2/19/1982#)
            list.Add(person)
            _repository.Save(list)

            'Assert.IsNotNull(person.Id)
        End Sub

    End Class

End Namespace