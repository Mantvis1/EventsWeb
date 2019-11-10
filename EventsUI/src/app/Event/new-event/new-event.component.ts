import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-new-event",
  templateUrl: "./new-event.component.html",
  styleUrls: ["./new-event.component.css"]
})
export class NewEventComponent implements OnInit {
  constructor() {}

  newEvent(title: string, summary: string) {
    console.log(title + " " + summary);
  }

  ngOnInit() {}
}
