import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit {
  error: string;

  constructor(private router: Router) {}

  register(name: string, pass: string, passRepeat: string) {
    if (pass == passRepeat) {
      this.addNewUser(name, pass);
    }
    this.showError();
  }

  redirectToLogIn() {
    this.router.navigate(["/login"]);
  }

  addNewUser(name: string, password: string) {
    console.log(name + " " + password);
    this.redirectToLogIn();
  }
  showError() {
    this.error = "Slaptazodziai nesutampa";
  }
  ngOnInit() {}
}
