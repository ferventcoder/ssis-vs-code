Imports System

Namespace DataMart

    Public Class PersonDimension

        Private _id As Integer
        Private _systemOfRecordId As Integer
        Private _firstName As String
        Private _lastName As String
        Private _effectiveDateFrom As DateTime
        Private _effectiveDateId As Integer
        'Private _effectiveDateTo As DateTime

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

        Public ReadOnly Property EffectiveDateFrom() As DateTime
            Get
                Return _effectiveDateFrom
            End Get
        End Property

        'Public ReadOnly Property EffectiveDateTo() As DateTime
        '    Get
        '        Return _effectiveDateTo
        '    End Get
        'End Property

        Public ReadOnly Property EffectiveDateId() As Integer
            Get
                Return _effectiveDateId
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

        Public Sub New(ByVal id As Integer, ByVal systemOfRecordID As Integer, ByVal firstName As String, ByVal lastName As String, ByVal effectiveDateID As Integer, ByVal effectiveDate As DateTime)
            _id = id
            _systemOfRecordId = systemOfRecordID
            _firstName = firstName
            _lastName = lastName
            _effectiveDateId = effectiveDateID
            _effectiveDateFrom = effectiveDate
        End Sub

        Private Sub New()
            ' for Nhibernate
        End Sub

    End Class

End Namespace