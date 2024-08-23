
Friend Module Program
    Public DefaultMac As String = "F0-2F-74-DC-58-5E"

    <STAThread()>
    Friend Sub Main(args As String())
        If (args.Count() > 0 AndAlso args(0).ToLower() = "cli") Then
            If (args.Count() > 1) Then
                DefaultMac = args(1)
            End If
            frmMain.WakeupOnLan(DefaultMac).Wait()
            Console.WriteLine($"send WOL to {DefaultMac} address")
        Else
            Application.SetHighDpiMode(HighDpiMode.SystemAware)
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New frmMain)

        End If
    End Sub
End Module

