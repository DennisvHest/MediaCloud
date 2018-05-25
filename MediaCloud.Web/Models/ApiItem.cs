using System;
using System.Collections.Generic;
using System.Linq;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiItem {

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string PosterPath { get; set; }
    public string BackdropPath { get; set; }
    public string ItemType { get; set; }

    public IEnumerable<ApiGenre> Genres { get; set; }

    public ApiItem(Item item) {
      Id = item.Id;
      Title = item.Title;
      Description = item.Description;
      ReleaseDate = item.ReleaseDate;
      PosterPath = item.PosterPath;
      BackdropPath = item.BackdropPath;

      ItemType = item is Movie ? typeof(Movie).Name : typeof(Series).Name;

      Genres = item.ItemGenres.Select(ig => new ApiGenre(ig.Genre));
    }
  }
}
