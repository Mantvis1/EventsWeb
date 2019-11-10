import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-edit-event",
  templateUrl: "./edit-event.component.html",
  styleUrls: ["./edit-event.component.css"]
})
export class EditEventComponent implements OnInit {
  eventId: number;

  constructor(private activatedRoute: ActivatedRoute) {
    this.eventId = this.activatedRoute.snapshot.params.id;
  }

  deleteEvent() {
    console.log("renginyes istrintas " + this.eventId);
  }

  editEvent(title: string, summary: string) {
    console.log(title + " " + summary);
  }

  ngOnInit() {}
}
