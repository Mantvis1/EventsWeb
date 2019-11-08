import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { PageNotFoundComponent } from "./page-not-found/page-not-found.component";
import { EventsComponent } from "./Event/events/events.component";
import { EventDetailsComponent } from "./Event/event-details/event-details.component";
import { UsersComponent } from "./User/users/users.component";
import { UserDetailsComponent } from "./User/user-details/user-details.component";
import { HomeComponent } from "./home/home.component";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    PageNotFoundComponent,
    EventsComponent,
    EventDetailsComponent,
    UsersComponent,
    UserDetailsComponent,
    HomeComponent
  ],
  imports: [BrowserModule, HttpClientModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
