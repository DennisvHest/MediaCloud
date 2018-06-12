using System;
using System.Net.Http.Headers;
using MediaCloud.Common.Models;

namespace MediaCloud.Common.Helpers {

    public static class StreamHelper {

        /// <summary>
        /// Parses the range header value from an incoming request to a RangeHeader object.
        /// </summary>
        /// <param name="header">The range header value of the request.</param>
        /// <returns>A RangeHeader object created using the header value.</returns>
        public static RangeHeader ParseRangeHeader(string header) {
            string[] rangeHeader = header.Split('=');
            string[] rangeHeaderValues = rangeHeader[1].Split('-');

            long? from = null;
            long? to = null;

            try {
                from = long.Parse(rangeHeaderValues[0]);
            } catch (Exception) { }

            try {
                to = long.Parse(rangeHeaderValues[1]);
            } catch (Exception) { }

            return new RangeHeader {
                Unit = rangeHeader[0],
                Value = new RangeItemHeaderValue(from, to)
            };
        }
    }
}
