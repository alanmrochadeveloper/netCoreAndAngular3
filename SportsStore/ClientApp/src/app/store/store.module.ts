
import { NgModule, NO_ERRORS_SCHEMA } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { CartSummaryComponent } from "./cartSummary.component";
import { CategoryFilterComponent } from "./categoryFilter.component";
import { PaginationComponent } from "./pagination.component";
import { ProductListComponent } from "./productList.component";
import { RatingsComponent } from "./ratings.component";
import { ProductSelectionComponent } from "./productSelection.component";
import { CartDetailComponent } from "./cartDetail.component";
import { FormsModule } from "@angular/forms";//for [(ngModel)] directive insite input element
import { RouterModule } from "@angular/router"; //for routerLink
import {CheckoutDetailsComponent } from "./checkout/checkoutDetails.component";
import { CheckoutPaymentComponent} from "./checkout/checkoutPayment.component";
import {CheckoutSummaryComponent} from "./checkout/checkoutSummary.component";
import {OrderConfirmationComponent} from "./checkout/orderConfirmation.component";
import { BlazorLoader } from "./blazorLoader.component";

@NgModule({
    declarations: [CartSummaryComponent, CategoryFilterComponent, PaginationComponent, ProductListComponent, RatingsComponent, ProductSelectionComponent, CartDetailComponent,
         CheckoutDetailsComponent, CheckoutPaymentComponent, CheckoutSummaryComponent, OrderConfirmationComponent, BlazorLoader ],
    imports: [BrowserModule, FormsModule, RouterModule], // which Module it depends on which provide default directives and data bindings required for an angular application running a Browser
    exports: [ProductSelectionComponent], // which one will be exported to manage all declarations
    schemas: [NO_ERRORS_SCHEMA]
})

export class StoreModule {}
