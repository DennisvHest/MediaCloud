using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;

namespace MediaCloud.Common.Helpers {

    public static class FileHelper {

        public static bool FileExists(FileInfo fileInfo) {
            if (string.IsNullOrEmpty(fileInfo.FullName))
                return false;

            if (!fileInfo.Exists)
                return false;

            return true;
        }

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
