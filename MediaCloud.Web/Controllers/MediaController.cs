using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MediaCloud.Common;
using MediaCloud.Common.Helpers;
using MediaCloud.Domain.Entities;
using MediaCloud.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/[controller]")]
  public class MediaController : Controller {

    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService) {
      _mediaService = mediaService;
    }

    [HttpGet("{id}/stream")]
    public async Task<HttpResponseMessage> Stream(int id) {
      Media media = await _mediaService.Get(id);

      FileInfo fileInfo = new FileInfo(media.FileLocation);

      HttpResponseMessage response = new HttpResponseMessage();

      if (!FileHelper.FileExists(fileInfo)) {
        response.StatusCode = HttpStatusCode.NotFound;
        return response;
      }

      string rangeHeader = Request.Headers["range"];

      response.Headers.AcceptRanges.Add("bytes");

      // The request will be treated as normal request if there is no Range header.
      if (rangeHeader == null) {
        response.StatusCode = HttpStatusCode.OK;
        response.Content = new PushStreamContent((outputStream, httpContent, transpContext) => {
          using (outputStream) // Copy the file to output stream straightforward. 
          using (Stream inputStream = fileInfo.OpenRead()) {
            try {
              inputStream.CopyTo(outputStream, Settings.ReadStreamBufferSize);
            } catch (Exception error) {
              Debug.WriteLine(error);
            }
          }
        }, "video/mp4");

        response.Content.Headers.ContentLength = fileInfo.Length;
        return response;
      }

      string[] range = rangeHeader.Split('=')[1].Split("-");
      long? from = null;
      long? to = null;

      try {
        from = long.Parse(range[0]);
      } catch (Exception e) {}

      try {
        to = long.Parse(range[1]);
      } catch (Exception e) { }

      RangeItemHeaderValue rangeItemHeader = new RangeItemHeaderValue(from, to);

      long start = 0, end = 0;

      // 1. If the unit is not 'bytes'.
      // 2. If there are multiple ranges in header value.
      // 3. If start or end position is greater than file length.
//      if (rangeHeader.Unit != "bytes" || rangeHeader.Ranges.Count > 1 ||
//          !TryReadRangeItem(rangeHeader.Ranges.First(), totalLength, out start, out end)) {
//        response.StatusCode = HttpStatusCode.RequestedRangeNotSatisfiable;
//        response.Content = new StreamContent(Stream.Null);  // No content for this status.
//        response.Content.Headers.ContentRange = new ContentRangeHeaderValue(totalLength);
//        response.Content.Headers.ContentType = GetMimeNameFromExt(fileInfo.Extension);
//
//        return response;
//      }

      if (!FileHelper.TryReadRangeItem(rangeItemHeader, fileInfo.Length, out start, out end)) {

      }

      ContentRangeHeaderValue contentRange = new ContentRangeHeaderValue(start, end, fileInfo.Length);

      // We are now ready to produce partial content.
      response.StatusCode = HttpStatusCode.PartialContent;
      response.Content = new PushStreamContent((outputStream, httpContent, transpContext) => {
        using (outputStream) // Copy the file to output stream in indicated range.
        using (Stream inputStream = fileInfo.OpenRead())
          FileHelper.CreatePartialContent(inputStream, outputStream, start, end);

      }, "video/mp4");

      response.Content.Headers.ContentLength = end - start + 1;
      response.Content.Headers.ContentRange = contentRange;

      return response;
    }
  }
}
