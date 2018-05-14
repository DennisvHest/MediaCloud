using System;
using System.Net.Http.Headers;
using MediaCloud.Common.Models;

namespace MediaCloud.Common.Helpers {

    public static class StreamHelper {

        public static RangeHeader ParseRangeHeader(string header) {
            string[] rangeHeader = header.Split('=');
            string[] rangeHeaderValues = rangeHeader[1].Split('-');

            long? from = null;
            long? to = null;

            try {
                from = long.Parse(rangeHeaderValues[0]);
            } catch (Exception e) { }

            try {
                to = long.Parse(rangeHeaderValues[1]);
            } catch (Exception e) { }

            return new RangeHeader {
                Unit = rangeHeader[0],
                Value = new RangeItemHeaderValue(from, to)
            };
        }
    }
}
