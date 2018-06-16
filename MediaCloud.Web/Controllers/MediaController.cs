using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MediaCloud.Common;
using MediaCloud.Common.Helpers;
using MediaCloud.Common.Models;
using MediaCloud.Domain.Entities;
using MediaCloud.Services;
using MediaCloud.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/[controller]")]
  public class MediaController : Controller {

    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService) {
      _mediaService = mediaService;
    }

    [HttpGet("{id}")]
    public async Task<ApiMedia> Get(int id) {
      return new ApiMedia(await _mediaService.Get(id), true);
    }

    [HttpGet("{id}/stream")]
    public async Task<HttpResponseMessage> Stream(int id) {
      Media media = await _mediaService.Get(id);

      HttpResponseMessage response = new HttpResponseMessage();

      //Check if media exists
      if (media == null) {
        response.StatusCode = HttpStatusCode.NotFound;
        return response;
      }

      FileInfo fileInfo = new FileInfo(media.FileLocation);

      if (!FileHelper.FileExists(fileInfo)) {
        response.StatusCode = HttpStatusCode.NotFound;
        return response;
      }

      string rangeHeaderString = Request.Headers["range"];

      response.Headers.AcceptRanges.Add("bytes");

      // The request will be treated as normal request if there is no Range header.
      if (rangeHeaderString == null) {
        CreateNoRangeResponse(response, fileInfo);
        return response;
      }

      RangeHeader rangeHeader = StreamHelper.ParseRangeHeader(rangeHeaderString);

      //If the range is not satisfiable, return a RequestedRangeNotSatisfiable response
      if (rangeHeader.Unit != "bytes" || /*rangeHeader.Ranges.Count > 1 ||*/
          !FileHelper.TryReadRangeItem(rangeHeader.Value, fileInfo.Length, out long start, out long end)) {
        CreateRangeNotSatisfiableResponse(response, fileInfo);
        return response;
      }

      //The range header is fine, return partial content
      CreatePartialContentResponse(response, fileInfo, start, end);
      return response;
    }

    private void CreateNoRangeResponse(HttpResponseMessage response, FileInfo fileInfo) {
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
    }

    private void CreateRangeNotSatisfiableResponse(HttpResponseMessage response, FileInfo fileInfo) {
      response.StatusCode = HttpStatusCode.RequestedRangeNotSatisfiable;
      response.Content = new StreamContent(System.IO.Stream.Null);
      response.Content.Headers.ContentRange = new ContentRangeHeaderValue(fileInfo.Length);
      response.Content.Headers.ContentType = new MediaTypeHeaderValue("video/mp4");
    }

    private void CreatePartialContentResponse(HttpResponseMessage response, FileInfo fileInfo, long start, long end) {
      ContentRangeHeaderValue contentRange = new ContentRangeHeaderValue(start, end, fileInfo.Length);

      response.StatusCode = HttpStatusCode.PartialContent;
      response.Content = new PushStreamContent((outputStream, httpContent, transpContext) => {
        using (outputStream)
        using (Stream inputStream = fileInfo.OpenRead())
          FileHelper.CreatePartialContent(inputStream, outputStream, start, end);

      }, "video/mp4");

      response.Content.Headers.ContentLength = end - start + 1;
      response.Content.Headers.ContentRange = contentRange;
    }
  }
}
