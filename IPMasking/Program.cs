using System.Net;
using IPMasking;
using IPMasking.Extensions;
using Utilities.Extensions;

const string sameSubnetMessage = "True: IP Addresses are in the same subnet. \n";
const string differentSubnetMessage = "False: IP Addresses aren't in the same subnet. \n";


// Application entry point with exception handling
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

// Main application method
void RunApplication()
{
    PrintColoredMessage("Welcome to IP Validator", ConsoleColor.Cyan);
    PrintColoredMessage($"\nInfo:", ConsoleColor.White);
    PrintColoredMessage("For exit type 'exit' anytime\n", ConsoleColor.Yellow);

    var baseIPAddress = GetIpAddress($"Please input base IP Address: ");

    PrintMaskOptions();
    var baseMask = GetMaskOption("Select mask by entering its number:");

    PrintColoredMessage("\nConfiguration:", ConsoleColor.Cyan);
    PrintColoredMessage($"IP Address:\t{baseIPAddress}", ConsoleColor.DarkCyan);
    PrintColoredMessage($"Mask:\t\t{baseMask}", ConsoleColor.DarkCyan);

    PrintColoredMessage("\nValidation started! Enter IPs to compare with base address. \n", ConsoleColor.Cyan);

    while (true)
    {
        var newIp = GetIpAddress($"Please input IP Address to test:");

        var same = IsInSameSubnet(baseIPAddress, newIp, baseMask);


        if (same)
            PrintColoredMessage(sameSubnetMessage, ConsoleColor.Green);
        else
            PrintColoredMessage(differentSubnetMessage, ConsoleColor.Red);
    }
}

// Helper methods
IPAddress GetIpAddress(string prompt)
{
    while (true)
    {
        var input = GetAnswer(prompt);

        if (IPAddress.TryParse(input, out var ip))
            return ip;

        PrintColoredMessage("Invalid IP address, try again.\n", ConsoleColor.Red);
    }
}
IPAddress GetMaskOption(string prompt)
{
    while (true)
    {
        var input = GetAnswer(prompt);

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

// Display available mask options
void PrintMaskOptions()
{
    PrintColoredMessage("\nAvailable Masks:", ConsoleColor.White);

    foreach (Mask mask in Enum.GetValues(typeof(Mask)))
    {
        PrintColoredMessage($"{(byte)mask}: {mask.GetDisplayName()}", ConsoleColor.Yellow);
    }

    Console.WriteLine();
}
string GetAnswer(string prompt)
{
    while (true)
    {
        PrintColoredMessage(prompt, ConsoleColor.DarkYellow);
        var input = Console.ReadLine();

        if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            throw new OperationCanceledException();

        if (!string.IsNullOrWhiteSpace(input))
            return input;

        PrintColoredMessage("Invalid Input, try again.\n", ConsoleColor.Red);
    }
}

// Utility method
void PrintColoredMessage(string message, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ResetColor();
}
