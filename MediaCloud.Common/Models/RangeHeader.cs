using System.Net.Http.Headers;

namespace MediaCloud.Common.Models {

    public class RangeHeader {

        public string Unit { get; set; }
        public RangeItemHeaderValue Value { get; set; }
    }
}
