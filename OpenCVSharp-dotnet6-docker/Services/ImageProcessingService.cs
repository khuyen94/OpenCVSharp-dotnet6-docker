using OpenCvSharp;

namespace OpenCVSharp_dotnet6_docker.Services
{
    public static class ImageProcessingService
    {
        // as seen here: https://pyimagesearch.com/2015/09/07/blur-detection-with-opencv/
        public static bool IsBlurry(string imageFilePath, double threshold = 100.0)
        {
            Mat mat = Cv2.ImRead(imageFilePath, ImreadModes.Grayscale);
            var varianceOfLaplacian = VarianceOfLaplacian(mat);
            return varianceOfLaplacian < threshold;
        }

        // as seen here: https://stackoverflow.com/questions/58005091/how-to-get-the-variance-of-laplacian-in-c-sharp
        private static double VarianceOfLaplacian(Mat mat)
        {
            using var laplacian = new Mat();
            int kernel_size = 3;
            int scale = 1;
            int delta = 0;
            int ddepth = mat.Type().Depth;
            Cv2.Laplacian(mat, laplacian, ddepth, kernel_size, scale, delta);
            Cv2.MeanStdDev(laplacian, out var mean, out var stddev);
            return stddev.Val0 * stddev.Val0;
        }
    }
}
