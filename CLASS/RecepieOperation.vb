
Public Class RecepieOperation
    Public Sub New()
        _RECIPIEDETAILS = New RECIPIEDETAILS()
        _SIDE = New SIDE()
        _JOG = New JOG()
        _VISION = New VISIONPROCESS()
        _PLC = New PLC()
        _WPROCESS = New WPROCESS()
        _ZPROCESS = New ZPROCESS()
        _RECEIPENAME = New RECEIPENAME()
    End Sub

    Public Property _RECIPIEDETAILS As RECIPIEDETAILS
    Public Property _VISION As VISIONPROCESS
    Public Property _SIDE As SIDE
    Public Property _JOG As JOG
    Public Property _ZPROCESS As ZPROCESS
    Public Property _WPROCESS As WPROCESS
    Public Property _PLC As PLC
    Public Property _RECEIPENAME As RECEIPENAME


    Public Class RECIPIEDETAILS
        Public Property _BOARD As BOARD
        Public Property _LOCATION As LOCATIONS
        Public Property _TEACH As TEACH
        Public Property _CODE As CODE

        Public Property _SHAPE As SHAPE
        Public Property _PARTCOLOR As PARTCOLOR
        Public Property _MARKPOSITION As MARKPOSITION

    End Class
    Public Class MARKPOSITION
        Public Property xvalue As String
        Public Property yvalue As String
        Public Property che As String
        Public Property P_THK As String
    End Class
    Public Class BOARD
        Public Property BOARDNAME As String
        Public Property P_LENGTH As String
        Public Property P_WIDTH As String
        Public Property P_THK As String
        Public Property P_WEIGHT As String
        Public Property WIDTH As String
        Public Property MARGIN As String
        Public Property ROWCOUNT As String
        Public Property ROWPITCH As String
        Public Property COULMNCOUNT As String
        Public Property COULMNPITCH As String
    End Class

    Public Class LOCATIONS
        Public Property _POSITION As List(Of POSITIONS)
        Public Property _CONVEYOR As List(Of CONVEYOR)
    End Class

    Public Class POSITIONS
        Public Property LOADPOSITION As String
        Public Property UNLOADPOSITION As String
        Public Property HOMEPOSITION As String
    End Class

    Public Class CONVEYOR
        Public Property PCBSTOPPER As String
        Public Property PCBLOAD As String
        Public Property GATEOPENL As String
        Public Property PCBCLAMB As String
        Public Property PCBUNLOAD As String
        Public Property GATEOPENR As String
        Public Property SERVOON As String
    End Class

    Public Class TEACH

        Public Property MOVE As String
        Public Property TEACHPOS As String
        Public Property LEARSHAPE As String
        Public Property SAVE As String
        Public Property TEST As String
        Public Property X As String ' New Property
        Public Property Y As String ' New Property
        Public Property CW As String ' New Property
    End Class

    Public Class CODE
        Public Property VARIABLE As String
        Public Property VARIABLEINPUT As String
        Public Property JUSTIFY As String
        Public Property STRINGCODE As String
        Public Property STRINGCODEASCIIVALUE As String
        Public Property SCANSPEED As String
        Public Property LASERPOWER As String
        Public Property pROCESS As PROCESS
    End Class



    Public Class VISIONPROCESS
        Public Property CONNECT As String
        Public Property F1TRIG As String
        Public Property F2TRIG As String
        Public Property TEST As String
        Public Property EXHAST As String
        Public Property LENABLE As String
        Public Property LMARK As String
        Public Property _F1 As F1
        Public Property _F2 As F2
    End Class

    Public Class F1
        Public Property SHAPE As String
        Public Property COLOR As String
        Public Property ROX1 As String
        Public Property ROY1 As String
        Public Property ROX2 As String
        Public Property ROY2 As String
        Public Property THRESHOLD As String
        Public Property AREARADIUS As String
    End Class

    Public Class F2
        Public Property SHAPE As String
        Public Property COLOR As String
        Public Property ROX1 As String
        Public Property ROY1 As String
        Public Property ROX2 As String
        Public Property ROY2 As String
        Public Property THRESHOLD As String
        Public Property AREARADIUS As String
    End Class

    Public Class SIDE
        Public Property FRONT As String
        Public Property BACK As String
    End Class
    Public Class SHAPE
        Public Property Circle As String
        Public Property Rectangle As String
        Public Property Genric As String
    End Class
    Public Class PARTCOLOR
        Public Property RED As String
        Public Property BLUE As String
        Public Property GREEN As String
    End Class

    Public Class JOG
        Public Property JOGUP As String
        Public Property JOGDOWN As String
        Public Property JOGFORWARD As String
        Public Property JOGBACKWARD As String
    End Class

    Public Class ZPROCESS
        Public Property ZUP As String
        Public Property ZDOWN As String
    End Class

    Public Class WPROCESS
        Public Property WUP As String
        Public Property WDOWN As String
    End Class

    Public Class PLC
        Public Property Xvalue As String
        Public Property Yvalue As String
    End Class

    Public Class RECEIPENAME
        Public Property RNAME As String
        Public Property RPATH As String
    End Class
End Class
