Imports System.Text

'http://www.codeproject.com/Articles/11142/Generate-a-Random-String-Key-in-VB-NET

Public Class RandomKeyGenerator

    Public Property Letters() As String
    Public Property Numbers() As String
    Public Property CharCount() As Integer
    Public Property Capatalize As Boolean

    Public Sub New(intKeyChars As Integer)

        Me.New(intKeyChars,
               SettingsWrapper.RandomKey_AllowCapitalLetters)

    End Sub

    Public Sub New(intKeyChars As Integer,
                   Optional ByVal bolCapitalize As Boolean = True)

        Me.New(intKeyChars,
               SettingsWrapper.RandomKey_Letters,
               SettingsWrapper.RandomKey_Numbers,
               bolCapitalize)

    End Sub

    Public Sub New(intKeyChars As Integer,
                   strKeyLetters As String,
                   strKeyNumbers As String,
                   Optional ByVal bolCapitalize As Boolean = True)

        Me.CharCount = intKeyChars
        Me.Letters = strKeyLetters
        Me.Numbers = strKeyNumbers
        Me.Capatalize = bolCapitalize

    End Sub

    Public Function Generate() As String

        Dim intKey As Integer
        Dim sngRandom As Single
        Dim arrIndex As Int16
        Dim stb As New StringBuilder
        Dim strLetter As String

        'CONVERT LettersArray & NumbersArray TO CHARACTR ARRAYS
        Dim LettersArray As Char() = Me.Letters.ToCharArray
        Dim NumbersArray As Char() = Me.Numbers.ToCharArray

        For intKey = 1 To Me.CharCount

            Randomize()
            sngRandom = Rnd()
            arrIndex = -1

            'IF THE VALUE IS AN EVEN NUMBER WE GENERATE A LETTER, OTHERWISE WE GENERATE A NUMBER  
            'THE NUMBER '111' WAS RANDOMLY CHOSEN. ANY NUMBER WILL DO, WE JUST NEED TO BRING THE VALUE ABOVE '0'
            If NumbersArray.Length > 1 AndAlso (CType(sngRandom * 111, Integer)) Mod 2 = 0 Then

                'GENERATE A RANDOM INDEX IN THE LETTERS CHARACTER ARRAY
                Do While arrIndex < 0
                    arrIndex = Convert.ToInt16(LettersArray.GetUpperBound(0) * sngRandom)
                Loop
                strLetter = LettersArray(arrIndex)

                'CREATE ANOTHER RANDOM NUMBER. IF IT IS ODD, WE CAPITALIZE THE LETTER
                If (CType(arrIndex * sngRandom * 99, Integer)) Mod 2 <> 0 Then
                    strLetter = LettersArray(arrIndex).ToString
                    If Me.Capatalize = True Then strLetter = strLetter.ToUpper
                End If
                stb.Append(strLetter)
            Else
                'GENERATE A RANDOM INDEX IN THE NUMBERS CHARACTER ARRAY
                Do While arrIndex < 0
                    arrIndex = Convert.ToInt16(NumbersArray.GetUpperBound(0) * sngRandom)
                Loop
                stb.Append(NumbersArray(arrIndex))
            End If
        Next

        Return stb.ToString

    End Function

    Public Function GenerateLettersOnly() As String

        Dim intKey As Integer
        Dim sngRandom As Single
        Dim arrIndex As Int16
        Dim stb As New StringBuilder
        Dim strLetter As String

        'CONVERT LettersArray & NumbersArray TO CHARACTR ARRAYS
        Dim LettersArray As Char() = Me.Letters.ToCharArray
        Dim NumbersArray As Char() = Me.Numbers.ToCharArray

        For intKey = 1 To Me.CharCount

            Randomize()
            sngRandom = Rnd()
            arrIndex = -1

            'GENERATE A RANDOM INDEX IN THE LETTERS CHARACTER ARRAY
            Do While arrIndex < 0
                arrIndex = Convert.ToInt16(LettersArray.GetUpperBound(0) * sngRandom)
            Loop
            strLetter = LettersArray(arrIndex)

            'CREATE ANOTHER RANDOM NUMBER. IF IT IS ODD, WE CAPITALIZE THE LETTER
            If (CType(arrIndex * sngRandom * 99, Integer)) Mod 2 <> 0 Then
                strLetter = LettersArray(arrIndex).ToString
                If Me.Capatalize = True Then strLetter = strLetter.ToUpper
            End If
            stb.Append(strLetter)

        Next

        Return stb.ToString

    End Function

    Public Function GenerateNumbersOnly() As String

        Dim intKey As Integer
        Dim sngRandom As Single
        Dim arrIndex As Int16
        Dim stb As New StringBuilder

        'CONVERT LettersArray & NumbersArray TO CHARACTR ARRAYS
        Dim LettersArray As Char() = Me.Letters.ToCharArray
        Dim NumbersArray As Char() = Me.Numbers.ToCharArray

        For intKey = 1 To Me.CharCount

            Randomize()
            sngRandom = Rnd()
            arrIndex = -1

            'GENERATE A RANDOM INDEX IN THE NUMBERS CHARACTER ARRAY
            Do While arrIndex < 0
                arrIndex = Convert.ToInt16(NumbersArray.GetUpperBound(0) * sngRandom)
            Loop
            stb.Append(NumbersArray(arrIndex))

        Next

        Return stb.ToString

    End Function

End Class
