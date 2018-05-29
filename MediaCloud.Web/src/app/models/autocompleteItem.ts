import { Select2OptionData } from "ng2-select2";

export class AutocompleteItem implements Select2OptionData {
    id: string;
    text: string;
    disabled?: boolean;
    children?: Select2OptionData[];
    additional?: any;
}