using ImageLibs.BinarizeJpeg;

if (args.Length < 1) {
    Console.WriteLine("usage: clearlogo <path>");
    return;
}

Console.WriteLine("== Start");

var converter = new Binarizer().Start(args[0]);
Console.WriteLine($"Result: {converter}");

Console.WriteLine("== End");