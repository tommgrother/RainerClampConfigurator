Imports System.IO
Module Module1

    Sub Main()

        Dim dirString As String = "c:\temp\r\"

        Dim di As New DirectoryInfo(dirString)




        Dim fileArray As FileInfo() = di.GetFiles()

        Dim fileI As FileInfo

        Console.WriteLine("Please place all files to be converted into 'C:\temp\r' folder. Press any key to continue.")
        Console.ReadLine()

        For Each fileI In fileArray
            Dim clipData As String
            Dim clamp1 As Double
            Dim clamp2 As Double
            Dim clamp3 As Double
            Dim clamp4 As Double

            Dim newclampFull As String
            Dim oldClampFull As String

            Dim doorNumber As String

            doorNumber = fileI.ToString
            doorNumber = doorNumber.Substring(0, 6)


            For Each line As String In File.ReadLines(dirString & doorNumber & ".mpf")
                If line.Contains("AUTOSET_CLP") Then
                    clipData = line.ToString()

                End If
            Next

            Dim bracketValue() As String = clipData.Split("(")
            bracketValue(1) = bracketValue(1).TrimEnd(")")

            Dim bracketString As String




            bracketString = bracketValue(1).ToString


            oldClampFull = "AUTOSET_CLP(" & bracketString & ")"

            Dim arrayPlacements() As String

            arrayPlacements = bracketString.Split(",")

            For i = 0 To UBound(arrayPlacements)
                Select Case (i)
                    Case 0
                        clamp1 = arrayPlacements(i)
                    Case 1
                        clamp2 = Convert.ToDouble(arrayPlacements(i))
                    Case 2
                        clamp3 = Convert.ToDouble(arrayPlacements(i))
                    Case 3
                        clamp4 = Convert.ToDouble(arrayPlacements(i))
                End Select
            Next



            newclampFull = "AUTOSET_CLP(" & clamp1 & ".0," & clamp3 & ".0,0," & clamp4 & ")"




            My.Computer.FileSystem.WriteAllText(dirString & doorNumber & ".mpf", My.Computer.FileSystem.ReadAllText(dirString & doorNumber & ".mpf").Replace(oldClampFull, newclampFull), False)

            Console.Write(fileI.ToString & " Clamps moved" & vbCrLf)

        Next

        Console.ReadLine()






    End Sub

End Module
