import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UserModel } from "../model/user.model";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {
  constructor() {
    super();
  }

  GetAll(): Observable<Array<UserModel>> {
    return this.http.get<Array<UserModel>>(`${this.apiBaseUrl}User`);
  }

  Add(userModel: UserModel): Observable<UserModel> {
    return this.http.post<UserModel>(`${this.apiBaseUrl}User`, userModel);
  }

  Update(userModel: UserModel): Observable<UserModel> {
    return this.http.put<UserModel>(`${this.apiBaseUrl}User`, userModel);
  }

  Delete(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiBaseUrl}User/` + id);
  }
}
