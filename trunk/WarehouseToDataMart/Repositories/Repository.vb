Imports System.Collections.Generic
Imports NHibernate
Imports NHibernate.Transform

Namespace Repositories

    Public Class Repository(Of T)
        Implements IRepository(Of T)

        Protected _factory As ISessionFactory

        Public Sub New(ByVal factory As ISessionFactory)
            _factory = factory
        End Sub

        Public Function GetAll() As System.Collections.Generic.IList(Of T) Implements IRepository(Of T).GetAll
            Dim list As IList(Of T)
            Dim persistentClass As System.Type = GetType(T)

            Using session As ISession = _factory.OpenSession
                Dim criteria As ICriteria = session.CreateCriteria(persistentClass)
                list = criteria.List(Of T)()
            End Using

            Return list
        End Function

        Public Sub Save(ByVal list As IList(Of T)) Implements IRepository(Of T).Save

            Using session As ISession = _factory.OpenSession
                Using transaction As ITransaction = session.BeginTransaction()
                    For Each item As T In list
                        session.Save(item)
                    Next

                    transaction.Commit()
                End Using
            End Using

        End Sub

        Public Function GetWithCriteria(ByVal detachedCriteria As NHibernate.Expression.DetachedCriteria) As System.Collections.Generic.IList(Of T) Implements IRepository(Of T).GetWithCriteria
            Dim list As IList(Of T)

            Using session As ISession = _factory.OpenSession()
                Dim criteria As ICriteria = detachedCriteria.GetExecutableCriteria(session)
                list = criteria.List(Of T)()
            End Using

            Return list
        End Function

        Public Function GetTransformationWithCriteria(ByVal detachedCriteria As NHibernate.Expression.DetachedCriteria) As System.Collections.Generic.List(Of T) Implements IRepository(Of T).GetTransformationWithCriteria
            Dim list As IList(Of T)

            Using session As ISession = _factory.OpenSession()
                Dim criteria As ICriteria = detachedCriteria.GetExecutableCriteria(session)
                list = criteria _
                        .SetResultTransformer(New AliasToBeanResultTransformer(GetType(T))) _
                        .List(Of T)()
            End Using

            Return CType(list, List(Of T))
        End Function

    End Class

End Namespace