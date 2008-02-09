Imports System.Collections.Generic
Imports NHibernate.Expression

Namespace Repositories

    Public Interface IRepository(Of T)

        Function GetAll() As IList(Of T)
        Function GetWithCriteria(ByVal detachedCriteria As DetachedCriteria) As IList(Of T)
        Function GetTransformationWithCriteria(ByVal detachedCriteria As DetachedCriteria) As List(Of T)
        Sub Save(ByVal list As IList(Of T))

    End Interface

End Namespace