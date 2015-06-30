Public Class CharRecon
    ' Number of characters in the class, if you want to increase the # of characters
    Private Const NUMBER_OF_CLUSTERS As Integer = 7
    ' Number of inputs in the ANN
    Private Const VEC_LEN As Integer = 63
    ' Number of training patterns
    Private Const TRAINING_PATTERNS As Integer = 21

    Private LVQNew As LVQ

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
    Private charN As Integer()

    Private mFontNames As Char() = New Char() {"A", "B", "C", "D", "E", "J", "K"}

    Private Pattern As Integer()() = New Integer(20)() {}

    Private Target As Integer() = New Integer() {0, 1, 2, 3, 4, 5, 6, _
                                                 0, 1, 2, 3, 4, 5, 6, _
                                                 0, 1, 2, 3, 4, 5, 6}
    Private Sub InitializePatterns()
        'Load each character in to a pattern array.
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
    Public Property CharNew()
        Get
            Return CharN
        End Get
        Set(ByVal value)
            charN = value
        End Set
    End Property
    Public Sub TrainNetwork()
        Dim i As Integer
        InitializePatterns()

        ' Define the type of LVQ
        LVQNew = New LVQ(NUMBER_OF_CLUSTERS, VEC_LEN, TRAINING_PATTERNS, Pattern, Target)

        ' Initializing weights
        For i = 0 To NUMBER_OF_CLUSTERS - 1
            LVQNew.initializeWeights(i, Pattern(i))
        Next

        ' Train the network
        LVQNew.trainNetwork()
    End Sub

    Public Function getCharac() As Char
        Return mFontNames(LVQNew.getCluster(charN))
    End Function
End Class
