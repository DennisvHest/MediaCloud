import { Component, OnInit, ViewChild } from '@angular/core';
import { Directory } from '../../models/directory';
import { DirectoryService } from '../directory.service';
import { LibraryService } from '../../libraries/library.service';
import { NgModel, NgForm } from '@angular/forms';
import { Router, NavigationStart } from '@angular/router';
import { SideNavComponent } from '../side-nav/side-nav.component';

@Component({
  selector: 'mc-library-add',
  templateUrl: './library-add.component.html',
  styleUrls: ['./library-add.component.css']
})
export class LibraryAddComponent implements OnInit {

  drives: Directory[];
  directories: Directory[];

  name: string;
  selectedLibraryType: number = 0;
  selectedDrive: Directory;
  selectedDirectory: Directory;
  selectedDirectoryPath: string;

  showDirectoriesLoader = true;
  submitting = false;

  @ViewChild("selectedDirectoryPathInput") selectedDirectoryPathInput: NgModel;
  @ViewChild("nameInput") nameInput: NgModel;

  constructor(
    private directoryService: DirectoryService,
    private libraryService: LibraryService,
    private router: Router
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
              this.showDirectoriesLoader = false;
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
    this.selectedDirectoryPath = this.selectedDrive.name;
    this.updateDirectoryView(drive.name);
  }

  onDirectorySelect(directory: Directory) {
    this.selectedDirectory = directory;
    this.selectedDirectoryPath = this.selectedDirectory.path;
    this.updateDirectoryView(directory.path);
  }

  createLibrary(form: NgForm) {
    if (form.valid) {
      this.submitting = true;
      this.libraryService.create(this.name, this.selectedLibraryType, this.selectedDirectory.path)
        .subscribe((createdResponse) => {
          this.libraryService.libraryUpdated.next(true);
          this.submitting = false;
          this.router.navigate(['libraries', createdResponse.id]);
        });
    }
  }

  private updateDirectoryView(path: string) {
    this.showDirectoriesLoader = true;
    this.directoryService.getSubDirectories(path)
      .subscribe(directories => {
        this.showDirectoriesLoader = false;
        this.directories = directories;
      });
  }
}
