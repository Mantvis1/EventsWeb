import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { PageNotFoundComponent } from "./page-not-found/page-not-found.component";
import { EventsComponent } from "./events/events.component";
import { EventDetailsComponent } from "./event-details/event-details.component";
import { HomeComponent } from "./home/home.component";
import { UsersComponent } from "./users/users.component";
import { UserDetailsComponent } from "./user-details/user-details.component";

const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "register", component: RegisterComponent },
  { path: "events", component: EventsComponent },
  { path: "event/:id", component: EventDetailsComponent },
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
