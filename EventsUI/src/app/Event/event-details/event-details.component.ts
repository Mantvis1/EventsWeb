import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { ApiService } from "../../api.service";

@Component({
  selector: "app-event-details",
  templateUrl: "./event-details.component.html",
  styleUrls: ["./event-details.component.css"]
})
export class EventDetailsComponent implements OnInit {
  id: any;
  event: any;
  url: any;

  constructor(private activatedRoute: ActivatedRoute, private api: ApiService) {
    this.id = this.activatedRoute.snapshot.params.id;
    this.url = "events/" + this.id;
  }

  getEventById(): void {
    this.api.getUrl(this.url).subscribe(data => {
      this.event = { title: data.title, id: data.id, summary: data.summary };
    });
  }

  ngOnInit() {
    this.getEventById();
  }
}
