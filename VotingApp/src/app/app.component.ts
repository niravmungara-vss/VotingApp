import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserService } from '../service/user.service';
import { UserModel, UserTypes } from '../model/user.model';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { VoteService } from '../service/vote.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, CommonModule, ToastrModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  public readonly fb = inject(FormBuilder);

  title = 'Voting App';
  lstUser: Array<UserModel> | null = [];
  lstCandidates: Array<UserModel> | null = [];
  lstVoters: Array<UserModel> | null = [];

  isAddNewVoter: boolean = false;
  VoterName: string = '';

  isAddNewCandidate: boolean = false;
  CandidateName: string = '';

  public fg: FormGroup = {} as any;
  public fgIniValue: any;

  constructor(private userService: UserService,
    private toastr: ToastrService,
    private voteService: VoteService
  ) {
    this.fg = this.fb.group({
      VoterUserId: [0, Validators.required],
      CandidateUserId: [0, Validators.required]
    });
    this.fgIniValue = this.fg.value;
  }

  ngOnInit(): void {
    this.bindUser();
  }

  bindUser() {
    this.userService.GetAll().subscribe(respo => {
      this.lstUser = respo;
      this.lstCandidates = this.lstUser.filter(t => t.UserType == UserTypes.Candidates);
      this.lstVoters = this.lstUser.filter(t => t.UserType == UserTypes.Voters);
    })
  }

  saveVoter() {
    if (this.VoterName) {
      this.userService.Add({ Name: this.VoterName, UserType: UserTypes.Voters }).subscribe({
        next: respo => {
          if (respo.Id) {
            this.lstUser?.push(respo);
            this.lstVoters?.push(respo);
            this.VoterName = '';
            this.isAddNewVoter = false;
            this.toastr.success('Voter Save Successfully');
          }
        }, error: (error: any) => {
          this.toastr.error('Voter name already exist');
        }
      })
    } else {
      this.toastr.error('Please enter Voter Name');
    }
  }

  saveCandidate() {
    if (this.CandidateName) {
      this.userService.Add({ Name: this.CandidateName, UserType: UserTypes.Candidates }).subscribe({
        next: respo => {
          if (respo.Id) {
            this.lstUser?.push(respo);
            this.lstCandidates?.push(respo);
            this.CandidateName = '';
            this.isAddNewCandidate = false;
            this.toastr.success('Candidate Save Successfully');
          }
        }, error: (error: any) => {
          this.toastr.error('Candidate name already exist');
        }
      })
    } else {
      this.toastr.error('Please enter Candidate Name');
    }
  }

  saveVote() {
    const reqModel = this.fg.getRawValue();
    if (reqModel.VoterUserId && reqModel.CandidateUserId) {
      this.voteService.Add(reqModel).subscribe({
        next: (value) => {
          this.bindUser();
          this.fg.reset(this.fgIniValue);
          this.toastr.success('Thank you for your Vote.');
        }, error: (error: any) => {
          this.toastr.error('The voter already voted for this candidate.');
        }
      })
    } else {
      this.toastr.error('Please select Voter and Candidate.');
    }
  }

}
