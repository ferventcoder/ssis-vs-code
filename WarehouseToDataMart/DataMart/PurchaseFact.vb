Imports System
Imports WarehouseToDataMart.Common

Namespace DataMart

    Public Class PurchaseFact

        Private _id As Integer
        Private _amount As Decimal
        Private _numberOfItems As Integer
        Private _purchaseDateId As Integer
        Private _purchaseDate As DateTime
        Private _personId As Integer
        Private _purchaseDateInformation As DateInformation

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

        Public ReadOnly Property PurchaseDateInformation() As DateInformation
            Get
                Return _purchaseDateInformation
            End Get
        End Property

        Public ReadOnly Property PurchaseDateId() As Integer
            Get
                If (_purchaseDateId = Nothing) Then
                    _purchaseDateId = PurchaseDateInformation.Id
                End If

                Return _purchaseDateId
            End Get
        End Property

        Public ReadOnly Property PurchaseDate() As DateTime
            Get
                If (_purchaseDate < #1/1/1900#) Then
                    _purchaseDate = PurchaseDateInformation.CalendarDate
                End If

                Return _purchaseDate
            End Get
        End Property

        Public ReadOnly Property PersonId() As Integer
            Get
                Return _personId
            End Get
        End Property

        Public Sub New(ByVal id As Integer, ByVal amount As Decimal, ByVal numberOfItems As Integer, ByVal purchaseDateInformation As DateInformation, ByVal personId As Integer)
            _id = id
            _amount = amount
            _numberOfItems = numberOfItems
            _personId = personId
            _purchaseDateInformation = purchaseDateInformation
            _purchaseDateId = purchaseDateInformation.Id
            _purchaseDate = purchaseDateInformation.CalendarDate
        End Sub

        Private Sub New()
            ' for Nhibernate
        End Sub

    End Class

End Namespace