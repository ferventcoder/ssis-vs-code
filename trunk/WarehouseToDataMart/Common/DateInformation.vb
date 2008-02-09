Imports System

Namespace Common

    Public Class DateInformation

        Protected _id As Integer
        Protected _calendarDate As DateTime
        Protected _monthNumber As Integer
        Protected _monthName As String

        Public ReadOnly Property Id() As Integer
            Get
                Return _id
            End Get
        End Property

        Public ReadOnly Property CalendarDate() As DateTime
            Get
                Return _calendarDate
            End Get
        End Property

        Public ReadOnly Property MonthNumber() As Integer
            Get
                Return _monthNumber
            End Get
        End Property

        Public ReadOnly Property MonthName() As String
            Get
                Return _monthName
            End Get
        End Property

        Public Sub New(ByVal id As Integer, ByVal calendarDate As Date, ByVal monthNumber As Integer, ByVal monthName As String)
            _id = id
            _calendarDate = calendarDate
            _monthNumber = monthNumber
            _monthName = monthName
        End Sub

        Private Sub New()
            'for Nhibernate
        End Sub

    End Class

End Namespace