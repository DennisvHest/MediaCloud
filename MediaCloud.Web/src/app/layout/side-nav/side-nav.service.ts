import { Output, Injectable, EventEmitter } from "@angular/core";

@Injectable()
export class SideNavService {

    isOpen = false;

    @Output() change: EventEmitter<boolean> = new EventEmitter();

    open() {
        this.isOpen = true;
        this.change.emit(this.isOpen);
    }

    close() {
        this.isOpen = false;
        this.change.emit(this.isOpen);
    }

    toggle() {
        this.isOpen = !this.isOpen;
        this.change.emit(this.isOpen);
    }
}