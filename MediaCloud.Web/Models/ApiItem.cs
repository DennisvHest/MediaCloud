using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiItem {

    public int Id { get; set; }
    public string Title { get; set; }

    public ApiItem(Item item) {
      Id = item.Id;
      Title = item.Title;
    }
  }
}
