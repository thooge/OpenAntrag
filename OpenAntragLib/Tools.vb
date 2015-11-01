Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Web
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.Security

Public Module Tools

    Public Function GetCookie(ByVal key As String,
                              Optional ByVal returnEmptyString As Boolean = False) As String

        If HttpContext.Current.Request.Cookies(key) IsNot Nothing Then
            Return HttpContext.Current.Request.Cookies(key).Value
        Else
            If returnEmptyString = True Then
                Return ""
            Else
                Return Nothing
            End If
        End If

    End Function

    Public Function GetRequestDomain() As String

        Dim arrAuthority As String() = HttpContext.Current.Request.Url.Authority.Split(".")

        Dim stb As New StringBuilder

        If arrAuthority.Length >= 2 Then
            stb.Append(arrAuthority(arrAuthority.Length - 2)).Append(".")
        End If

        stb.Append(arrAuthority(arrAuthority.Length - 1))

        Return stb.ToString

    End Function

    <Extension()>
    Public Function RepresentationRoles(user As System.Security.Principal.IPrincipal) As String()

        Dim arrRepRoles As String()
        Dim arrRoles As String() = Roles.GetRolesForUser(user.Identity.Name)

        arrRepRoles = (From s As String In arrRoles
                       Where s.ToUpper() <> "ADMIN"
                       Select s).ToArray()

        Return arrRepRoles

    End Function

    Public Function IsAdmin(Optional repKey As String = Nothing) As Boolean

        If HttpContext.Current.User.IsInRole("admin") = True OrElse
            (String.IsNullOrEmpty(repKey) = False And HttpContext.Current.User.IsInRole(repKey) = True) Then
            Return True
        End If

        Return False

    End Function

    Public Function IsCreatedByAdmin(repKey As String, createdBy As String) As Boolean

        If String.IsNullOrEmpty(createdBy) = False Then
            If HttpContext.Current.User.IsInRole("admin") = True Then
                Return True
            Else
                If IsAdmin(repKey) = True Then
                    Return (HttpContext.Current.User.Identity.Name.ToLower = createdBy.ToLower)
                End If
            End If
        End If

        Return False

    End Function

    Public Function ControllerExists(strControllerNamePart As String) As Boolean

        'http://stackoverflow.com/questions/7033428/how-to-make-sure-controller-and-action-exists-before-doing-redirect-asp-net-mvc

        Dim stbControllerName As New StringBuilder(strControllerNamePart.ToLower)
        stbControllerName.Append("controller")

        Dim types As Type() = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
        Dim type As Type = types.Where(Function(t) t.Name.ToLower = stbControllerName.ToString).SingleOrDefault

        If type IsNot Nothing Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function GetUnixTimestampFromDate(dat As DateTime) As Integer

        Dim datStart As DateTime = #1/1/1970#
        Dim ts As TimeSpan

        ts = dat.Subtract(datStart)
        Return CType(Math.Abs(ts.TotalSeconds()), Integer)

    End Function

    Public Function GetDateFromTimestamp(ByVal intTimestamp As Integer) As DateTime

        Dim ts As TimeSpan
        Dim datStart As Date = #1/1/1970#

        If intTimestamp = 0 Then Return datStart

        ts = New TimeSpan(0, 0, intTimestamp)
        Return datStart.Add(ts)

    End Function

    Public Function FormatTwoDates(strDate1 As String, strDate2 As String) As String

        Dim dat1 As DateTime = CType(strDate1, DateTime)
        Dim dat2 As DateTime = CType(strDate2, DateTime)

        Dim intDayDelta As Integer = DateDiff("d", dat1, dat2)
        Dim intMonthDelta As Integer = dat2.Month - dat1.Month
        Dim intYearDelta As Integer = dat2.Year - dat1.Year

        Dim stb As New StringBuilder

        If intYearDelta <> 0 Then
            stb.Append(Format(dat1, "dd. MMMM yyyy"))
            stb.Append(" - ")
            stb.Append(Format(dat2, "dd. MMMM yyyy"))
        ElseIf intMonthDelta <> 0 Then
            stb.Append(Format(dat1, "dd. MMMM "))
            stb.Append(" - ")
            stb.Append(Format(dat2, "dd. MMMM yyyy"))
        Else
            stb.Append(Format(dat1, "dd. "))
            If intDayDelta = 1 Then
                stb.Append(" / ")
            Else
                stb.Append(" - ")
            End If
            stb.Append(Format(dat2, "dd. MMMM yyyy"))
        End If

        Return stb.ToString

    End Function

    Public Function GetMd5(ByVal str As String) As String

        'Dim strHash As String = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str.Trim(), "MD5")
        'strHash = strHash.Trim().ToLower()

        Dim strHash As String
        Using md5Hash As MD5 = MD5.Create()
            Dim data As Byte() = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str))
            Dim sBuilder As New StringBuilder()

            Dim i As Integer
            For i = 0 To data.Length - 1
                sBuilder.Append(data(i).ToString("x2"))
            Next i

            strHash = sBuilder.ToString().Trim().ToLower()

        End Using

        Return strHash

    End Function

    Public Function VerifyMd5(ByVal input As String, ByVal hash As String) As Boolean

        Dim hashOfInput As String = GetMd5(input)

        Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase

        If comparer.Compare(hashOfInput, hash.Trim().ToLower()) = 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    <Extension()>
    Public Function GetExtension(strFileName As String) As String

        Dim arr As String() = Split(strFileName, ".")
        Return arr(arr.Length - 1)

    End Function

    <Extension()>
    Public Function GetFilesMultiExtension(ByVal di As DirectoryInfo,
                                           ByVal strExtensionList As String) As FileInfo()

        Dim lstFileInfo As New List(Of FileInfo)
        Dim arrExtensions As String() = Split(strExtensionList, ",")

        For Each ext As String In arrExtensions
            lstFileInfo.AddRange(di.GetFiles("*." & ext))
        Next

        Return lstFileInfo.ToArray()

    End Function

    <Extension()>
    Public Function RandomizeList(Of T)(ByVal list As List(Of T)) As List(Of T)

        Dim rnd As New Random()
        Return list.OrderBy(Function(m) rnd.Next()).ToList()

    End Function

    <Extension()>
    Public Function ToFourDigitYear(intToDigitYear As Integer) As Integer

        Dim stb As New StringBuilder
        stb.Append(20).Append(intToDigitYear)
        Return CType(stb.ToString, Integer)

    End Function

    <Extension()>
    Public Sub Prepend(ByRef str As String,
                       ByVal strPrepend As String,
                       Optional ByVal strDelimiter As String = "")
        str = String.Concat(strPrepend, strDelimiter, str)
    End Sub

    <Extension()>
    Public Sub Prepend(ByRef stb As StringBuilder,
                       ByVal strValue As String,
                       Optional ByVal strDelimiter As String = "")

        Dim stbNew As New StringBuilder(strValue)
        If stb.ToString.Length > 0 AndAlso strDelimiter.Length > 0 Then stbNew.Append(strDelimiter)
        stbNew.Append(stb.ToString())
        stb = stbNew

    End Sub

    <Extension()>
    Public Function CleanHtmlCode(strHtml As String,
                                  Optional ByVal bolCleanEmptyTags As Boolean = False) As String

        Dim strRetVal As String

        'Whitespace
        Dim regex1 As New Regex(">[\s]*<")
        strRetVal = regex1.Replace(strHtml, "><")

        'Empty Tags
        If bolCleanEmptyTags = True Then
            Dim regex2 As New Regex("<(\w+)\b[^>]*>\s*</\1\s*>")
            strRetVal = regex2.Replace(strHtml, "")
        End If

        'Linebreaks
        strRetVal = strRetVal.Replace(vbCrLf, "").Replace(vbCr, "")

        Return strRetVal

    End Function

    <Extension()>
    Public Function StripHtmlCode(strHtml As String) As String

        Dim oRegEx As New Regex("<.*?>")

        Return oRegEx.Replace(strHtml, "")

    End Function

    <Extension()>
    Public Function StripSpecialCharsForTitle(strText As String) As String
        Return Regex.Replace(strText, "[^\w-_ ]+", "")
    End Function

    <Extension()>
    Public Function CutEllipsis(stbText As StringBuilder, intMaxLength As Integer) As String
        Return stbText.ToString.CutEllipsis(intMaxLength)
    End Function

    <Extension()>
    Public Function CutEllipsis(strText As String, intMaxLength As Integer) As String

        If strText.Length <= intMaxLength Then
            Return strText
        Else
            Return String.Concat(Left(strText, intMaxLength - 3), "...")
        End If

    End Function

    <Extension()>
    Public Function PreserveLines(strValue As String) As String

        Dim stb As New StringBuilder(strValue)
        stb.Replace(vbCrLf, "\n")
        Return stb.ToString

    End Function

    <Extension()>
    Public Function EnsureMarkdown(strValue As String) As String

        Return strValue.StripHtmlCode.PreserveLines

    End Function

    <Extension()>
    Public Function ShowNone(strValue) As String

        If String.IsNullOrEmpty(strValue) = False Then
            Return strValue
        Else
            Return "- keine Angabe -"
        End If

    End Function

    <Extension()>
    Public Function HasHtmlCode(strValue As String) As Boolean

        Dim oRegEx As New Regex("<.*?>")
        Return oRegEx.IsMatch(strValue)

    End Function

    <Extension()>
    Public Function ToLocalUrl(strUrlPart As String) As String

        Dim strUrl As String = Nothing

        If strUrlPart IsNot Nothing Then
            If strUrlPart.Length > 1 AndAlso
                Not strUrlPart.StartsWith("//") AndAlso
                Not strUrlPart.StartsWith("/\\") Then
                strUrl = String.Concat("http://", HttpContext.Current.Request.Url.Authority, strUrlPart)
            End If
        End If

        Return strUrl

    End Function

    Public Function IsValidMail(strMail As String) As Boolean
        Try
            Static rx As New Regex("^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$")
            Return rx.IsMatch(strMail)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function IsUri(strUrl As String) As Boolean
        Dim bolRetVal As Boolean = False
        Try
            Dim u As New Uri(strUrl)
            If u IsNot Nothing Then bolRetVal = True
        Catch ex As Exception
        End Try
        Return bolRetVal
    End Function

    Public Function IsValidUrl(strUrl As String) As Boolean
        Try
            'Const strRegExPattern As String = "^(?i)\b((?:https?://|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'.,<>?«»""‘’]))$"
            Const strRegExPattern As String = "^(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\?([^#]*))?(#(.*))?"

            Static rx As New Regex(strRegExPattern)
            Return rx.IsMatch(strUrl)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetRfc822Date(ByVal [date] As DateTime) As String

        'http://madskristensen.net/post/Convert-a-date-to-the-RFC822-standard-for-use-in-RSS-feeds.aspx

        Dim offset As Integer = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours

        Dim timeZone1 As String = "+" & offset.ToString().PadLeft(2, "0"c)

        If offset < 0 Then
            Dim i As Integer = offset * -1
            timeZone1 = "-" & i.ToString().PadLeft(2, "0"c)
        End If

        Return [date].ToString("ddd, dd MMM yyyy HH:mm:ss " & timeZone1.PadRight(5, "0"c))

    End Function

    Public Function MakeReadableUrl(ByVal strValue As String,
                                    Optional ByVal bolExcludeDot As Boolean = False) As String

        strValue = strValue.ToLower

        strValue = Regex.Replace(strValue, "[\s_]", "-") '(Leerezeichen und Interpunktion)

        strValue = Regex.Replace(strValue, "[ä]", "ae") '(Umlaute...)
        strValue = Regex.Replace(strValue, "[ü]", "ue")
        strValue = Regex.Replace(strValue, "[ö]", "oe")
        strValue = Regex.Replace(strValue, "[áà]", "a") '(Accénts...)
        strValue = Regex.Replace(strValue, "[úù]", "u")
        strValue = Regex.Replace(strValue, "[óò]", "o")
        strValue = Regex.Replace(strValue, "[éè]", "e")
        strValue = Regex.Replace(strValue, "[íì]", "i")

        strValue = Regex.Replace(strValue, "[ß]", "ss")
        strValue = Regex.Replace(strValue, "[\[\]\(\)\{\}\|\?\+\*\^\$\\]", "") '(RegEx-Metazeichen)
        strValue = Regex.Replace(strValue, "[,:;'""@#~^<>°!§%&/=]", "")

        If bolExcludeDot = False Then
            strValue = Regex.Replace(strValue, "[.]", "")
        End If

        strValue = Regex.Replace(strValue, "(\-)+", "-") '(mehrfache Striche zu einem zusammenfassen)

        Return strValue.ToLower()

    End Function

    <Extension()>
    Public Function ReadableLength(fi As FileInfo) As String

        Dim arrSizeDef As String() = {"Byte", "KB", "MB", "GB"}
        Dim intLen As Double = fi.Length
        Dim intOrder As Integer = 0

        While intLen >= 1024 AndAlso intOrder + 1 < arrSizeDef.Length
            intOrder += 1
            intLen = intLen / 1024
        End While

        ' Adjust the format string to your preferences. For example "{0:0.#}{1}" would 
        ' show a single decimal place, and no space.
        Return [String].Format("{0:0.##} {1}", intLen, arrSizeDef(intOrder))

    End Function

    ''' <summary>
    ''' Fügt einen neuen Text an einen bestehenden an. Sofern der bestehende Text nicht leer ist, wird DAVOR der Separator angefügt.
    ''' </summary>
    ''' <param name="stb"></param>
    ''' <param name="strValue"></param>
    ''' <param name="strSeperator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function AppendWithSeperator(ByVal stb As StringBuilder,
                                        ByVal strValue As String,
                                        ByVal strSeperator As String) As StringBuilder

        If stb.Length > 0 Then
            stb.Append(strSeperator).Append(strValue)
        Else
            stb.Append(strValue)
        End If

        Return stb

    End Function

    ''' <summary>
    ''' http://www.codeproject.com/Articles/191424/Resizing-an-Image-On-The-Fly-using-NET
    ''' </summary>
    ''' <param name="image"></param>
    ''' <param name="size"></param>
    ''' <param name="preserveAspectRatio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ResizeImage(ByVal image As Image,
                                ByVal size As Size,
                                Optional ByVal preserveAspectRatio As Boolean = True) As Image

        Dim newWidth As Integer
        Dim newHeight As Integer

        If preserveAspectRatio Then
            Dim originalWidth As Integer = image.Width
            Dim originalHeight As Integer = image.Height
            Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
            Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
            Dim percent As Single = If(percentHeight < percentWidth,
                    percentHeight, percentWidth)
            newWidth = CInt(originalWidth * percent)
            newHeight = CInt(originalHeight * percent)
        Else
            newWidth = size.Width
            newHeight = size.Height
        End If

        Dim newImage As Image = New Bitmap(newWidth, newHeight)

        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using

        Return newImage

    End Function

    <Extension()>
    Public Function nz(Of T)(ByVal Value As T, ByVal Replacement As T) As T

        Try
            If Value Is Nothing OrElse IsDBNull(Value) Then
                Return Replacement
            Else
                Return Value
            End If
        Catch e As Exception
            Return Replacement
        End Try

    End Function

    <Extension()>
    Public Function BreakWordsHtml(strValue As String, Optional intLimit As Integer = 10) As String

        Dim stb As New StringBuilder(strValue)

        If strValue.Length > intLimit Then
            stb.Replace(" ", "<br>")
            stb.Replace("-", "-<br>")
        End If

        Return stb.ToString

    End Function

    <Extension()>
    Public Function ChangeColorBrightness(col As Color,
                                          correctionFactor As Single) As Color

        'http://stackoverflow.com/questions/801406/c-create-a-lighter-darker-color-based-on-a-system-color

        Dim red As Single = CSng(col.R)
        Dim green As Single = CSng(col.G)
        Dim blue As Single = CSng(col.B)

        If correctionFactor < 0 Then
            correctionFactor = 1 + correctionFactor
            red *= correctionFactor
            green *= correctionFactor
            blue *= correctionFactor
        Else
            red = (255 - red) * correctionFactor + red
            green = (255 - green) * correctionFactor + green
            blue = (255 - blue) * correctionFactor + blue
        End If

        Return Color.FromArgb(col.A,
                              CInt(Math.Truncate(red)),
                              CInt(Math.Truncate(green)),
                              CInt(Math.Truncate(blue)))
    End Function

End Module
