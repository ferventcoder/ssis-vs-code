Imports System

Namespace DataMart

    Public Class PurchasesPerDayAggregateFact

        Private _id As Integer
        Private _purchasesTotal As Decimal
        Private _purchasesDateId As Integer
        Private _purchasesDate As DateTime

        Public ReadOnly Property Id() As Integer
            Get
                Return _id
            End Get
        End Property

        Public Property PurchasesTotal() As Decimal
            Get
                Return _purchasesTotal
            End Get
            Set(ByVal value As Decimal)
                _purchasesTotal = value
            End Set
        End Property

        Public Property PurchasesDateId() As Integer
            Get
                Return _purchasesDateId
            End Get
            Set(ByVal value As Integer)
                _purchasesDateId = value
            End Set
        End Property

        Public Property PurchasesDate() As Date
            Get
                Return _purchasesDate
            End Get
            Set(ByVal value As Date)
                _purchasesDate = value
            End Set
        End Property

        Public Sub New(ByVal id As Integer, ByVal purchasesTotal As Decimal, ByVal purchasesDateId As Integer, ByVal purchasesDate As Date)
            _id = id
            _purchasesTotal = purchasesTotal
            _purchasesDateId = purchasesDateId
            _purchasesDate = purchasesDate
        End Sub

        Public Sub New()
            ' for NHibernate
        End Sub

    End Class

End Namespace