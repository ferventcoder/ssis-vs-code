Namespace DataMart

    Public Class PurchasesPerPersonPerMonthAggregateFact

        Private _id As Integer
        Private _personId As Integer
        Private _monthNumber As Integer
        Private _monthName As String
        Private _purchasesTotal As Decimal

        Public ReadOnly Property Id() As Integer
            Get
                Return _id
            End Get
        End Property

        Public ReadOnly Property PersonId() As Integer
            Get
                Return _personId
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

        Public ReadOnly Property PurchasesTotal() As Decimal
            Get
                Return _purchasesTotal
            End Get
        End Property

        Public Sub New(ByVal id As Integer, ByVal personId As Integer, ByVal monthNumber As Integer, ByVal monthName As String, ByVal purchasesTotal As Decimal)
            _id = id
            _personId = personId
            _monthNumber = monthNumber
            _monthName = monthName
            _purchasesTotal = purchasesTotal
        End Sub

        Private Sub New()
            ' for NHibernate
        End Sub

    End Class

End Namespace