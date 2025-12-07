using System.Net;
using IPMasking.Core;

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
    PrintColoredMessage("Welcome to IP Validator", ConsoleColor.Cyan);
    PrintColoredMessage("\nInfo:");
    PrintColoredMessage("For exit type 'exit' anytime\n", ConsoleColor.Yellow);

    var input = GetCidr("Please input base IP Address (format X.X.X.X/Y): ");
    var baseIp = IpMaskingUtils.SeparateIpAddress(input);
    var prefix = IpMaskingUtils.SeparatePrefix(input);
    var mask = IpMaskingUtils.GetMask(prefix);

    PrintColoredMessage("\nConfiguration:", ConsoleColor.Cyan);
    PrintColoredMessage($"IP Address:\t{baseIp}", ConsoleColor.DarkCyan);
    PrintColoredMessage($"Prefix:\t\t{prefix}", ConsoleColor.DarkCyan);
    PrintColoredMessage($"Mask:\t\t{mask}", ConsoleColor.DarkCyan);

    PrintColoredMessage("\nValidation started! Enter IPs to compare with base address.\n", ConsoleColor.Yellow);

    while (true)
    {
        var testIp = GetIpAddress("Please input IP Address to test: ");
        var same = IpMaskingUtils.IsInSameSubnet(baseIp, testIp, mask);

        if (same)
            PrintColoredMessage("True: IP Addresses are in the same subnet.\n", ConsoleColor.Green);
        else
            PrintColoredMessage("False: IP Addresses aren't in the same subnet.\n", ConsoleColor.Red);
    }
}

// Input methods
string GetCidr(string prompt)
{
    while (true)
    {
        var input = GetAnswer(prompt);

        if (IpMaskingUtils.IsValidIpAddress(input))
            return input;

        PrintColoredMessage("Invalid IP address format, try again.\n", ConsoleColor.Red);
    }
}
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
string GetAnswer(string prompt)
{
    while (true)
    {
        PrintColoredMessage(prompt, ConsoleColor.DarkYellow);
        var input = Console.ReadLine();

        if (input != null && input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            throw new OperationCanceledException();

        if (!string.IsNullOrWhiteSpace(input))
            return input;

        PrintColoredMessage("Invalid input, try again.\n", ConsoleColor.Red);
    }
}

// Print
void PrintColoredMessage(string message, ConsoleColor color = ConsoleColor.White)
{
    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ResetColor();
}