import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "./base.service";
import { VoteCountModel } from "../model/vote.model";

@Injectable({
  providedIn: 'root'
})
export class VoteService extends BaseService {
  constructor() {
    super()
  }

  Add(voteCountModel: VoteCountModel): Observable<number> {
    return this.http.post<number>(`${this.apiBaseUrl}VoteCount`, voteCountModel);
  }

}
