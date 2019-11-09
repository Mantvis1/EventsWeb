import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { PageNotFoundComponent } from "./page-not-found/page-not-found.component";
import { EventsComponent } from "./Event/events/events.component";
import { EventDetailsComponent } from "./Event/event-details/event-details.component";
import { HomeComponent } from "./home/home.component";
import { UsersComponent } from "./User/users/users.component";
import { UserDetailsComponent } from "./User/user-details/user-details.component";
import { MyEventsComponent } from "./Event/my-events/my-events.component";
import { EditEventComponent } from "./Event/edit-event/edit-event.component";
import { NewEventComponent } from "./Event/new-event/new-event.component";

const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "register", component: RegisterComponent },
  { path: "events", component: EventsComponent },
  { path: "event/:id", component: EventDetailsComponent },
  { path: "myEvents", component: MyEventsComponent },
  { path: "event/edit/:id", component: EditEventComponent },
  { path: "new", component: NewEventComponent },
  { path: "users", component: UsersComponent },
  { path: "user/:id", component: UserDetailsComponent },
  { path: "", component: HomeComponent },
  { path: "**", component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
