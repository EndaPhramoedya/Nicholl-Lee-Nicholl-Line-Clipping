Public Class Form1
    Dim x, y As Integer
    Dim ListDot As DotList
    Dim ListLine As LineList
    Public bmp As Drawing.Bitmap
    Dim bmp2 As Drawing.Bitmap
    Public graph As Graphics
    Dim graph2 As Graphics
    Dim ismousedown, isclipping As Boolean
    Public mRect As Rectangle
    Public dotorder As Integer = 1
    Public lineorder As Integer = 1

    Dim FILE_PATH As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    Dim FILE_NAME As String = IO.Path.Combine(FILE_PATH, "nln.txt")
    Dim fileReader As String

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        bmp = New Drawing.Bitmap(PictureBox1.Width, PictureBox1.Height)
        bmp2 = New Drawing.Bitmap(PictureBox1.Width, PictureBox1.Height)
        graph = Graphics.FromImage(bmp)
        graph.Clear(Color.White)
        PictureBox1.Image = bmp

        ListDot = New DotList
        ListDot.firstdot = Nothing
        ListLine = New LineList
        ListLine.firstline = Nothing
    End Sub

    'If mouse is clicked
    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseDown
        ismousedown = True
        x = e.X
        y = e.Y
    End Sub

    'Allow to retract the rectangle with mouse movement
    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseMove
        If ismousedown = True Then
            bmp2 = bmp.Clone
            graph2 = Graphics.FromImage(bmp2)

            If isclipping Then
                mRect.X = x
                mRect.Y = y
                If e.X < x Then
                    mRect.Width = mRect.X - e.X
                    mRect.X = e.X
                Else
                    mRect.Width = e.X - mRect.X
                End If
                If e.Y < y Then
                    mRect.Height = mRect.Y - e.Y
                    mRect.Y = e.Y
                Else
                    mRect.Height = e.Y - mRect.Y
                    graph2.DrawRectangle(New Pen(Color.Black, 1), mRect)
                End If
            Else
                graph2.DrawLine(Pens.Black, x, y, e.X, e.Y)
            End If
            PictureBox1.Image = bmp2
        End If
    End Sub

    'Create rectangle when mouse is up
    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseUp
        Dim x2, y2 As Integer
        x2 = e.X
        y2 = e.Y

        If isclipping Then
            mRect.X = x
            mRect.Y = y
            If e.X < x Then
                mRect.Width = mRect.X - e.X
                mRect.X = e.X
            Else
                mRect.Width = e.X - mRect.X
            End If
            If e.Y < y Then
                mRect.Height = mRect.Y - e.Y
                mRect.Y = e.Y
            Else
                mRect.Height = e.Y - mRect.Y
            End If
            refreshscreen()
            graph.DrawRectangle(New Pen(Color.Black, 1), mRect)
        Else
            If x = x2 And y = y2 Then
                bmp.SetPixel(x, y, Color.Black)
                Dim isDot As New Dot
                isDot.id = dotorder
                isDot.p1 = x
                isDot.p2 = y
                isDot.nxt = Nothing
                ListDot.insert(isDot)
                ListBox1.Items.Add("Dot " + isDot.id.ToString)
                dotorder += 1
            Else
                graph.DrawLine(Pens.Black, x, y, e.X, e.Y)
                Dim isLine As New Line
                isLine.id = lineorder
                isLine.x = x
                isLine.a = x2
                isLine.y = y
                isLine.b = y2
                isLine.nxt = Nothing
                ListLine.insert(isLine)
                ListBox1.Items.Add("Line " + isLine.id.ToString)
                lineorder += 1
            End If
        End If
        ListLine.clip(mRect)
        ListDot.clip(mRect)
        PictureBox1.Image = bmp
        ismousedown = False
    End Sub

    'Allow to traverse the coordinates of the picturebox using mouse movement
    Private Sub MouseMovement(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        lblx.Text = CStr(e.X)
        lbly.Text = CStr(e.Y)
    End Sub

    'Clear anything on the screen
    Private Sub clear_btn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles clear_btn.Click
        ListBox1.Items.Clear()
        ListLine.firstline = Nothing
        ListDot.firstdot = Nothing
        mRect = Nothing
        isclipping = Nothing
        dotorder = 1
        lineorder = 1
        graph.Clear(Color.White)
        graph2.Clear(Color.White)
        PictureBox1.Image = bmp
    End Sub

    'Button for clearing only the window from the screen
    Private Sub windowClr_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles windowClr_btn.Click
        refreshscreen()
    End Sub

    'Button for clipping rectangle
    Private Sub clip_btn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles clip_btn.Click
        If isclipping = True Then
            isclipping = False
            clip_btn.Text = "CLIP MODE: OFF"
        Else
            isclipping = True
            clip_btn.Text = "CLIP MODE: ON"
            refreshscreen()
        End If
    End Sub

    'Button for deleting selected line and dot from listbox
    Private Sub delete_btn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles delete_btn.Click
        Dim deleteitems2 As String = ListBox1.SelectedItem
        Dim items2() As String = deleteitems2.Split(" "c)
        If items2(0) = "Dot" Then
            ListDot.delete(items2(1))
            ListBox1.Items.Remove(ListBox1.SelectedItem)
            refreshscreen()
        ElseIf items2(0) = "Line" Then
            ListLine.delete(items2(1))
            ListBox1.Items.Remove(ListBox1.SelectedItem)
            refreshscreen()
        End If
    End Sub

    Private Sub refreshscreen()
        graph.Clear(Color.White)
        ListDot.draw()
        ListLine.draw()
        PictureBox1.Image = bmp
    End Sub

    'Button for saving the screen with the content inside 'BELOM BISA SAVE AMA LOAD
    Private Sub save_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles save_btn.Click
        'Dim save As New SaveFileDialog
        'save.Filter = "JPG files (*.jpg)|*.jpg|Bitmaps (*.bmp)|*.bmp|Png(*.png)|*.png"
        'If (save.ShowDialog = DialogResult.OK) Then
        '    bmp.Save(save.FileName)
        'End If
        System.IO.File.WriteAllText(FILE_NAME, "")
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME)
        Dim dot As New Dot
        Dim line As New Line

        If (dotorder >= 0) Then
            objWriter.Write("dot ")
            objWriter.Write(Environment.NewLine)
            For i As Integer = 0 To dotorder
                objWriter.Write(dot.p1.ToString + " " + dot.p2.ToString)
                objWriter.Write(" ")
            Next
        End If
        If (lineorder >= 0) Then
            objWriter.Write("line ")
            objWriter.Write(Environment.NewLine)
            For i As Integer = 0 To lineorder
                objWriter.Write(line.x.ToString + " " + line.y.ToString + " " + line.a.ToString + " " + line.b.ToString)
                objWriter.Write(" ")
            Next
        End If
        objWriter.Close()
    End Sub

    'Button for loading saved screen file
    Private Sub load_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles load_btn.Click
        ListBox1.Items.Clear()
        ListLine.firstline = Nothing
        ListDot.firstdot = Nothing
        dotorder = 1
        lineorder = 1
        PictureBox1.Image = Nothing
        fileReader = My.Computer.FileSystem.ReadAllText(FILE_NAME)
        Dim file As String() = fileReader.Split(" ")
        Dim loadpoint As Boolean = False
        Dim loadline As Boolean = False
        Dim pointcount As Integer = 0
        Dim linecount As Integer = 0
        Dim dot As New Dot
        Dim line As New Line
        For i As Integer = 0 To file.Length - 1
            'MsgBox(file(i))
            If file(i) = "dot" Then
                loadpoint = True
                loadline = False
            ElseIf file(i) = "line" Then
                loadpoint = False
                loadline = True
            Else
                If (loadpoint = True) Then
                    pointcount = pointcount + 1
                    If pointcount = 2 Then
                        dotorder = dotorder + 1
                        dot.p1 = file(i - 1)
                        dot.p2 = file(i)
                        ListBox1.Items.Add("Dot " + dot.id.ToString)
                        pointcount = 0
                    End If
                Else
                    linecount = linecount + 1
                    If linecount = 4 Then
                        lineorder = lineorder + 1
                        line.x = file(i - 3)
                        line.y = file(i - 2)
                        line.a = file(i - 1)
                        line.b = file(i)
                        ListBox1.Items.Add("Line " + line.id.ToString)
                        linecount = 0
                    End If
                End If
            End If
        Next
        bmp.SetPixel(dot.p1, dot.p2, Color.Black)
        graph2.DrawLine(Pens.Black, line.x, line.y, line.a, line.b)
    End Sub
End Class


Class Dot
    Public id As Integer
    Public p1, p2 As Integer
    Public nxt As Dot
    Sub draw()
        Form1.bmp.SetPixel(p1, p2, Color.Black)
    End Sub
End Class

Class Dotlist
    Public firstdot As Dot
    Sub draw()
        Dim temp As Dot
        temp = firstdot
        While Not IsNothing(temp)
            temp.draw()
            temp = temp.nxt

        End While
    End Sub

    Sub insert(ByVal a As Dot)
        If IsNothing(firstdot) Then
            firstdot = a
        Else
            Dim temp As Dot
            temp = firstdot
            While Not IsNothing(temp.nxt)
                temp = temp.nxt

            End While
            temp.nxt = a
        End If
    End Sub

    Sub delete(ByVal id As Integer)
        If firstdot.id = id Then
            firstdot = firstdot.nxt
        Else
            Dim temp1 As Dot
            Dim temp2 As Dot

            temp1 = firstdot.nxt
            temp2 = firstdot

            While Not temp1.id = id
                temp2 = temp1
                temp1 = temp1.nxt

            End While
            temp2.nxt = temp1.nxt

        End If
    End Sub

    Function cliparea(ByVal xmin As Integer, ByVal xmax As Integer, ByVal ymin As Integer, ByVal ymax As Integer, ByVal x As Integer, ByVal y As Integer)
        Dim area As Integer = 0

        If (x <= xmin Or x >= xmax) Then
            area = 1
        End If

        If (y <= ymin Or y >= ymax) Then
            area = 1
        End If

        Return area
    End Function

    Sub clipdot(ByVal xmin As Integer, ByVal xmax As Integer, ByVal ymin As Integer, ByVal ymax As Integer, ByVal temp As Dot)
        Dim x As Integer = temp.p1
        Dim y As Integer = temp.p2

        Dim area As Integer = cliparea(xmin, xmax, ymin, ymax, x, y)
        Dim accepted As Boolean = False
        While True
            If area = 0 Then
                accepted = True
                Exit While
            Else
                Exit While
            End If
        End While
        If (accepted) Then
            Form1.bmp.SetPixel(x, y, Color.Red)
            Form1.PictureBox1.Image = Form1.bmp
        End If
    End Sub

    Sub clip(ByVal a As Rectangle)
        Dim xmin As Integer = a.X
        Dim xmax As Integer = a.X + a.Width
        Dim ymin As Integer = a.Y
        Dim ymax As Integer = a.Y + a.Height
        Dim temp As Dot = firstdot
        While Not IsNothing(temp)
            clipdot(xmin, xmax, ymin, ymax, temp)
            temp = temp.nxt
        End While
    End Sub
End Class

Class Line
    Public id As Integer
    Public x, y, a, b As Integer
    Public nxt As line
    Sub draw()
        Form1.graph.DrawLine(Pens.Black, x, y, a, b)
    End Sub
End Class

Class LineList
    Public firstline As Line
    Sub draw()
        Dim temp As Line
        temp = firstline
        While Not IsNothing(temp)
            temp.draw()
            temp = temp.nxt
        End While
    End Sub

    Sub insert(ByVal l As Line)
        If IsNothing(firstline) Then
            firstline = l
        Else
            Dim temp As Line
            temp = firstline
            While Not IsNothing(temp.nxt)
                temp = temp.nxt

            End While
            temp.nxt = l
        End If
    End Sub

    Sub delete(ByVal id As Integer)
        If firstline.id = id Then
            firstline = firstline.nxt
        Else
            Dim temp1 As Line
            Dim temp2 As Line

            temp1 = firstline.nxt
            temp2 = firstline

            While Not temp1.id = id
                temp2 = temp1
                temp1 = temp1.nxt

            End While
            temp2.nxt = temp1.nxt
        End If
    End Sub

    Sub clip(ByVal a As Rectangle)
        Dim xmin As Integer = a.X
        Dim ymin As Integer = a.Y
        Dim xmax As Integer = a.X + a.Width
        Dim ymax As Integer = a.Y + a.Height
        Dim temp As Line = firstline
        While Not IsNothing(temp)
            clipcode(xmin, ymin, xmax, ymax, temp)
            temp = temp.nxt
        End While
    End Sub

    Sub clipcode(ByVal xmin As Integer, ByVal ymin As Integer, ByVal xmax As Integer, ByVal ymax As Integer, ByVal temp As Line)
        Dim x1 As Integer = temp.x
        Dim y1 As Integer = temp.y
        Dim x2 As Integer = temp.a
        Dim y2 As Integer = temp.b
        Dim area As Integer = cliparea(x1, y1, xmin, ymin, xmax, ymax)
        Dim nln As New NLN_ClipCode
        Select Case area
            Case 1
                nln.Inside(x1, x2, y1, y2, xmin, ymin, xmax, ymax)
                Exit Sub
            Case 2
                nln.Edge(x1, x2, y1, y2, xmin, ymin, xmax, ymax)
                Exit Sub
            Case 3
                nln.Corner(x1, x2, y1, y2, xmin, ymin, xmax, ymax)
                Exit Sub
            Case Else
        End Select
    End Sub

    Function cliparea(ByVal x As Integer, ByVal y As Integer, ByVal xmin As Integer, ByVal ymin As Integer, ByVal xmax As Integer, ByVal ymax As Integer)

        If (x >= xmin And x <= xmax And y >= ymin And y <= ymax) Then 'p1 inside
            Return 1
        ElseIf ((x < xmin And y >= ymin And y <= ymax) Or (x > xmax And y >= ymin And y <= ymax) Or (y > ymax And x >= xmin And x <= xmax) Or (y < ymin And x >= xmin And x <= xmax)) Then 'p1 at top, right, left and bottom
            Return 2
        ElseIf ((x <= xmin And y <= ymin) Or (x >= xmax And y <= ymin) Or (x <= xmin And y >= ymax) Or (x >= xmax And y >= ymax)) Then 'p1 at top-right, bottom-right, bottom-left, and top-left
            Return 3
        Else
            Return 0
        End If

    End Function
End Class

Class NLN_ClipCode

    Public Sub Inside(ByVal x1 As Integer, ByVal x2 As Integer, ByVal y1 As Integer, ByVal y2 As Integer, ByVal xmin As Integer, ByVal ymin As Integer, ByVal xmax As Integer, ByVal ymax As Integer)
        Dim nx1, ny1, nx2, ny2 As Integer
        Dim m, m1, m2, m3, m4 As Single
        Dim accepted As Boolean = True

        m = (y2 - y1) / (x2 - x1) 'slope for p1 and p2
        m1 = (ymin - y1) / (xmin - x1) 'slope for p1 through bottom-left
        m2 = (ymin - y1) / (xmax - x1) 'slope for p1 through bottom-right
        m3 = (ymax - y1) / (xmax - x1) 'slope for p1 through top-right
        m4 = (ymax - y1) / (xmin - x1) 'slope for p1 through top-left
        nx1 = x1
        ny1 = y1

        If (x1 < x2) Then
            If (m < m2) Then 'p2 on bottom
                If (y2 < ymin) Then
                    nx2 = x1 + (ymin - y1) / m
                    ny2 = ymin
                Else
                    nx2 = x2
                    ny2 = y2
                End If
            ElseIf (m > m2 And m < m3) Then 'p2 on right
                If (x2 > xmax) Then
                    nx2 = xmax
                    ny2 = y1 + (xmax - x1) * m
                Else
                    nx2 = x2
                    ny2 = y2
                End If
            ElseIf (m > m3) Then 'p2 on top
                If (y2 > ymax) Then
                    nx2 = x1 + (ymax - y1) / m
                    ny2 = ymax
                Else
                    nx2 = x2
                    ny2 = y2
                End If
            End If

        ElseIf (x1 > x2) Then
            If (m < m4) Then
                If (y2 > ymax) Then
                    nx2 = x1 + (ymax - y1) / m
                    ny2 = ymax
                Else
                    nx2 = x2
                    ny2 = y2
                End If
            ElseIf (m > m1) Then
                If (y2 < ymin) Then
                    nx2 = x1 + (ymin - y1) / m
                    ny2 = ymin
                Else
                    nx2 = x2
                    ny2 = y2
                End If
            ElseIf (m <= m1 And m >= m4) Then
                If (x2 < xmin) Then
                    nx2 = xmin
                    ny2 = y1 + (xmin - x1) * m
                Else
                    nx2 = x2
                    ny2 = y2
                End If

            End If
        ElseIf (x1 = x2) Then
            If (y2 < ymin) Then
                nx2 = x2
                ny2 = ymin
            ElseIf (y2 > ymax) Then
                nx2 = x2
                ny2 = ymax
            Else
                nx2 = x2
                ny2 = y2
            End If
        Else

            accepted = False

        End If

        If (accepted) Then
            Form1.graph.DrawLine(Pens.Red, nx1, ny1, nx2, ny2)
            Form1.PictureBox1.Image = Form1.bmp
        End If

    End Sub

    Public Sub Edge(ByVal x1 As Integer, ByVal x2 As Integer, ByVal y1 As Integer, ByVal y2 As Integer, ByVal xmin As Integer, ByVal ymin As Integer, ByVal xmax As Integer, ByVal ymax As Integer)
        Dim nx1, ny1, nx2, ny2 As Integer
        Dim m, m1, m2, m3, m4 As Single
        Dim accepted As Boolean = True
        Dim temp As Integer

        m = (y2 - y1) / (x2 - x1)
        m1 = (ymin - y1) / (xmin - x1)
        m2 = (ymin - y1) / (xmax - x1)
        m3 = (ymax - y1) / (xmax - x1)
        m4 = (ymax - y1) / (xmin - x1)

        nx1 = x1
        ny1 = y1

        If (x1 < xmin) Then 'Point p1 start from left
            If (m > m1 And m < m2) Then 'Point p2 in the left-Bottom region
                If (y2 > ymin) Then 'Point p2 is inside the clip window
                    nx1 = xmin
                    ny1 = y1 + m * (xmin - x1)
                    nx2 = x2
                    ny2 = y2
                Else 'Point p2 is outside the clip window
                    nx1 = xmin
                    ny1 = y1 + m * (xmin - x1)
                    ny2 = ymin
                    nx2 = x1 + (ymin - y1) / m

                End If

            ElseIf (m > m2 And m < m3) Then 'Point p2 in left-right region
                If (x2 < xmax) Then 'Point p2 is inside the clip window
                    nx1 = xmin
                    ny1 = y1 + m * (xmin - x1)
                    nx2 = x2
                    ny2 = y2
                Else 'Point p2 is outside the clip window
                    nx1 = xmin
                    ny1 = y1 + m * (xmin - x1)
                    nx2 = xmax
                    ny2 = y1 + m * (xmax - x1)
                End If

            ElseIf (m > m3 And m < m4) Then 'Point p2 in the Left-Top region
                If (y2 < ymax) Then 'Point p2 is inside the clip window
                    nx1 = xmin
                    ny1 = y1 + m * (xmin - x1)
                    nx2 = x2
                    ny2 = y2
                Else 'Point p2 is outside the clip window
                    nx1 = xmin
                    ny1 = y1 + m * (xmin - x1)
                    ny2 = ymax
                    nx2 = x1 + (ymax - y1) / m
                End If
            Else
                accepted = False
            End If
            '--------------------------------------------------------------------------------------------------------------
        ElseIf (x1 > xmax) Then 'Point p1 start from right
            x1 = -(x1)
            If (m > m1 And m < m2) Then 'Point p2 in the right-Bottom region
                If (y2 > ymin) Then 'Point p2 is inside the clip window
                    x1 = -(x1) 'retransform
                    nx1 = xmax
                    ny1 = y1 + m * (xmax - x1)
                    nx2 = x2
                    ny2 = y2
                Else 'Point p2 is outside the clip window
                    x1 = -(x1)
                    nx1 = xmax
                    ny1 = y1 + m * (xmax - x1)
                    ny2 = ymin
                    nx2 = x1 + (ymin - y1) / m

                End If

            ElseIf (m < m1 And m > m4) Then 'Point p2 in right-left region
                If (x2 > xmin) Then 'Point p2 is inside the clip window
                    x1 = -(x1)
                    nx1 = xmax
                    ny1 = y1 + m * (xmax - x1)
                    nx2 = x2
                    ny2 = y2
                Else 'Point p2 is outside the clip window
                    x1 = -(x1)
                    nx1 = xmax
                    ny1 = y1 + m * (xmax - x1)
                    nx2 = xmin
                    ny2 = y1 + (xmin - x1) * m
                End If
            ElseIf (m > m3 And m < m4) Then 'Point p2 in the Right-Top region
                If (y2 < ymax) Then 'Point p2 is inside the clip window
                    x1 = -(x1)
                    nx1 = xmax
                    ny1 = y1 + m * (xmax - x1)
                    nx2 = x2
                    ny2 = y2
                Else 'Point p2 is outside the clip window
                    x1 = -(x1)
                    nx1 = xmax
                    ny1 = y1 + m * (xmax - x1)
                    ny2 = ymax
                    nx2 = x1 + (ymax - y1) / m
                End If
            Else
                accepted = False
            End If
            '--------------------------------------------------------------------------
            'ElseIf (y1 < ymin) Then 'Point p1 start from bottom 'NGGAK TAU LAGI CUK
            '    temp = x1
            '    x1 = y1
            '    y1 = temp

            '    temp = x2
            '    x2 = y2
            '    y2 = temp

            '    If (m > m2 And m < m3) Then 'Point p2 in the bottom-right region
            '        If (x2 < xmax) Then 'Point p2 is inside the clip window
            '            temp = x1
            '            x1 = y1
            '            y1 = temp
            '            temp = x2
            '            x2 = y2
            '            y2 = temp

            '            nx1 = x1 + (ymin - y1) / m
            '            ny1 = ymin
            '            nx2 = x2
            '            ny2 = y2

            '        Else 'Point p2 is outside the clip window
            '            temp = x1
            '            x1 = y1
            '            y1 = temp
            '            temp = x2
            '            x2 = y2
            '            y2 = temp

            '            nx1 = x1 + (ymin - y1) / m
            '            ny1 = ymin
            '            nx2 = xmax
            '            ny2 = y1 + m * (xmax - x1)
            '        End If

            '    ElseIf (m > m3 And m < m4) Then 'Point p2 in the bottom-top region
            '        If (y2 < ymax) Then 'Point p2 is inside the clip window
            '            temp = x1
            '            x1 = y1
            '            y1 = temp
            '            temp = x2
            '            x2 = y2
            '            y2 = temp

            '            nx1 = x1 + (ymin - y1) / m
            '            ny1 = ymin
            '            nx2 = x2
            '            ny2 = y2
            '        Else 'Point p2 is outside the clip window
            '            temp = x1
            '            x1 = y1
            '            y1 = temp
            '            temp = x2
            '            x2 = y2
            '            y2 = temp

            '            nx1 = x1 + (ymin - y1) / m
            '            ny1 = ymin
            '            nx2 = x1 + (ymax - y1) / m
            '            ny2 = ymax
            '        End If

            '    ElseIf (m > m4 And m < m2) Then 'Point p2 in bottom-left region
            '        If (x2 > xmin) Then 'Point p2 is inside the clip window
            '            temp = x1
            '            x1 = y1
            '            y1 = temp
            '            temp = x2
            '            x2 = y2
            '            y2 = temp

            '            nx1 = x1 + (ymin - y1) / m
            '            ny1 = ymin
            '            nx2 = x2
            '            ny2 = y2
            '        Else 'Point p2 is outside the clip window
            '            temp = x1
            '            x1 = y1
            '            y1 = temp
            '            temp = x2
            '            x2 = y2
            '            y2 = temp

            '            nx1 = x1 + (ymin - y1) / m
            '            ny1 = ymin
            '            nx2 = xmin
            '            ny2 = y1 + (xmin - x1) / m
            '        End If

            '    Else
            '        accepted = False
            '    End If
            '-------------------------------------------------------------------------
            'ElseIf (y1 > ymax) Then 'Point P1 starts from top 'NGGAK TAU LAGI CUK
            '    temp = x1
            '    x1 = -(y1)
            '    y1 = temp

            '    temp = x2
            '    x2 = -(y2)
            '    y2 = temp

            '    ymax = -(ymax)
            '    ymin = -(ymin)

            '    If (m > m3 And m < m2) Then 'point p2 in the left-right region
            '        If (x2 > xmin) Then 'point p2 is inside the clip window
            '            nx1 = ymax
            '            ny1 = y1 + m * (ymax - x1)
            '            nx2 = x2
            '            ny2 = y2
            '        Else 'point p2 is outside the clip window
            '            nx1 = ymax
            '            ny1 = y1 + m * (ymax - x1)
            '            nx2 = ymin
            '            ny2 = y1 + m * (ymin - x1)
            '        End If

            '    ElseIf (m > m2 And m < m1) Then 'point p2 in top-bottom region
            '        If (x2 < xmax) Then 'point p2 is inside the clip window
            '            nx1 = xmax
            '            ny1 = y1 + m * (xmax - x1)
            '            nx2 = x2
            '            ny2 = y2

            '            nx2 = x1 + (ymin - y1) / m
            '            ny2 = ymin
            '        Else 'point p2 is outside the clip window
            '            nx1 = xmax
            '            ny1 = y1 + m * (xmax - x1)
            '            nx2 = xmin
            '            ny2 = y1 + (xmin - x1) * m
            '        End If

            '    ElseIf (m > m1 And m < m4) Then 'point p2 in the right-left region
            '        If (y2 < ymax) Then 'point p2 is inside the clip window
            '            nx1 = ymin
            '            ny1 = y1 + m * (ymin - x1)
            '            nx2 = x2
            '            ny2 = y2
            '        Else 'point p2 is outside the clip window
            '            nx1 = ymin
            '            ny1 = y1 + m * (ymin - x1)
            '            nx2 = ymax
            '            ny2 = y1 + (ymax - x1) * m
            '        End If
            '    Else
            '        accepted = False
            '    End If

        End If
        If (accepted) Then
            Form1.graph.DrawLine(Pens.Red, nx1, ny1, nx2, ny2)
            Form1.PictureBox1.Image = Form1.bmp
        End If
    End Sub

    Public Sub Corner(ByVal x1 As Integer, ByVal x2 As Integer, ByVal y1 As Integer, ByVal y2 As Integer, ByVal xmin As Integer, ByVal ymin As Integer, ByVal xmax As Integer, ByVal ymax As Integer)
        Dim nx1, ny1, nx2, ny2 As Integer
        Dim m, m1, m2, m3, m4, tm1, tm2 As Single
        Dim flag As Boolean = True
        Dim accepted As Boolean = True

        'tm1 = ((ymin - y1)) / (xmin - x1)
        'tm2 = (ymax - ymin) / (xmax - xmin) 'diagonal slope

        m = (y2 - y1) / (x2 - x1)
        m1 = (ymin - y1) / (xmin - x1)
        m2 = (ymin - y1) / (xmax - x1)
        m3 = (ymax - y1) / (xmax - x1)
        m4 = (ymax - y1) / (xmin - x1)

        nx1 = x1
        ny1 = y1

        If (x1 <= xmin And y1 <= ymin) Then 'point P1 starts form bottom-left corner
            If (m < m1 And m > m2) Then
                'Point p2 is outside the clip window
                If (x2 > xmax And y2 > ymin) Then
                    ny1 = ymin
                    nx1 = x1 + (ymin - y1) / m
                    nx2 = xmax
                    ny2 = y1 + m * (xmax - x1)
                    'Point p2 is inside the clip window
                ElseIf (y2 > ymin And x2 < xmax) Then
                    ny1 = ymin
                    nx1 = x1 + (ymin - y1) / m
                    ny2 = y2
                    nx2 = x2
                End If

                ' Point p2 is in Left-Bottom region
            ElseIf (m > m3 And m < m4) Then
                If (y2 >= ymax) Then 'Point p2 is outside the clip window
                    nx1 = xmin
                    ny1 = y1 + m * (xmin - x1)
                    nx2 = x1 + (ymax - y1) / m
                    ny2 = ymax
                ElseIf (y2 >= xmin) Then 'Point p2 is inside the clip window
                    nx1 = xmin
                    ny1 = y1 + m * (xmin - x1)
                    ny2 = y2
                    nx2 = x2
                End If
            ElseIf ((m > m2 And m < m3) Or (m < m2 And m > m3)) Then

                'Point p2 is in Top-Bottom region (case1)
                If (m1 > m3) Then
                    ' Point p2 is outside the clip window
                    If (y2 >= ymax) Then
                        ny1 = ymin
                        nx1 = x1 + (ymin - y1) / m
                        nx2 = ymax
                        ny2 = x1 + (ymax - y1) / m
                        ' Point p2 is inside the clip window
                    Else
                        ny1 = ymin
                        nx1 = x1 + (ymin - y1) / m
                        nx2 = x2
                        ny2 = y2
                    End If

                    ' Point p2 is in Left-Right region (case2)
                Else
                    ' Point p2 is outside the clip window
                    If (x2 >= xmax) Then

                        nx1 = xmin
                        ny1 = y1 + m * (xmin - x1)
                        nx2 = xmax
                        ny2 = y1 + m * (xmax - x1)

                        ' Point p2 is inside the clip window
                    ElseIf (x2 <= xmax) Then

                        nx1 = xmin
                        ny1 = y1 + m * (xmin - x1)
                        nx2 = x2
                        ny2 = y2
                    End If
                End If

            Else
                accepted = False
            End If

        ElseIf (x1 >= xmax And y1 <= ymin) Then
            x1 = -(x1)
            If (m < m1 And m > m2) Then
                'Point p2 is outside the clip window
                If (y2 > ymin And x2 < xmin) Then
                    x1 = -(x1)
                    ny1 = ymin
                    nx1 = x1 + (ymin - y1) / m
                    nx2 = xmin
                    ny2 = y1 + m * (xmin - x1)

                    'Point p2 is inside the clip window
                ElseIf (x2 > xmin And y2 > ymin) Then
                    x1 = -(x1)
                    ny1 = ymin
                    nx1 = x1 + (ymin - y1) / m
                    ny2 = y2
                    nx2 = x2
                End If

                ' Point p2 is in Left-Bottom region
            ElseIf (m > m3 And m < m4) Then
                If (y2 >= ymax) Then 'Point p2 is outside the clip window
                    x1 = -(x1)
                    nx1 = xmax
                    ny1 = y1 + m * (xmax - x1)
                    nx2 = x1 + (ymax - y1) / m
                    ny2 = ymax
                ElseIf (y2 >= xmin) Then 'Point p2 is inside the clip window
                    x1 = -(x1)
                    nx1 = xmax
                    ny1 = y1 + m * (xmax - x1)
                    ny2 = y2
                    nx2 = x2
                End If
            ElseIf ((m > m2 And m > m3) Or (m < m2 And m > m3)) Then

                'Point p2 is in Top-Bottom region (case1)
                If (m2 < m4) Then
                    ' Point p2 is outside the clip window
                    If (y2 >= ymax) Then
                        ny1 = ymin
                        nx1 = x1 + (ymin - y1) / m
                        nx2 = ymax
                        ny2 = x1 + (ymax - y1) / m
                        ' Point p2 is inside the clip window
                    Else
                        ny1 = ymin
                        nx1 = x1 + (ymin - y1) / m
                        nx2 = x2
                        ny2 = y2
                    End If

                    ' Point p2 is in Left-Right region (case2)
                Else
                    ' Point p2 is outside the clip window
                    If (x2 <= xmin) Then
                        x1 = -(x1)
                        nx1 = xmax
                        ny1 = y1 + m * (xmax - x1)
                        nx2 = xmin
                        ny2 = y1 + m * (xmin - x1)

                        ' Point p2 is inside the clip window
                    ElseIf (x2 >= xmin) Then
                        x1 = -(x1)
                        nx1 = xmax
                        ny1 = y1 + m * (xmax - x1)
                        nx2 = x2
                        ny2 = y2
                    End If
                End If
            Else
                accepted = False
            End If
        ElseIf (x1 <= xmin And y1 >= ymax) Then
            y1 = -(y1)
            If (m > m1 And m < m2) Then 'left-top region
                'Point p2 is outside the clip window
                If (x2 > xmin And y2 < ymin) Then
                    y1 = -(y1)
                    ny1 = y1 + m * (xmin - x1)
                    nx1 = xmin
                    nx2 = x1 + (ymin - y1) / m
                    ny2 = ymin
                    'Point p2 is inside the clip window
                ElseIf (y2 > ymin And x2 > xmin) Then
                    y1 = -(y1)
                    ny1 = y1 + m * (xmin - x1)
                    nx1 = xmin
                    ny2 = y2
                    nx2 = x2
                End If

                ' Point p2 is in Bottom-Right region
            ElseIf (m < m3 And m > m4) Then
                If (x2 >= xmax) Then 'Point p2 is outside the clip window
                    y1 = -(y1)
                    nx1 = x1 + (ymax - y1) / m
                    ny1 = ymax
                    nx2 = xmax
                    ny2 = y1 + m * (xmax - x1)
                ElseIf (x2 <= xmax) Then 'Point p2 is inside the clip window
                    y1 = -(y1)
                    nx1 = x1 + (ymax - y1) / m
                    ny1 = ymax
                    ny2 = y2
                    nx2 = x2
                End If

            ElseIf ((m > m2 And m < m3) Or (m < m2 And m > m3)) Then
                'Point p2 is in Bottom-Top region (case1)
                If (m2 > m4) Then
                    ' Point p2 is outside the clip window
                    If (y2 >= ymax) Then
                        y1 = -(y1) 'mark
                        ny1 = ymin
                        nx1 = x1 + (ymin - y1) / m
                        nx2 = ymax
                        ny2 = x1 + (ymax - y1) / m
                        ' Point p2 is inside the clip window
                    Else
                        y1 = -(y1) 'mark
                        ny1 = ymin
                        nx1 = x1 + (ymin - y1) / m
                        nx2 = x2
                        ny2 = y2
                    End If

                    ' Point p2 is in Left-Right region (case2)
                Else
                    ' Point p2 is outside the clip window
                    If (x2 >= xmax) Then
                        y1 = -(y1)
                        nx1 = xmin
                        ny1 = y1 + m * (xmin - x1)
                        nx2 = xmax
                        ny2 = y1 + m * (xmax - x1)

                        ' Point p2 is inside the clip window
                    ElseIf (x2 <= xmax) Then
                        y1 = -(y1)
                        nx1 = xmin
                        ny1 = y1 + m * (xmin - x1)
                        nx2 = x2
                        ny2 = y2
                    End If

                End If
            Else
                accepted = False
            End If

        ElseIf (x1 >= xmax And y1 >= ymax) Then
            y1 = -(y1)
            x1 = -(x1)
            If (m > m1 And m < m2) Then 'Right-Top region
                'Point p2 is outside the clip window
                If (x2 < xmax And y2 < ymin) Then
                    y1 = -(y1)
                    x1 = -(x1)
                    ny1 = y1 + m * (xmax - x1)
                    nx1 = xmax
                    nx2 = x1 + (ymin - y1) / m
                    ny2 = ymin
                    'Point p2 is inside the clip window
                ElseIf (y2 > ymin And x2 > xmin) Then
                    y1 = -(y1)
                    x1 = -(x1)
                    ny1 = y1 + m * (xmax - x1)
                    nx1 = xmax
                    ny2 = y2
                    nx2 = x2
                End If

                ' Point p2 is in Bottom-Left region
            ElseIf (m < m3 And m > m4) Then
                If (x2 <= xmin) Then 'Point p2 is outside the clip window
                    y1 = -(y1)
                    x1 = -(x1)
                    nx1 = x1 + (ymax - y1) / m
                    ny1 = ymax
                    nx2 = xmin
                    ny2 = y1 + m * (xmin - x1)
                ElseIf (x2 <= ymax) Then 'Point p2 is inside the clip window
                    y1 = -(y1)
                    x1 = -(x1)
                    nx1 = x1 + (ymax - y1) / m
                    ny1 = ymax
                    ny2 = y2
                    nx2 = x2
                End If
            ElseIf ((m > m2 And m < m3) Or (m < m2 And m > m3)) Then

                'Point p2 is in Bottom-Top region (case1)
                If (m2 < m4) Then
                    ' Point p2 is outside the clip window
                    If (y2 <= ymax) Then
                        y1 = -(y1)
                        x1 = -(x1)
                        ny1 = ymin
                        nx1 = x1 + (ymin - y1) / m
                        nx2 = ymax
                        ny2 = x1 + (ymax - y1) / m
                        ' Point p2 is inside the clip window
                    Else
                        y1 = -(y1) 'mark
                        x1 = -(x1)
                        ny1 = ymax
                        nx1 = x1 + (ymin - y1) / m
                        nx2 = x2
                        ny2 = y2
                    End If

                    ' Point p2 is in Right-Left region (case2)
                Else
                    ' Point p2 is outside the clip window
                    If (x2 <= xmin) Then
                        y1 = -(y1)
                        x1 = -(x1)
                        nx1 = xmax
                        ny1 = y1 + m * (xmax - x1)
                        nx2 = xmin
                        ny2 = y1 + m * (xmin - x1)

                        ' Point p2 is inside the clip window
                    ElseIf (x2 >= xmin) Then
                        y1 = -(y1)
                        x1 = -(x1)
                        nx1 = xmax
                        ny1 = y1 + m * (xmax - x1)
                        nx2 = x2
                        ny2 = y2
                    End If
                End If

            Else
                accepted = False
            End If
        End If
        If (accepted) Then
            Form1.graph.DrawLine(Pens.Red, nx1, ny1, nx2, ny2)
            Form1.PictureBox1.Image = Form1.bmp
        End If
    End Sub
End Class
