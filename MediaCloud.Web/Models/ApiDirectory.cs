using System.IO;

namespace MediaCloud.Web.Models {

  public class ApiDirectory {

    public string Path { get; set; }
    public string Name { get; set; }

    public ApiDirectory(DriveInfo drive) {
      Name = drive.Name;
    }

    public ApiDirectory(DirectoryInfo directory) {
      Path = directory.FullName;
      Name = directory.Name;
    }
  }
}
