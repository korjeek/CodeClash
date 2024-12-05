import { User } from "./userInterfaces.ts";
import { Issue } from "./issueInterfaces.ts";

export interface RoomOptions {
    time: string,
    issueId: string
}

export interface Room {
    id: string,
    issue: Issue,
    participants: User[],
    time: string
}