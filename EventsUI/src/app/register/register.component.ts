import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ApiService } from "../api.service";
import { FormGroup, FormBuilder } from "@angular/forms";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit {
  error: string;
  form: FormGroup;

  constructor(
    private router: Router,
    private api: ApiService,
    public fb: FormBuilder
  ) {
    this.form = this.fb.group({
      name: [""],
      password: [""],
      repeatPassword: [""],
      email: [""]
    });
  }

  register() {
    if (
      this.form.value.name.length == 0 ||
      this.form.value.password.length == 0 ||
      this.form.value.email.length == 0
    ) {
      this.showError("vardas,el. paštas ir slaptazodis negali buti tusti");
    } else if (this.form.value.password == this.form.value.repeatPassword) {
      this.addNewUser();
    } else {
      this.showError("Slaptazodziai nesutampa");
    }
  }

  redirectToLogIn() {
    this.router.navigate(["/login"]);
  }

  addNewUser() {
    this.api
      .postUrl("auth/register", {
        name: this.form.value.name,
        password: this.form.value.password,
        email: this.form.value.email
      })
      .subscribe(
        res => {
          console.log("received ok response from patch request");
        },
        error => {
          console.error("There was an error during the request");
          console.log(error);
        }
      );
    this.redirectToLogIn();
  }
  showError(message: string) {
    this.error = message;
  }

  ngOnInit() {}
}
