Imports System.Collections.Generic
Imports NHibernate.Expression
Imports WarehouseToDataMart.Repositories
Imports WarehouseToDataMart.DataMart

Public Class DataMartAggregator


    Private _purchasesPerDayCriteria As DetachedCriteria = DetachedCriteria.For(Of PurchaseFact)() _
        .SetProjection(Projections.ProjectionList() _
            .Add(Projections.Sum("Amount"), "PurchasesTotal") _
            .Add(Projections.Property("PurchaseDateId"), "PurchasesDateId") _
            .Add(Projections.Property("PurchaseDate"), "PurchasesDate") _
            .Add(Projections.GroupProperty("PurchaseDateId")) _
            .Add(Projections.GroupProperty("PurchaseDate")) _
        ) _
        .AddOrder(Order.Asc("PurchaseDateId"))


    Public Function AggregatePurchasesPerDay() As IList(Of PurchasesPerDayAggregateFact)
        Return New Repository(Of PurchasesPerDayAggregateFact)(HibernateDataMartMapperFactory.SessionFactory).GetTransformationWithCriteria(_purchasesPerDayCriteria)
    End Function

    Public Function AggregatePurchasesPerPersonPerMonth() As IList(Of PurchasesPerPersonPerMonthAggregateFact)
        Dim list As IList(Of PurchasesPerPersonPerMonthAggregateFact) = New List(Of PurchasesPerPersonPerMonthAggregateFact)


        'Using session As ISession = HibernateDataMartMapperFactory.SessionFactory.OpenSession

        'Dim crit As ICriteria = session.CreateCriteria(GetType(PurchaseFact)) _
        '    .SetProjection(Projections.ProjectionList() _
        '        .Add(Projections.Sum("Amount"), "PurchasesTotal") _
        '        .Add(Projections.Property("PersonId"), "PersonId") _
        '        .Add(Projections.GroupProperty("PersonId")) _
        '    )

        'Dim o As Object = crit.List

        'Dim crit2 As ICriteria = session.CreateCriteria(GetType(DateInformation)) _
        '    .SetProjection(Projections.ProjectionList() _
        '        .Add(Projections.Property("MonthNumber")) _
        '        .Add(Projections.Property("MonthName")) _
        '        .Add(Projections.GroupProperty("MonthNumber")) _
        '        .Add(Projections.GroupProperty("MonthName")) _
        '    ) _
        '    .AddOrder(Order.Asc("MonthNumber"))

        'Dim o2 As Object = crit2.List()


        'Dim list2 As Object = session.CreateSQLQuery( _
        '        "SELECT p.Amount, m.MonthNumber " & _
        '        "FROM PurchaseFact p, DateInformationDimension m" _
        '    ) _
        '    .AddEntity(GetType(PurchaseFact))


        'Dim list2 As Object = session.CreateSQLQuery( _
        '    "SELECT  " & _
        '       "SUM({p}.PurchaseAmount) as TotalPurchases " & _
        '       ",{p}.PersonID " & _
        '       ",{m}.MonthInYearNum " & _
        '       ",{m}.MonthName " & _
        '    "FROM PeopleSalesDM.dbo.PurchaseFacts {p} " & _
        '        ",MasterDatesDim {m} " & _
        '    "WHERE {p}.PurchaseDateID = {m}.DateID " & _
        '    "GROUP BY {p}.PersonID, {m}.MonthInYearNum, {m}.MonthName " & _
        '    "ORDER BY {m}.MonthInYearNum,{p}.PersonID " _
        '    ) _
        '.List()





        '"SELECT p.Amount  " & _
        '   "FROM PurchaseFact p " & _
        '   "INNER JOIN DateInformationDimension m " & _
        '       "ON m.Id = p.PurchaseDateId " _



        Dim i As Integer = 0

        'End Using

        Return list
    End Function

End Class