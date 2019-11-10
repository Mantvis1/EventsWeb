import { Component, OnInit } from "@angular/core";
import { ApiService } from "src/app/api.service";

@Component({
  selector: "app-users",
  templateUrl: "./users.component.html",
  styleUrls: ["./users.component.css"]
})
export class UsersComponent implements OnInit {
  users: any = [];
  url: string;
  url2: string;

  constructor(private api: ApiService) {
    this.url = "users";
  }

  getAllUsers(): void {
    this.api.getUrl(this.url).subscribe(data => {
      this.users = data;
    });
  }

  banOrUnbanUser(userId: number) {
    this.api.patchUrl("users/" + 1008).subscribe(
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
