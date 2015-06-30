Public Class Test
    Private charIn As Integer() = New Integer() {1, 1, 1, 0, 0, 1, 1, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 1, 0, 0, _
                                             0, 1, 0, 1, 0, 0, 0, _
                                             0, 1, 1, 0, 0, 0, 0, _
                                             0, 1, 0, 1, 0, 0, 0, _
                                             0, 1, 0, 0, 1, 0, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             1, 1, 1, 0, 0, 1, 1}

    Private Sub Buttons_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.Controls(sender.name).BackColor = Color.Black Then
            Me.Controls(sender.name).BackColor = SystemColors.Control
            Me.Controls(sender.name).UseVisualStyleBackColor = True
        ElseIf (Not sender.name = "Button64") Then
            Me.Controls(sender.name).BackColor = Color.Black
        End If
    End Sub

    Private Sub Test_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim btn As Button = Nothing
        For Each btnctrl In Me.Controls
            If TypeOf btnctrl Is Button Then
                btn = DirectCast(btnctrl, Button)
                AddHandler btn.Click, AddressOf Me.Buttons_Click
            End If
        Next
    End Sub

    Private Sub Button64_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button64.Click
        Dim strNum As String
        Dim intNum As Integer
        Dim cr As CharRecon

        ' This loop is for filling the array charIn
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is Button Then
                strNum = (ctrl.name).ToString().Substring(6, _
                            (ctrl.name).ToString().Length - 6)
                intNum = CInt(strNum)
                If Not intNum = 64 Then
                    If ctrl.backcolor = Color.Black Then
                        charIn(intNum - 1) = 1
                    Else
                        charIn(intNum - 1) = 0
                    End If
                End If
            End If
        Next

        ' ANN things
        cr = New CharRecon()
        cr.CharNew = charIn
        cr.TrainNetwork()

        MsgBox(cr.getCharac())
    End Sub
End Class
