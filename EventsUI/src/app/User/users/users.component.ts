import { Component, OnInit } from "@angular/core";
import { ApiService } from "src/app/api.service";
import { Router, NavigationEnd } from "@angular/router";

@Component({
  selector: "app-users",
  templateUrl: "./users.component.html",
  styleUrls: ["./users.component.css"]
})
export class UsersComponent implements OnInit {
  users: any = [];
  url: string;
  url2: string;

  mySubscription: any;

  constructor(private api: ApiService, private router: Router) {
    this.url = "users";
  }

  getAllUsers(): void {
    this.api.getUrl(this.url).subscribe(data => {
      this.users = data;
    });
  }

  //need to reload html after button click. no solutions yet.
  banOrUnbanUser(userId: number) {
    this.api.patchUrl("users/" + userId).subscribe(
      res => {
        console.log("received ok response from patch request");
      },
      error => {
        console.error("There was an error during the request");
        console.log(error);
      }
    );
  }

  ngOnInit() {
    this.getAllUsers();
  }
}
