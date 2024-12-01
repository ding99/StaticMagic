using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageLibs.BinarizeJpeg;

public class Binarizer {

    public bool Start(string ipath) {

        Console.WriteLine($"input path:  [{ipath}]");
        var folder = Path.GetDirectoryName(ipath) ?? string.Empty;
        var name = Path.GetFileNameWithoutExtension(ipath);
        var rpath = Path.Combine(folder, $"{name}_r.png");
        var gpath = Path.Combine(folder, $"{name}_g.png");
        var bpath = Path.Combine(folder, $"{name}_b.png");
        Console.WriteLine($"output path: [{bpath}]");

        using var imageR = Image.Load<Rgba32>(ipath);
        using var imageG = imageR.Clone();
        using var imageB = imageR.Clone();

        var w = imageR.Width;
        var h = imageR.Height;
        Console.WriteLine($"Dimensions: {w} x {h}");

        int step = 255;

        for (int y = 0; y < h; y++) {
            for(int x = 0; x < w; x++) {
                Rgba32 pr = imageR[x, y];
                Rgba32 pg = imageG[x, y];
                Rgba32 pb = imageB[x, y];

                if (pr.R < step || pr.G < step || pr.B < step) {
                    pr.G = 0;
                    pr.B = 0;
                } else {
                    pr.R = 255;
                    pr.G = 255;
                    pr.B = 255;
                    pr.A = 255;
                }
                imageR[x, y] = pr;

                if (pg.R < step || pg.G < step || pg.B < step) {
                    pg.R = 0;
                    pg.B = 0;
                } else {
                    pg.R = 255;
                    pg.G = 255;
                    pg.B = 255;
                    pg.A = 255;
                }
                imageG[x, y] = pg;

                if (pb.R < step || pb.G < step || pb.B < step) {
                    pb.R = 0;
                    pb.G = 0;
                } else {
                    pb.R = 255;
                    pb.G = 255;
                    pb.B = 255;
                    pb.A = 255;
                }
                imageB[x, y] = pb;
            }
        }

        imageR.Save(rpath);
        imageG.Save(gpath);
        imageB.Save(bpath);

        return true;
    }

}
