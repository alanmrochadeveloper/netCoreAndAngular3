import { Component} from "@angular/core";
import { NavigationService } from "../models/navigation.service";
//import {Repository} from "../models/repository";

@Component({
    selector: "store-categoryfilter",
    templateUrl: "categoryFilter.component.html"
})
export class CategoryFilterComponent{
    constructor(public service: NavigationService){
        
    }
}