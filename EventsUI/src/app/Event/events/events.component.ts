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
      for (const d of data as any) {
        this.events.push({ id: d.id, title: d.title });
      }
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
