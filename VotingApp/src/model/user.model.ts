export enum UserTypes {
  Candidates = 1,
  Voters = 2
}

export interface UserModel {
  Id?: number;
  Name?: string;
  UserType?: UserTypes;
  IsVoted?: boolean;
  CandidateVoteTotal?: number;
}
