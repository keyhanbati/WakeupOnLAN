﻿// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

class Program
{
    static void Main(string[] args)
    {
        var mac = "F0-2F-74-DC-58-5E";
        if (args.Count() > 0)
            mac = args[0];
        WakeOnLan(mac).Wait();
        Console.WriteLine($"send WOL to {mac} address");
        Console.ReadLine();
    }

    public static async Task WakeOnLan(string macAddress)
    {
        byte[] magicPacket = BuildMagicPacket(macAddress);
        foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces().Where((n) =>
            n.NetworkInterfaceType != NetworkInterfaceType.Loopback && n.OperationalStatus == OperationalStatus.Up))
        {
            IPInterfaceProperties iPInterfaceProperties = networkInterface.GetIPProperties();
            foreach (MulticastIPAddressInformation multicastIPAddressInformation in iPInterfaceProperties.MulticastAddresses)
            {
                IPAddress multicastIpAddress = multicastIPAddressInformation.Address;
                if (multicastIpAddress.ToString().StartsWith("ff02::1%", StringComparison.OrdinalIgnoreCase)) // Ipv6: All hosts on LAN (with zone index)
                {
                    UnicastIPAddressInformation? unicastIPAddressInformation = iPInterfaceProperties.UnicastAddresses.Where((u) =>
                        u.Address.AddressFamily == AddressFamily.InterNetworkV6 && !u.Address.IsIPv6LinkLocal).FirstOrDefault();
                    if (unicastIPAddressInformation != null)
                    {
                        await SendWakeOnLan(unicastIPAddressInformation.Address, multicastIpAddress, magicPacket);
                    }
                }
                else if (multicastIpAddress.ToString().Equals("224.0.0.1")) // Ipv4: All hosts on LAN
                {
                    UnicastIPAddressInformation? unicastIPAddressInformation = iPInterfaceProperties.UnicastAddresses.Where((u) =>
                        u.Address.AddressFamily == AddressFamily.InterNetwork && !iPInterfaceProperties.GetIPv4Properties().IsAutomaticPrivateAddressingActive).FirstOrDefault();
                    if (unicastIPAddressInformation != null)
                    {
                        await SendWakeOnLan(unicastIPAddressInformation.Address, multicastIpAddress, magicPacket);
                    }
                }
            }
        }
    }

    static byte[] BuildMagicPacket(string macAddress) // MacAddress in any standard HEX format
    {
        macAddress = Regex.Replace(macAddress, "[: -]", "");
        byte[] macBytes = Convert.FromHexString(macAddress);

        IEnumerable<byte> header = Enumerable.Repeat((byte)0xff, 6); //First 6 times 0xff
        IEnumerable<byte[]> data = Enumerable.Repeat(macBytes, 16);
        IEnumerable<byte> data2 = data.SelectMany(m => m); // then 16 times MacAddress
        return header.Concat(data2).ToArray();
    }

    static async Task SendWakeOnLan(IPAddress localIpAddress, IPAddress multicastIpAddress, byte[] magicPacket)
    {
        using UdpClient client = new(new IPEndPoint(localIpAddress, 0));
        await client.SendAsync(magicPacket, magicPacket.Length, new IPEndPoint(multicastIpAddress, 9));
    }


}


