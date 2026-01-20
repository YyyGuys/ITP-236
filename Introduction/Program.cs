using System;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("ITP-236 Spring-2026 Introduction Project (Bob Dust)");
Console.WriteLine(new string('-', 80));
PrintHeader();

Print("bool", sizeof(bool), "false | true", "true");
Print("byte", sizeof(byte), $"{byte.MinValue} .. {byte.MaxValue}", "255");
Print("sbyte", sizeof(sbyte), $"{sbyte.MinValue} .. {sbyte.MaxValue}", "-128");
Print("short", sizeof(short), $"{short.MinValue} .. {short.MaxValue}", "-32768");
Print("ushort", sizeof(ushort), $"{ushort.MinValue} .. {ushort.MaxValue}", "65535");
Print("int", sizeof(int), $"{int.MinValue} .. {int.MaxValue}", "123456");
Print("uint", sizeof(uint), $"{uint.MinValue} .. {uint.MaxValue}", "123456u");
Print("long", sizeof(long), $"{long.MinValue} .. {long.MaxValue}", "1234567890123L");
Print("ulong", sizeof(ulong), $"{ulong.MinValue} .. {ulong.MaxValue}", "1234567890123UL");
Print("float", sizeof(float), $"≈ {float.MinValue} .. {float.MaxValue} (single)", "3.14f");
Print("double", sizeof(double), $"≈ {double.MinValue} .. {double.MaxValue} (double)", "3.1415926535");
Print("decimal", sizeof(decimal), $"≈ {decimal.MinValue} .. {decimal.MaxValue} (128-bit)", "3.1415926535897932384626433833m");
Print("char", sizeof(char), "UTF-16 code unit (0..65535)", "'A' (65)");
Print("string", null, "Unicode text (reference type)", "\"Hello, world!\"");
Print("object", null, "Base CLR type (reference type)", "new object()");

Console.WriteLine(new string('-', 80));
Console.WriteLine("Press any key to exit...");
Console.ReadKey(intercept: true);

static void PrintHeader()
{
    Console.WriteLine("{0,-10} {1,6} {2,-44} {3}", "Type", "Bytes", "Range / Notes", "Example");
    Console.WriteLine(new string('-', 80));
}

static void Print(string name, int? sizeInBytes, string rangeOrNotes, string example)
{
    var sizeText = sizeInBytes.HasValue ? sizeInBytes.Value.ToString() : "N/A";
    Console.WriteLine("{0,-10} {1,6} {2,-44} {3}", name, sizeText, Truncate(rangeOrNotes, 44), example);
}

static string Truncate(string s, int maxLen)
{
    if (s.Length <= maxLen) return s;
    return s.Substring(0, maxLen - 3) + "...";
}
