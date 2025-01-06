import { User } from "./UserInterfaces.ts";

export interface Room {
    id: string;
    name: string;
    time: string;
    issueName: string;
    users: User[];
}

export interface CreateRoomData {
    roomName: string;
    time: string;
    issueId: string;
}