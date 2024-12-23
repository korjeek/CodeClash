import { User } from "./userInterfaces.ts";

export interface Room {
    id: string;
    name: string;
    time: string;
    users: User[];
}