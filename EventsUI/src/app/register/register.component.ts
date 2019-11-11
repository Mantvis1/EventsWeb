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

  register(name: string, pass: string, passRepeat: string, email: string) {
    if (name.length == 0 || pass.length == 0) {
      this.showError("vardas ir slaptazodis negali buti tusti");
    }
    if (pass == passRepeat) {
      this.addNewUser(name, pass, email);
    }
    this.showError("Slaptazodziai nesutampa");
  }

  redirectToLogIn() {
    this.router.navigate(["/login"]);
  }

  addNewUser(name: string, password: string, email: string) {
    this.api
      .postUrl("register", {
        name: name,
        password: password,
        email: email
      })
      .subscribe();
    this.redirectToLogIn();
  }
  showError(message: string) {
    this.error = "";
  }

  submitForm() {
    console.log(this.form);
  }
  ngOnInit() {}
}
