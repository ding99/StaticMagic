using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;


namespace ImageLibs.BinarizeJpeg;

public class Binarizer {

    public bool Start(string ipath) {

        Console.WriteLine($"input path:  [{ipath}]");
        var folder = Path.GetDirectoryName(ipath) ?? string.Empty;
        var name = Path.GetFileNameWithoutExtension(ipath);
        var rpath = Path.Combine(folder, $"{name}_red.png");
        var gpath = Path.Combine(folder, $"{name}_green.png");
        var bpath = Path.Combine(folder, $"{name}_blue.png");
        var dpath = Path.Combine(folder, $"{name}_black.png");
        Console.WriteLine($"output path: [{bpath}]");

        using var imageR = Image.Load<Rgba32>(ipath);
        using var imageG = imageR.Clone();
        using var imageB = imageR.Clone();
        using var imageD = imageR.Clone();

        var w = imageR.Width;
        var h = imageR.Height;
        Console.WriteLine($"Dimensions: {w} x {h}");

        int step = 175;

        for (int y = 0; y < h; y++) {
            for(int x = 0; x < w; x++) {
                Rgba32 pr = imageR[x, y];
                Rgba32 pg = imageG[x, y];
                Rgba32 pb = imageB[x, y];
                Rgba32 pd = imageD[x, y];

                if (pr.R < step) {
                    pr.R = 168;
                    pr.G = 24;
                    pr.B = 32;
                    pr.A = 255;
                } else {
                    pr.R = 255;
                    pr.G = 255;
                    pr.B = 255;
                    pr.A = 0;
                }
                imageR[x, y] = pr;

                if (pg.G < step) {
                    pg.R = 0;
                    pg.G = 128;
                    pg.B = 72;
                    pg.A = 255;
                } else {
                    pg.R = 255;
                    pg.G = 255;
                    pg.B = 255;
                    pg.A = 0;
                }
                imageG[x, y] = pg;

                if (pb.B < step) {
                    pb.R = 8;
                    pb.G = 112;
                    pb.B = 192;
                    pb.A = 255;

                    pd.R = 0;
                    pd.G = 0;
                    pd.B = 0;
                    pd.A = 255;
                } else {
                    pb.R = 255;
                    pb.G = 255;
                    pb.B = 255;
                    pb.A = 0;

                    pd.R = 255;
                    pd.G = 255;
                    pd.B = 255;
                    pd.A = 0;
                }
                imageB[x, y] = pb;
                imageD[x, y] = pd;
            }
        }

        imageR.Save(rpath);
        imageG.Save(gpath);
        imageB.Save(bpath);
        imageD.Save(dpath);

        return true;
    }

}
