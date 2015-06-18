Public Class Form1
    Private Const NUMBER_OF_CLUSTERS As Integer = 7
    Private Const VEC_LEN As Integer = 63
    Private Const TRAINING_PATTERNS As Integer = 21

    Private LVQ As LVQ1class

    'If you look closely, you can see the letters made out of 1's
    Private A1 As Integer() = New Integer() {0, 0, 1, 1, 0, 0, 0, _
                                             0, 0, 0, 1, 0, 0, 0, _
                                             0, 0, 0, 1, 0, 0, 0, _
                                             0, 0, 1, 0, 1, 0, 0, _
                                             0, 0, 1, 0, 1, 0, 0, _
                                             0, 1, 1, 1, 1, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             1, 1, 1, 0, 1, 1, 1}

    Private A As Integer() = New Integer() {1, 1, 1, 0, 0, 1, 1, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 1, 0, 0, _
                                             0, 1, 0, 1, 0, 0, 0, _
                                             0, 1, 1, 0, 0, 0, 0, _
                                             0, 1, 0, 1, 0, 0, 0, _
                                             0, 1, 0, 0, 1, 0, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             1, 1, 1, 0, 0, 1, 1}

    Private B1 As Integer() = New Integer() {1, 1, 1, 1, 1, 1, 0, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 1, 1, 1, 1, 0, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             1, 1, 1, 1, 1, 1, 0}

    Private C1 As Integer() = New Integer() {0, 0, 1, 1, 1, 1, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 0, 1, 1, 1, 1, 0}

    Private D1 As Integer() = New Integer() {1, 1, 1, 1, 1, 0, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             1, 1, 1, 1, 1, 0, 0}

    Private E1 As Integer() = New Integer() {1, 1, 1, 1, 1, 1, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 0, _
                                             0, 1, 0, 1, 0, 0, 0, _
                                             0, 1, 1, 1, 0, 0, 0, _
                                             0, 1, 0, 1, 0, 0, 0, _
                                             0, 1, 0, 0, 0, 0, 0, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             1, 1, 1, 1, 1, 1, 1}

    Private J1 As Integer() = New Integer() {0, 0, 0, 1, 1, 1, 1, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 0, 1, 1, 1, 0, 0}

    Private K1 As Integer() = New Integer() {1, 1, 1, 0, 0, 1, 1, _
                                             0, 1, 0, 0, 1, 0, 0, _
                                             0, 1, 0, 1, 0, 0, 0, _
                                             0, 1, 1, 0, 0, 0, 0, _
                                             0, 1, 1, 0, 0, 0, 0, _
                                             0, 1, 0, 1, 0, 0, 0, _
                                             0, 1, 0, 0, 1, 0, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             1, 1, 1, 0, 0, 1, 1}

    Private A2 As Integer() = New Integer() {0, 0, 0, 1, 0, 0, 0, _
                                             0, 0, 0, 1, 0, 0, 0, _
                                             0, 0, 0, 1, 0, 0, 0, _
                                             0, 0, 1, 0, 1, 0, 0, _
                                             0, 0, 1, 0, 1, 0, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 1, 1, 1, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0}

    Private B2 As Integer() = New Integer() {1, 1, 1, 1, 1, 1, 0, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 1, 1, 1, 1, 1, 0, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 1, 1, 1, 1, 1, 0}

    Private C2 As Integer() = New Integer() {0, 0, 1, 1, 1, 0, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 0, 1, 1, 1, 0, 0}

    Private D2 As Integer() = New Integer() {1, 1, 1, 1, 1, 0, 0, _
                                             1, 0, 0, 0, 0, 1, 0, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 1, 0, _
                                             1, 1, 1, 1, 1, 0, 0}

    Private E2 As Integer() = New Integer() {1, 1, 1, 1, 1, 1, 1, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 1, 1, 1, 1, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 1, 1, 1, 1, 1, 1}

    Private J2 As Integer() = New Integer() {0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 0, 1, 1, 1, 0, 0}

    Private K2 As Integer() = New Integer() {1, 0, 0, 0, 0, 1, 0, _
                                             1, 0, 0, 0, 1, 0, 0, _
                                             1, 0, 0, 1, 0, 0, 0, _
                                             1, 0, 1, 0, 0, 0, 0, _
                                             1, 1, 0, 0, 0, 0, 0, _
                                             1, 0, 1, 0, 0, 0, 0, _
                                             1, 0, 0, 1, 0, 0, 0, _
                                             1, 0, 0, 0, 1, 0, 0, _
                                             1, 0, 0, 0, 0, 1, 0}

    Private A3 As Integer() = New Integer() {0, 0, 0, 1, 0, 0, 0, _
                                             0, 0, 0, 1, 0, 0, 0, _
                                             0, 0, 1, 0, 1, 0, 0, _
                                             0, 0, 1, 0, 1, 0, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 1, 1, 1, 1, 0, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 1, 0, 0, 0, 1, 1}

    Private B3 As Integer() = New Integer() {1, 1, 1, 1, 1, 1, 0, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 1, 1, 1, 1, 0, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             1, 1, 1, 1, 1, 1, 0}

    Private C3 As Integer() = New Integer() {0, 0, 1, 1, 1, 0, 1, _
                                             0, 1, 0, 0, 0, 1, 1, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 0, _
                                             1, 0, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 0, 1, 1, 1, 0, 0}

    Private D3 As Integer() = New Integer() {1, 1, 1, 1, 0, 0, 0, _
                                             0, 1, 0, 0, 1, 0, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 1, 0, 0, _
                                             1, 1, 1, 1, 0, 0, 0}

    Private E3 As Integer() = New Integer() {1, 1, 1, 1, 1, 1, 1, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             0, 1, 0, 0, 1, 0, 0, _
                                             0, 1, 1, 1, 1, 0, 0, _
                                             0, 1, 0, 0, 1, 0, 0, _
                                             0, 1, 0, 0, 0, 0, 0, _
                                             0, 1, 0, 0, 0, 0, 0, _
                                             0, 1, 0, 0, 0, 0, 1, _
                                             1, 1, 1, 1, 1, 1, 1}

    Private J3 As Integer() = New Integer() {0, 0, 0, 0, 1, 1, 1, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 0, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 0, 1, 1, 1, 0, 0}

    Private K3 As Integer() = New Integer() {1, 1, 1, 0, 0, 1, 1, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             0, 1, 0, 0, 1, 0, 0, _
                                             0, 1, 0, 1, 0, 0, 0, _
                                             0, 1, 1, 0, 0, 0, 0, _
                                             0, 1, 0, 1, 0, 0, 0, _
                                             0, 1, 0, 0, 1, 0, 0, _
                                             0, 1, 0, 0, 0, 1, 0, _
                                             1, 1, 1, 0, 0, 1, 1}

    Private mFontNames As String() = New String() {"A1", "B1", "C1", "D1", _
                                                    "E1", "J1", "K1", _
                                                    "A2", "B2", "C2", "D2", _
                                                    "E2", "J2", "K2", _
                                                    "A3", "B3", "C3", "D3", _
                                                    "E3", "J3", "K3"}

    Private Pattern As Integer()() = New Integer(20)() {}

    Private Target As Integer() = New Integer() {0, 1, 2, 3, 4, 5, 6, _
                                                 0, 1, 2, 3, 4, 5, 6, _
                                                 0, 1, 2, 3, 4, 5, 6}

    Private Sub InitializePatterns()
        'Insert pattern arrays into Pattern() to make an array of arrays.
        Pattern(0) = A1
        Pattern(1) = B1
        Pattern(2) = C1
        Pattern(3) = D1
        Pattern(4) = E1
        Pattern(5) = J1
        Pattern(6) = K1
        Pattern(7) = A2
        Pattern(8) = B2
        Pattern(9) = C2
        Pattern(10) = D2
        Pattern(11) = E2
        Pattern(12) = J2
        Pattern(13) = K2
        Pattern(14) = A3
        Pattern(15) = B3
        Pattern(16) = C3
        Pattern(17) = D3
        Pattern(18) = E3
        Pattern(19) = J3
        Pattern(20) = K3
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
                                                                Handles Button1.Click
        Dim i As Integer

        InitializePatterns()
        LVQ = New LVQ1class(NUMBER_OF_CLUSTERS, _
                            VEC_LEN, TRAINING_PATTERNS, _
                            Pattern, _
                            Target)

        For i = 0 To NUMBER_OF_CLUSTERS - 1
            LVQ.initializeWeights(i, Pattern(i))
            TextBox1.Text += "Weights for cluster " + CStr(i) + _
                          " initialized to pattern " + CStr(mFontNames(i)) + _
                          vbCrLf
        Next i

        LVQ.trainNetwork()
        i = LVQ.getCluster(A)
        TextBox1.Text = "Pattern " + mFontNames(i) + " belongs to cluster " + _
                       CStr(i) + vbCrLf
        'For i = 0 To 6
        '    For j = 0 To 20
        '        Debug.Print(LVQ.weights(i, j).ToString)
        '    Next j
        'Next i
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
