import { Component, OnInit } from "@angular/core";
import { ApiService } from "../../api.service";
@Component({
  selector: "app-events",
  templateUrl: "./events.component.html",
  styleUrls: ["./events.component.css"]
})
export class EventsComponent implements OnInit {
  events: any = [];

  constructor(private api: ApiService) {}

  getEvents(): void {
    this.api.getUrl("events").subscribe(data => {
      this.events = data;
    });
  }

  clearDataArray(): void {
    this.events.length = 0;
  }

  ngOnInit() {
    this.clearDataArray();
    this.getEvents();
  }
}
