Imports System
Imports WarehouseToDataMart.Common

Namespace Warehouse

    Public Class Purchase

        Private _id As Integer
        Private _amount As Decimal
        Private _numberOfItems As Integer
        Private _purchaseDate As DateInformation
        Private _personId As Integer

        Public ReadOnly Property Id() As Integer
            Get
                Return _id
            End Get
        End Property

        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property

        Public Property NumberOfItems() As Integer
            Get
                Return _numberOfItems
            End Get
            Set(ByVal value As Integer)
                _numberOfItems = value
            End Set
        End Property

        Public ReadOnly Property PurchaseDate() As DateInformation
            Get
                Return _purchaseDate
            End Get
        End Property

        Public ReadOnly Property PersonId() As Integer
            Get
                Return _personId
            End Get
        End Property

        Public Sub New(ByVal id As Integer, ByVal amount As Decimal, ByVal numberOfItems As Integer, ByVal purchaseDate As DateInformation, ByVal personId As Integer)
            _id = id
            _amount = amount
            _numberOfItems = numberOfItems
            _purchaseDate = purchaseDate
            _personId = personId
        End Sub

        Private Sub New()
            ' for Nhibernate
        End Sub

    End Class

End Namespace