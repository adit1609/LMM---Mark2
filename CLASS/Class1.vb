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
        Alarms.Add(New Alarm("0001", "EMERGENCY STOP"))
        Alarms.Add(New Alarm("0002", "X AXIS SERVO ALARM"))
        Alarms.Add(New Alarm("0003", "Y AXIS SERVO ALARM"))
        Alarms.Add(New Alarm("0003", "Z AXIS SERVO ALARM"))
        Alarms.Add(New Alarm("0004", "W AXIS SERVO ALARM"))
        Alarms.Add(New Alarm("0005", "SERVO POWER OFF "))
        Alarms.Add(New Alarm("0006", "SERVO POWER OFF X AXIS "))
        Alarms.Add(New Alarm("0007", "SERVO POWER OFF Y AXIS  "))
        Alarms.Add(New Alarm("0008", "SERVO POWER OFF Z AXIS  "))
        Alarms.Add(New Alarm("0010", "AXIS W IS NOT ON"))
        Alarms.Add(New Alarm("0011", "POSITIONING ERROR MODULE 1 "))
        Alarms.Add(New Alarm("0012", "POSITIONING ERROR MODULE 2 "))
        Alarms.Add(New Alarm("0013", "+ LIMIT X AXIS"))
        Alarms.Add(New Alarm("0014", "- LIMIT X AXIS"))
        Alarms.Add(New Alarm("0015", "X AXIS FORWARD LIMIT"))
        Alarms.Add(New Alarm("0016", "X AXIS REVERSE LIMIT"))
        Alarms.Add(New Alarm("0017", "Z AXIS UPPER LIMIT"))
        Alarms.Add(New Alarm("0018", "Z AXIS LOWER LIMIT"))
        Alarms.Add(New Alarm("0019", "W AXIS FORWARD LIMIT"))
        Alarms.Add(New Alarm("0020", "FIDUCIAL FAILED"))
        Alarms.Add(New Alarm("0021", "LASER MARKER NOT READY"))
        Alarms.Add(New Alarm("0022", "MACHINE DOOR IS OPENED"))
        Alarms.Add(New Alarm("0023", "AIR PRESSOR NOK"))
        Alarms.Add(New Alarm("0024", "SELECT ANY OPERATION MODE"))
        Alarms.Add(New Alarm("0025", "GATE LEFT NOT OPEN"))
        Alarms.Add(New Alarm("0026", "GATE LEFT NOT CLOSED"))
        Alarms.Add(New Alarm("0027", "GATE RIGHT NOT OPEN"))
        Alarms.Add(New Alarm("0028", "GATE RIGHT NOT CLOSED"))
        Alarms.Add(New Alarm("0029", "CONVEYOR WIDTH INPUT ERROR"))
        Alarms.Add(New Alarm("0030", "CONVEYOR IS NOT ON SET VALUE"))
        Alarms.Add(New Alarm("0031", "MACHINE NEED TO INITIALIZE FIRST"))
        Alarms.Add(New Alarm("0032", "AUTO MODE DISABLE"))
        Alarms.Add(New Alarm("0033", "Y AXIS MOVE DISABLED, NEED MOVE X AXIS FIRST"))
        Alarms.Add(New Alarm("0034", "CYCLE INTERRUPTED, PLEASE RESTART AGAIN"))
        Alarms.Add(New Alarm("0035", "LIVE MODE NOT TURNED ON "))
        Alarms.Add(New Alarm("0036", "FIDUCIAL FAILED START NEW CYCLE"))
        Alarms.Add(New Alarm("0037", "FRONT FLIPPER IS NOT IN HOME POSITION"))
        Alarms.Add(New Alarm("0038", "REAR FLIPPER IS NOT IN HOME POSITION"))
        Alarms.Add(New Alarm("0039", "NEED TO MOVE FLIP POSITION FIRST"))
        Alarms.Add(New Alarm("0040", "ALL AXIS SERVO IS OFF"))
        Alarms.Add(New Alarm("0041", "HOME OPERATION UNABLE TO PERFORM BECAUSE PCB IS ALREADY CLAMPED"))
        Alarms.Add(New Alarm("0042", "AUTO OPERATION CAN NOT OPERATE DUE TO PCB IS AVAILABLE ON TRACK"))
        Alarms.Add(New Alarm("0043", "PLC TO LASER COMMUNICATION ERROR"))
        Alarms.Add(New Alarm("0044", "NI TO LASER COMMUNICATION ERROR"))
        Alarms.Add(New Alarm("0045", "PLC TO VISION COMMUNICATION ERROR"))
    End Sub

    ' Override ToString for easy display
    Public Overrides Function ToString() As String
        Return $"{Number}: {Name}"
    End Function
End Class
