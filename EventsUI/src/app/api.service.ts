import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

const url = "https://localhost:44349/api/";

@Injectable({
  providedIn: "root"
})
export class ApiService {
  constructor(private http: HttpClient) {}

  getUrl(end: string) {
    return this.http.get(url + end);
  }

  patchUrl(end: string) {
    return this.http.patch(url + end, null);
  }
}
