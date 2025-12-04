using IPMasking;
using IPMasking.Extensions;
using System.Net;

try
{
    RunApplication();
}
catch (OperationCanceledException)
{
    SetColor(ConsoleColor.Yellow);
    Console.WriteLine("\nApplication terminated by user.");
    Console.ResetColor();
}
catch (Exception ex)
{
    SetColor(ConsoleColor.Red);
    Console.WriteLine($"\nUnexpected error: {ex.Message}");
    Console.ResetColor();
}


void RunApplication()
{
    SetColor(ConsoleColor.Cyan);
    Console.WriteLine("Welcome to IP Validator\n");
    Console.ResetColor();

    var baseIPAddress = GetIpAddress("Please input base IP Address (or type 'exit' to quit):");

    PrintMaskOptions();
    var baseMask = GetMaskOption("Select mask by entering its number:");

    SetColor(ConsoleColor.Cyan);
    Console.WriteLine("\nValidation started! Enter IPs to compare with base address.\n");
    Console.ResetColor();

    while (true)
    {
        var newIp = GetIpAddress("Please input IP Address (or type 'exit' to quit):");

        

        var same = IsInSameSubnet(baseIPAddress, newIp, baseMask);

        if (same)
        {
            SetColor(ConsoleColor.Green);
            Console.WriteLine("True");
        }
        else
        {
            SetColor(ConsoleColor.Red);
            Console.WriteLine("False");
        }

        // Add an empty line for better readability
        Console.WriteLine();

        Console.ResetColor();
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

        SetColor(ConsoleColor.Red);
        Console.WriteLine("Invalid IP address, try again.\n");
        Console.ResetColor();
    }
}


void PrintMaskOptions()
{
    Console.WriteLine("\nAvailable Masks:");

    foreach (Mask mask in Enum.GetValues(typeof(Mask)))
    {
        SetColor(ConsoleColor.Yellow);
        Console.WriteLine($"{(byte)mask}: {mask.GetDisplayName()}");
        Console.ResetColor();
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
            SetColor(ConsoleColor.Red);
            Console.WriteLine("Invalid input. Enter a number.\n");
            Console.ResetColor();
            continue;
        }

        if (Enum.IsDefined(typeof(Mask), prefixValue))
        {
            return MaskingExtension.GetMask((Mask)prefixValue);
        }

        SetColor(ConsoleColor.Red);
        Console.WriteLine("Unknown mask. Try again.\n");
        Console.ResetColor();
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


void SetColor(ConsoleColor color)
{
    Console.ForegroundColor = color;
}
