using System.Net;
using IPMasking;
using IPMasking.Extensions;
using Utilities.Extensions;

const string exitMessage = "(or type 'exit' to quit)";

try
{
    RunApplication();
}
catch (OperationCanceledException)
{
    PrintColoredMessage("\nApplication terminated by user.", ConsoleColor.Yellow);
}
catch (Exception ex)
{
    PrintColoredMessage($"\nUnexpected error: {ex.Message}", ConsoleColor.Red);
}

void RunApplication()
{
    PrintColoredMessage("Welcome to IP Validator\n", ConsoleColor.Cyan);

    var baseIPAddress = GetIpAddress($"Please input base IP Address {exitMessage}:");

    PrintMaskOptions();
    var baseMask = GetMaskOption("Select mask by entering its number:");

    PrintColoredMessage("\nValidation started! Enter IPs to compare with base address.\n", ConsoleColor.Cyan);

    while (true)
    {
        var newIp = GetIpAddress($"Please input IP Address {exitMessage}:");

        var same = IsInSameSubnet(baseIPAddress, newIp, baseMask);

        if (same)
            PrintColoredMessage("True\n", ConsoleColor.Green);
        else
            PrintColoredMessage("False\n", ConsoleColor.Red);
    }
}

IPAddress GetIpAddress(string prompt)
{
    while (true)
    {
        Console.WriteLine(prompt);
        var input = Console.ReadLine();

        if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            throw new OperationCanceledException();

        if (IPAddress.TryParse(input, out var ip))
            return ip;

        PrintColoredMessage("Invalid IP address, try again.\n", ConsoleColor.Red);
    }
}

void PrintMaskOptions()
{
    PrintColoredMessage("\nAvailable Masks:", ConsoleColor.White);

    foreach (Mask mask in Enum.GetValues(typeof(Mask)))
    {
        PrintColoredMessage($"{(byte)mask}: {mask.GetDisplayName()}", ConsoleColor.Yellow);
    }

    Console.WriteLine();
}

IPAddress GetMaskOption(string prompt)
{
    while (true)
    {
        Console.WriteLine(prompt);
        var input = Console.ReadLine();

        if (!byte.TryParse(input, out byte prefixValue))
        {
            PrintColoredMessage("Invalid input. Enter a number.\n", ConsoleColor.Red);
            continue;
        }

        if (Enum.IsDefined(typeof(Mask), prefixValue))
            return MaskingExtension.GetMask((Mask)prefixValue);

        PrintColoredMessage("Unknown mask. Try again.\n", ConsoleColor.Red);
    }
}

bool IsInSameSubnet(IPAddress ip1, IPAddress ip2, IPAddress mask)
{
    var ip1Bytes = ip1.GetAddressBytes();
    var ip2Bytes = ip2.GetAddressBytes();
    var maskBytes = mask.GetAddressBytes();

    if (ip1Bytes.Length != ip2Bytes.Length || ip1Bytes.Length != maskBytes.Length)
        throw new ArgumentException("IP and mask lengths do not match.");

    for (int i = 0; i < ip1Bytes.Length; i++)
    {
        if ((ip1Bytes[i] & maskBytes[i]) != (ip2Bytes[i] & maskBytes[i]))
            return false;
    }

    return true;
}

void PrintColoredMessage(string message, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ResetColor();
}
