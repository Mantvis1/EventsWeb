import { Component, OnInit } from "@angular/core";
import { ApiService } from "src/app/api.service";
import { ActivatedRoute } from "@angular/router";
import { FormBuilder, FormGroup } from "@angular/forms";

@Component({
  selector: "app-user-details",
  templateUrl: "./user-details.component.html",
  styleUrls: ["./user-details.component.css"]
})
export class UserDetailsComponent implements OnInit {
  id: number;
  userInfo: any;
  userDataForm: FormGroup;

  constructor(
    private api: ApiService,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder
  ) {
    this.id = this.activatedRoute.snapshot.params.id;
    this.userDataForm = this.fb.group({
      name: [""],
      password: [""]
    });
  }

  getUserInfo() {
    this.api.getUrl("users/" + this.id).subscribe(data => {
      this.userInfo = data;
    });
  }

  postUserInfo() {}

  deleteAccount() {
    this.api.deleteUrl("users/" + this.id).subscribe(null, error => {
      console.error("There was an error during the request");
      console.log(error);
    });
  }

  ngOnInit() {
    this.getUserInfo();
  }
}
