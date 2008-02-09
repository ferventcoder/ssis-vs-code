Imports System
Imports WarehouseToDataMart.Common

Namespace Warehouse

    Public Class Person

        Private _id As Integer
        Private _systemOfRecordId As Integer
        Private _firstName As String
        Private _lastName As String
        Private _effectiveDate As DateInformation
        'Private _purchases As IList(Of Purchase)

        Public ReadOnly Property Id() As Integer
            Get
                Return _id
            End Get
        End Property

        Public ReadOnly Property SystemOfRecordId() As Integer
            Get
                Return _systemOfRecordId
            End Get
        End Property

        Public ReadOnly Property EffectiveDate() As DateInformation
            Get
                Return _effectiveDate
            End Get
        End Property

        Public Property FirstName() As String
            Get
                Return _firstName
            End Get
            Set(ByVal value As String)
                _firstName = value
            End Set
        End Property

        Public Property LastName() As String
            Get
                Return _lastName
            End Get
            Set(ByVal value As String)
                _lastName = value
            End Set
        End Property

        'Public ReadOnly Property Purchases() As IList(Of Purchase)
        '    Get
        '        Return _purchases
        '    End Get
        'End Property

        Public Sub New(ByVal id As Integer, ByVal systemOfRecordID As Integer, ByVal firstName As String, ByVal lastName As String, ByVal effectiveDate As DateInformation)
            _id = id
            _systemOfRecordId = systemOfRecordID
            _firstName = firstName
            _lastName = lastName
            _effectiveDate = effectiveDate
        End Sub

        Private Sub New()
            ' for Nhibernate
        End Sub

    End Class

End Namespace