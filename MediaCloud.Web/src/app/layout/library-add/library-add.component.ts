import { Component, OnInit } from '@angular/core';
import { Directory } from '../../models/directory';
import { DirectoryService } from '../directory.service';
import { LibraryService } from '../../libraries/library.service';

@Component({
  selector: 'mc-library-add',
  templateUrl: './library-add.component.html',
  styleUrls: ['./library-add.component.css']
})
export class LibraryAddComponent implements OnInit {

  drives: Directory[];
  directories: Directory[];

  name: string;
  selectedLibraryType: number;
  selectedDrive: Directory;
  selectedDirectory: Directory;

  constructor(
    private directoryService: DirectoryService,
    private libraryService: LibraryService
  ) { }

  ngOnInit() {
    this.drives = [];
    this.directories = [];

    this.directoryService.getDrives()
      .subscribe(drives => {
        this.drives = drives;

        if (drives.length > 0) {
          this.selectedDrive = this.drives[0];

          this.directoryService.getSubDirectories(this.drives[0].name)
            .subscribe(directories => {
              this.directories = directories;
            });
        }
      });
  }

  onLibraryTypeSelect(libraryType: number) {
    this.selectedLibraryType = libraryType;
  }

  onDriveSelect(drive: Directory) {
    this.selectedDrive = drive;
    this.selectedDirectory = null;
    this.updateDirectoryView(drive.name);
  }

  onDirectorySelect(directory: Directory) {
    this.selectedDirectory = directory;
    this.updateDirectoryView(directory.path);
  }

  createLibrary() {
    this.libraryService.create(this.name, this.selectedLibraryType, this.selectedDirectory.path)
      .subscribe(() => {
        this.libraryService.libraryUpdated.next(true);
      });
  }

  private updateDirectoryView(path: string) {
    this.directoryService.getSubDirectories(path)
    .subscribe(directories => {
      this.directories = directories;
    });
  }
}
