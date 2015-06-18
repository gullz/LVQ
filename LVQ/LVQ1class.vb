Public Class LVQ1class

    Private Const DECAY_RATE As Double = 0.96 'About 100 iterations. 
    Private Const MIN_ALPHA As Double = 0.01
    Private Const RADIUS_REDUCTION_POINT As Double = 0.023 'Last 20% of iterations.


    Private clusters As Integer 'Total number of clusters.
    Private vLength As Integer 'Vector length.
    Private mIterations As Integer
    Private mReductionPoint As Integer = 0
    Private Alpha As Double = 0.6
    Private D() As Double 'Network nodes. The "clusters"

    'Weight matrix.
    Private w(,) As Double

    'Training patterns.
    Private mTrainPatterns
    Private mPattern As Integer()()
    Private mTarget As Integer()

    Public Sub New(ByVal numClusters As Integer, _
                   ByVal vecLength As Integer, _
                   ByVal numTPatterns As Integer, _
                   ByVal tPatterns As Integer()(), _
                   ByVal targets As Integer())

        clusters = numClusters
        vLength = vecLength
        mTrainPatterns = numTPatterns
        ReDim D(clusters - 1) 'Network nodes.

        'Training patterns.
        mPattern = New Integer(numTPatterns - 1)() {}
        mPattern = tPatterns

        ReDim mTarget(mTrainPatterns - 1)
        mTarget = targets

        'Weight matrix to be filled with values between 0.0 and 1.0
        ReDim w(clusters - 1, vLength - 1)

    End Sub

    Public Sub trainNetwork()

        training()

    End Sub

    Public Sub initializeWeights(ByVal clusterNumber As Integer, _
                                 ByVal trainingPattern As Integer())
        'clusterNumber = the output node (cluster) to assign the pattern to.
        'trainingPattern = the pattern which the output node will respond to.
        Dim i As Integer

        'Initialize weights.
        For i = 0 To vLength - 1
            w(clusterNumber, i) = CDbl(trainingPattern(i))
        Next i

    End Sub

    Private Sub training()
        Dim ReductionFlag As Boolean = False
        Dim VecNum As Integer
        Dim DMin As Integer

        mIterations = 0

        While Alpha > MIN_ALPHA
            mIterations += 1

            For VecNum = 0 To mTrainPatterns - 1

                'Compute input for all nodes.
                ComputeInput(mPattern, VecNum)

                'See which is smaller?
                DMin = minimum(D)
                Debug.Print(DMin)
                'Update the weights on the winning unit.
                UpdateWeights(VecNum, DMin)

            Next VecNum

            'Reduce the learning rate.
            Alpha = DECAY_RATE * Alpha

            'Reduce radius at specified point.
            If Alpha < RADIUS_REDUCTION_POINT Then
                If ReductionFlag = False Then
                    ReductionFlag = True
                    mReductionPoint = mIterations
                End If
            End If

        End While

    End Sub

    Public ReadOnly Property trainingPatterns()
        Get
            Return mTrainPatterns
        End Get
    End Property

    Public ReadOnly Property iterations()
        Get
            Return mIterations
        End Get
    End Property

    Public ReadOnly Property reductionPoint()
        Get
            Return mReductionPoint
        End Get
    End Property

    Public ReadOnly Property patterns() As Integer()()
        Get
            Return mPattern
        End Get
    End Property

    Public ReadOnly Property weights() As Double(,)
        Get
            Return w
        End Get
    End Property

    Public Function getCluster(ByVal inputPattern As Integer()) As Integer

        'Compute input for all nodes.
        ComputeInput(inputPattern)

        'See which is smaller?
        Return minimum(D)

    End Function

    Private Sub UpdateWeights(ByVal VectorNumber As Integer, ByVal DMin As Integer)
        Dim i As Integer

        For i = 0 To vLength - 1
            'Update the winner.
            If DMin = mTarget(VectorNumber) Then
                w(DMin, i) += (Alpha * (mPattern(VectorNumber)(i) - w(DMin, i)))
            Else
                w(DMin, i) -= (Alpha * (mPattern(VectorNumber)(i) - w(DMin, i)))
            End If
        Next i

    End Sub

    Private Sub ComputeInput(ByRef VectorArray As Integer()(), _
                             ByVal VectorNumber As Integer)
        'Overloaded function.  See ComputeInput below.
        Dim i, j As Integer

        clearArray(D)
        For i = 0 To clusters - 1
            For j = 0 To vLength - 1
                D(i) += (w(i, j) - VectorArray(VectorNumber)(j)) ^ 2

            Next j
        Next i

    End Sub

    Private Sub ComputeInput(ByRef VectorArray As Integer())
        'Overloaded function.  See ComputeInput above.
        Dim i, j As Integer

        clearArray(D)
        For i = 0 To clusters - 1
            For j = 0 To vLength - 1
                D(i) += (w(i, j) - VectorArray(j)) ^ 2
                'Debug.Print(D(i))
            Next j
        Next i

    End Sub

    Private Sub clearArray(ByRef anArray As Double())
        Dim i As Integer

        For i = 0 To clusters - 1
            anArray(i) = 0.0
        Next i

    End Sub

    Private Function minimum(ByRef NodeArray As Double()) As Integer
        Dim i As Integer
        Dim Winner As Integer
        Dim FoundNewWinner As Boolean
        Dim Done As Boolean = False

        Winner = 0
        Do Until Done

            FoundNewWinner = False
            For i = 0 To clusters - 1
                If i <> Winner Then             'Avoid self-comparison.
                    If NodeArray(i) < NodeArray(Winner) Then
                        Winner = i
                        FoundNewWinner = True
                    End If
                End If
            Next i

            If FoundNewWinner = False Then
                Done = True
            End If

        Loop

        Return Winner

    End Function

End Class
