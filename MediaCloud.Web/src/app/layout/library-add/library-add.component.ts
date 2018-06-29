import { Component, OnInit, ViewChild } from '@angular/core';
import { Directory } from '../../models/directory';
import { DirectoryService } from '../directory.service';
import { LibraryService } from '../../libraries/library.service';
import { NgModel, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'mc-library-add',
  templateUrl: './library-add.component.html',
  styleUrls: ['./library-add.component.css']
})
export class LibraryAddComponent implements OnInit {

  drives: Directory[];
  directories: Directory[];

  name: string;
  selectedLibraryType = 0;
  selectedDrive: Directory;
  selectedDirectory: Directory;
  selectedDirectoryPath: string;

  prevDirectories: Directory[];

  showDirectoriesLoader = true;
  submitting = false;
  progress: string;

  @ViewChild('selectedDirectoryPathInput') selectedDirectoryPathInput: NgModel;
  @ViewChild('nameInput') nameInput: NgModel;

  private _hubConnection: HubConnection | undefined;

  constructor(
    private directoryService: DirectoryService,
    private libraryService: LibraryService,
    private router: Router
  ) { }

  ngOnInit() {
    this.drives = [];
    this.directories = [];
    this.prevDirectories = [];

    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/hubs/library')
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this._hubConnection.on('progressReport', (data: any) => {
      console.log(data);
      this.progress = data.progressPercentage + '%';
    });

    this._hubConnection.on('libraryCreated', (data: any) => {
      console.log(data);
      this.router.navigate(['libraries', data.libraryId]);
    });

    this._hubConnection.start();

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
    this.prevDirectories = [];

    this.selectedDrive = drive;
    this.selectedDirectory = null;
    this.selectedDirectoryPath = this.selectedDrive.name;
    this.updateDirectoryView(drive.name);
  }

  onDirectorySelect(directory: Directory) {
    this.prevDirectories.push(this.selectedDirectory ? this.selectedDirectory : this.selectedDrive);

    this.selectedDirectory = directory;
    this.selectedDirectoryPath = this.selectedDirectory.path;
    this.updateDirectoryView(directory.path);
  }

  onBackDirectory() {
    const prevDirectory = this.prevDirectories.pop();

    if (this.prevDirectories.length === 0) {
      this.selectedDirectory = null;
      this.selectedDirectoryPath = prevDirectory.name;
    } else {
      this.selectedDirectory = prevDirectory;
      this.selectedDirectoryPath = prevDirectory.path;
    }

    this.updateDirectoryView(prevDirectory.path ? prevDirectory.path : prevDirectory.name);
  }

  createLibrary(form: NgForm) {
    if (form.valid) {
      this.submitting = true;
      this._hubConnection.invoke('Create', this.selectedLibraryType, this.name, this.selectedDirectory.path);
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
