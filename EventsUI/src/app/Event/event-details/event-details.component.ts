import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { ApiService } from "../../api.service";
import { Event } from "./../shared/event.model";

@Component({
  selector: "app-event-details",
  templateUrl: "./event-details.component.html",
  styleUrls: ["./event-details.component.css"]
})
export class EventDetailsComponent implements OnInit {
  id: any;
  event: Event;
  url: any;

  constructor(private activatedRoute: ActivatedRoute, private api: ApiService) {
    this.id = this.activatedRoute.snapshot.params.id;
    this.url = "events/" + this.id;
  }

  getEventById(): void {
    this.api.getUrl(this.url).subscribe(data => {
      const d = data as any;
      const name = function(): string {
        let result;
        this.name.length = 0;
        this.api.getUrl("users/" + d.createdBy).subscribe(userInfo => {
          result = userInfo.name;
        });
        return result;
      };
      this.event = new Event(
        d.id,
        d.title,
        d.summary,
        name.prototype.constructor.name
      );
    });
  }

  ngOnInit() {
    this.getEventById();
  }
}
