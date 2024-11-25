Public Class Alarm
    Public Property Number As String
    Public Property Name As String
    Public Shared Property Alarms As List(Of Alarm) = New List(Of Alarm)

    ' Constructor to initialize an individual alarm
    Public Sub New(alarmNumber As String, alarmName As String)
        Me.Number = alarmNumber
        Me.Name = alarmName
    End Sub

    ' Method to populate the list with alarms
    Public Shared Sub InitializeAlarms()
        Alarms.Add(New Alarm("01AB", "EMERGENCY STOP"))
        Alarms.Add(New Alarm(2, "X AXIS SERVO ALARM"))
        Alarms.Add(New Alarm(3, "Y AXIS SERVO ALARM"))
        Alarms.Add(New Alarm(4, "Z AXIS SERVO ALARM"))
        Alarms.Add(New Alarm(5, "W AXIS SERVO ALARM"))
        Alarms.Add(New Alarm(6, "SERVO POWER OFF "))
        Alarms.Add(New Alarm(7, "SERVO POWER OFF X AXIS "))
        Alarms.Add(New Alarm(8, "SERVO POWER OFF Y AXIS  "))
        Alarms.Add(New Alarm(9, "SERVO POWER OFF Z AXIS  "))
        Alarms.Add(New Alarm(10, "AXIS W IS NOT ON"))
        Alarms.Add(New Alarm(11, "POSITIONING ERROR MODULE 1 "))
        Alarms.Add(New Alarm(12, "POSITIONING ERROR MODULE 2 "))
        Alarms.Add(New Alarm(13, "+ LIMIT X AXIS"))
        Alarms.Add(New Alarm(14, "- LIMIT X AXIS"))
        Alarms.Add(New Alarm(15, "X AXIS FORWARD LIMIT"))
        Alarms.Add(New Alarm(16, "X AXIS REVERSE LIMIT"))
        Alarms.Add(New Alarm(17, "Z AXIS UPPER LIMIT"))
        Alarms.Add(New Alarm(18, "Z AXIS LOWER LIMIT"))
        Alarms.Add(New Alarm(19, "W AXIS FORWARD LIMIT"))
        Alarms.Add(New Alarm(20, "FIDUCIAL FAILED"))
        Alarms.Add(New Alarm(21, "LASER MARKER NOT READY"))
        Alarms.Add(New Alarm(22, "MACHINE DOOR IS OPENED"))
        Alarms.Add(New Alarm(23, "AIR PRESSOR NOK"))
        Alarms.Add(New Alarm(24, "SELECT ANY OPERATION MODE"))
        Alarms.Add(New Alarm(25, "GATE LEFT NOT OPEN"))
        Alarms.Add(New Alarm(26, "GATE LEFT NOT CLOSED"))
        Alarms.Add(New Alarm(27, "GATE RIGHT NOT OPEN"))
        Alarms.Add(New Alarm(28, "GATE RIGHT NOT CLOSED"))
        Alarms.Add(New Alarm(29, "CONVEYOR WIDTH INPUT ERROR"))
        Alarms.Add(New Alarm(30, "CONVEYOR IS NOT ON SET VALUE"))
        Alarms.Add(New Alarm(31, "MACHINE NEED TO INITIALIZE FIRST"))
        Alarms.Add(New Alarm(32, "AUTO MODE DISABLE"))
        Alarms.Add(New Alarm(33, "Y AXIS MOVE DISABLED, NEED MOVE X AXIS FIRST"))
        Alarms.Add(New Alarm(34, "CYCLE INTERRUPTED, PLEASE RESTART AGAIN"))
        Alarms.Add(New Alarm(35, "LIVE MODE NOT TURNED ON "))
        Alarms.Add(New Alarm(36, "FIDUCIAL FAILED START NEW CYCLE"))
        Alarms.Add(New Alarm(37, "FRONT FLIPPER IS NOT IN HOME POSITION"))
        Alarms.Add(New Alarm(38, "REAR FLIPPER IS NOT IN HOME POSITION"))
        Alarms.Add(New Alarm(39, "NEED TO MOVE FLIP POSITION FIRST"))
        Alarms.Add(New Alarm(40, "ALL AXIS SERVO IS OFF"))
        Alarms.Add(New Alarm(41, "HOME OPERATION UNABLE TO PERFORM BECAUSE PCB IS ALREADY CLAMPED"))
        Alarms.Add(New Alarm(42, "AUTO OPERATION CAN NOT OPERATE DUE TO PCB IS AVAILABLE ON TRACK"))
        Alarms.Add(New Alarm(43, "PLC TO LASER COMMUNICATION ERROR"))
        Alarms.Add(New Alarm(44, "NI TO LASER COMMUNICATION ERROR"))
        Alarms.Add(New Alarm(45, "PLC TO VISION COMMUNICATION ERROR"))
    End Sub

    ' Override ToString for easy display
    Public Overrides Function ToString() As String
        Return $"{Number}: {Name}"
    End Function
End Class
