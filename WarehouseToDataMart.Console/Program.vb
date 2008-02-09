Imports WarehouseToDataMart

Module Program

    Sub Main()
        Dim WHProgram As New WarehouseMoveProgram
        WHProgram.Run()
        System.Console.WriteLine("Please hit enter to continue...")
        System.Console.ReadLine()
    End Sub

End Module