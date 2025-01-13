export interface User {
    email: string,
    name: string,
    sentTime: string,
    programWorkingTime: string,
    competitionOverhead: string
}

export interface UsersDTO{
    success: boolean;
    data: User[];
    error: string;
}

export interface RoomStatus {
    hasRoom: boolean,
    competitionIssueId: string;
}