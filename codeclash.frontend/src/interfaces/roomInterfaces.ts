import { User } from "./userInterfaces.ts";
import { Issue } from "./issueInterfaces.ts";

export interface Room {
    id: string,
    issue: Issue,
    participants: User[],
    time: string
}