import { Component, OnInit } from "@angular/core";
import { ApiService } from "src/app/api.service";

@Component({
  selector: "app-my-events",
  templateUrl: "./my-events.component.html",
  styleUrls: ["./my-events.component.css"]
})
export class MyEventsComponent implements OnInit {
  events: any = [];
  url: any;

  constructor(private api: ApiService) {
    this.url = "userEvents/1008";
  }

  getEvents(): void {
    this.api.getUrl(this.url).subscribe(data => {
      for (const d of data as any) {
        this.events.push({ id: d.eventId, title: d.title });
      }
    });
  }

  ngOnInit() {
    this.getEvents();
  }
}
