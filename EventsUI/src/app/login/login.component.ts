import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ApiService } from "../api.service";
import { FormBuilder, Form, FormGroup } from "@angular/forms";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit {
  error: string;
  loginForm: FormGroup;
  userId: number;
  users: any = [];

  constructor(
    private router: Router,
    private api: ApiService,
    private fb: FormBuilder
  ) {
    this.loginForm = fb.group({ name: [""], password: [""] });
  }

  login(): void {
    if (this.checkId()) {
      this.router.navigate(["user/" + this.userId]);
    } else {
      this.showError();
    }
  }

  nameAndPassValidation(): void {
    this.api.getUrl("users").subscribe(
      res => {
        this.users = res;
      },
      error => {
        console.error("There was an error during the request");
        console.log(error);
      }
    );
  }

  checkId(): boolean {
    console.log(this.users);
    for (const d of this.users as any) {
      if (
        d.name == this.loginForm.value.name &&
        d.password == this.loginForm.value.password
      ) {
        this.userId = d.id;
        return true;
      }
    }
    return false;
  }

  showError() {
    this.error = "User not found";
  }
  ngOnInit() {
    this.nameAndPassValidation();
  }
}
