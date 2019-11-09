import { User } from "../../User/shared/user.model";

export class Event {
  id: number;
  title: string;
  summary: string;
  user: string;

  constructor(id: number, title: string, summary: string, user: string) {
    this.id = id;
    this.title = title;
    this.summary = summary;
    this.user = user;
  }

  getTitle(): string {
    return this.title;
  }

  getId(): number {
    return this.id;
  }
  getUser(): string {
    return this.user;
  }

  getSummary(): string {
    return this.summary;
  }
}
