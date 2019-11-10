import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit {
  error: string;

  constructor(private router: Router) {}

  validateUserInfo(name: string, pass: string): void {
    if (this.nameAndPassValidation(name, pass)) {
      this.router.navigate(["user/5"]);
    }
    this.showError();
  }

  nameAndPassValidation(name: string, pass: string): boolean {
    return true;
  }

  showError() {
    this.error = "User not found";
  }
  ngOnInit() {}
}
