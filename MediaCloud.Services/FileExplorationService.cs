using System.Collections.Generic;
using System.IO;

namespace MediaCloud.Services {

    public interface IFileExplorationService {
        IEnumerable<DriveInfo> GetDrives();
        IEnumerable<DirectoryInfo> GetSubDirectories(string path);
    }

    public class FileExplorationService : IFileExplorationService {

        public IEnumerable<DriveInfo> GetDrives() {
            return DriveInfo.GetDrives();
        }

        public IEnumerable<DirectoryInfo> GetSubDirectories(string path) {
            DirectoryInfo directories = new DirectoryInfo(path);

            return directories.GetDirectories();
        }
    }
}
