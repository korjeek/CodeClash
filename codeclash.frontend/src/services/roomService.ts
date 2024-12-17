import axios from 'axios';
import * as signalR from "@microsoft/signalr";
import { RoomOptions, Room } from "../interfaces/roomInterfaces.ts";

const API_URL = 'https://localhost:7282/room';

export interface JoinQuitRoomData {
    roomId: string,
    userEmail: string
}

export interface SolutionResponse {    
    // задача решена?
    //               да - высылаем всем пользователям, что задача решена, соревнование окончено
    //               нет - высылаем ответ тому, кто послал запрос
}

// по истечении таймера выслать всем сообщение, что соревнование закончено
// вывести результаты

export interface UserSolution {
    solution: string,
    issueId: string,
    roomId: string
}

export class RoomService {
    private connection: signalR.HubConnection;

    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(API_URL)
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Information)
            .build();
        
        this.connection.onclose(() => {
            console.error("Connection closed. Reconnecting...")
        })
    }

    async startConnection(): Promise<void> {
        try {
            await this.connection.start();
            console.log("Connected to hub");
        }
        catch (error) {
            console.log(error);
        }
    }

    async createRoom(roomOptions: RoomOptions): Promise<Room> {
        const methodName = "CreateRoom";
        try {
            const room = await this.connection.invoke<Room>(methodName, roomOptions);

            console.log(room);
            console.log("Room created");
            return room;
        }
        catch (error) {
            console.log(error);
            throw error;
        }
    }

    async joinRoom(room: Room): Promise<void> {
        try {
            await this.connection.invoke("JoinRoom", room.id);
        }
        catch (error) {
            console.log(error);
        }
    }

    async quitRoom(room: Room): Promise<void> {
        try {
            await this.connection.invoke("QuitRoom", room.id);
        }
        catch (error) {
            console.log(error);
        }
    }

    async sendSolution(solution: UserSolution): Promise<SolutionResponse> {
        try {
            return await this.connection.invoke<SolutionResponse>("CheckSolution", solution.solution, solution.issueId);
        }
        catch (error) {
            console.log(error);
            throw error;
        }
    }
}

// export const getRoomsList = async () => {
//     response = await axios.get('')
// }
