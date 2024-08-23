Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Text.RegularExpressions

Public Class frmMain

    Dim client As Sockets.UdpClient

    Private Async Sub btnTrunOn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrunOn.Click
        Await WakeupOnLan(txtMac.Text)
    End Sub

    Public Shared Async Function WakeupOnLan(macAddress As String) As Task

        Dim magicPacket As Byte() = BuildMagicPacket(macAddress)
        For Each networkInterface As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces() _
            .Where(Function(n) n.NetworkInterfaceType <> NetworkInterfaceType.Loopback AndAlso n.OperationalStatus = OperationalStatus.Up)

            Dim IPInterfaceProperties As IPInterfaceProperties = networkInterface.GetIPProperties()
            For Each multicastIPAddressInformation As MulticastIPAddressInformation In IPInterfaceProperties.MulticastAddresses

                Dim multicastIpAddress As IPAddress = multicastIPAddressInformation.Address
                If multicastIpAddress.ToString().StartsWith("ff02::1%", StringComparison.OrdinalIgnoreCase) Then  ' Ipv6 Then All hosts on LAN (with zone index)
                    'Nullable(Of )
                    Dim unicastIPAddressInformation As UnicastIPAddressInformation = IPInterfaceProperties.UnicastAddresses _
                        .Where(Function(u) u.Address.AddressFamily = AddressFamily.InterNetworkV6 AndAlso Not u.Address.IsIPv6LinkLocal).FirstOrDefault()
                    If unicastIPAddressInformation IsNot Nothing Then
                        Await SendWakeupOnLan(unicastIPAddressInformation.Address, multicastIpAddress, magicPacket)
                    End If

                ElseIf (multicastIpAddress.ToString().Equals("224.0.0.1")) Then ' Ipv4 Then All hosts on LAN

                    Dim unicastIPAddressInformation As UnicastIPAddressInformation = IPInterfaceProperties.UnicastAddresses _
                        .Where(Function(u) u.Address.AddressFamily = AddressFamily.InterNetwork AndAlso Not IPInterfaceProperties.GetIPv4Properties().IsAutomaticPrivateAddressingActive).FirstOrDefault()
                    If unicastIPAddressInformation IsNot Nothing Then

                        Await SendWakeupOnLan(unicastIPAddressInformation.Address, multicastIpAddress, magicPacket)
                    End If
                End If
            Next
        Next
    End Function

    Private Shared Function BuildMagicPacket(macAddress As String) As Byte() ' MacAddress In any standard HEX format

        macAddress = Regex.Replace(macAddress, "[: -]", "")
        Dim macBytes As Byte() = Convert.FromHexString(macAddress)
        Dim header As IEnumerable(Of Byte) = Enumerable.Repeat(Of Byte)(Byte.MaxValue, 6) ' First 6 times 0xff 
        Dim Data As IEnumerable(Of Byte) = Enumerable.Repeat(macBytes, 16).SelectMany(Function(m) m) ' Then 16 times MacAddress
        Return header.Concat(Data).ToArray()
    End Function

    Private Shared Async Function SendWakeupOnLan(localIpAddress As IPAddress, multicastIpAddress As IPAddress, magicPacket As Byte()) As Task
        Using client As UdpClient = New UdpClient(New IPEndPoint(localIpAddress, 0))
            Await client.SendAsync(magicPacket, magicPacket.Length, New IPEndPoint(multicastIpAddress, 9))
        End Using
    End Function
End Class
