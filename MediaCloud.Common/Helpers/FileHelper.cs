using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;

namespace MediaCloud.Common.Helpers {

    public static class FileHelper {

        /// <summary>
        /// Checks if a file exists in the file system.
        /// </summary>
        /// <param name="fileInfo">FileInfo of the file</param>
        /// <returns>A boolean value of whether or not the file exists.</returns>
        public static bool FileExists(FileInfo fileInfo) {
            if (string.IsNullOrEmpty(fileInfo.FullName))
                return false;

            if (!fileInfo.Exists)
                return false;

            return true;
        }

        /// <summary>
        /// Reads the range header value to create a start- and end value to read from a file.
        /// </summary>
        /// <param name="range">The range header value.</param>
        /// <param name="contentLength">The total lenght of the content.</param>
        /// <param name="start">The calculated start value.</param>
        /// <param name="end">The calculated end value.</param>
        /// <returns>A boolean if the reading was successful.</returns>
        public static bool TryReadRangeItem(RangeItemHeaderValue range, long contentLength, out long start, out long end) {
            if (range.From != null) {
                start = range.From.Value;
                if (range.To != null) {
                    end = range.To.Value;
                } else {
                    end = contentLength - 1;
                }
            } else {
                end = contentLength - 1;
                if (range.To != null) {
                    start = contentLength - range.To.Value;
                } else {
                    start = 0;
                }
            }
            return (start < contentLength && end < contentLength);
        }

        /// <summary>
        /// Creates partial content of the inputStream using the start- and end value. The resulting partial content
        /// will be written to the given outputStream.
        /// </summary>
        /// <param name="inputStream">Input stream to read from.</param>
        /// <param name="outputStream">Output stream to write the partial content to.</param>
        /// <param name="start">The start byte position for the partial content.</param>
        /// <param name="end">The end byte position for the partial content.</param>
        public static void CreatePartialContent(Stream inputStream, Stream outputStream, long start, long end) {
            int count = 0;
            long remainingBytes = end - start + 1;
            long position = start;
            byte[] buffer = new byte[Settings.ReadStreamBufferSize];

            inputStream.Position = start;
            do {
                try {
                    if (remainingBytes > Settings.ReadStreamBufferSize) {
                        count = inputStream.Read(buffer, 0, Settings.ReadStreamBufferSize);
                    } else {
                        count = inputStream.Read(buffer, 0, (int)remainingBytes);
                    }

                    outputStream.Write(buffer, 0, count);
                } catch (Exception error) {
                    Debug.WriteLine(error);
                    break;
                }
                position = inputStream.Position;
                remainingBytes = end - position + 1;
            } while (position <= end);
        }
    }
}
