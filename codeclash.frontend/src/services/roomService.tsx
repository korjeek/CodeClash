import axios from 'axios';
import * as signalR from "@microsoft/signalr";

const API_URL = 'https://localhost:7282/room';

export interface CreateRoomData {
    time: string,
    issueId: string,
    adminEmail: string
}

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

    async createRoom(createRoomData: CreateRoomData): Promise<{ roomKey: string;  }> {
        try {
            const roomData = await this.connection.invoke<{ roomKey: string; }>(
                "CreateRoom",
                createRoomData
            );

            console.log("Room created");
            return roomData;
        }
        catch (error) {
            console.log(error);
            throw error;
        }
    }

    async joinRoom(joinRoomData: JoinQuitRoomData): Promise<void> {
        try {
            await this.connection.invoke(
                "JoinRoom",
                joinRoomData.userEmail,
                joinRoomData.roomId
            );
        }
        catch (error) {
            console.log(error);
        }
    }

    async quitRoom(quitRoomData: JoinQuitRoomData): Promise<void> {
        try {
            await this.connection.invoke(
                "QuitRoom",
                quitRoomData.userEmail,
                quitRoomData.roomId
            );
        }
        catch (error) {
            console.log(error);
        }
    }

    async sendSolution(solution: UserSolution): Promise<SolutionResponse> { // использовать ли отдельный класс?
        try {
            return await this.connection.invoke<SolutionResponse>(
                "CheckSolution",
                solution.solution,
                solution.issueId,
                solution.roomId
            );
        }
        catch (error) {
            console.log(error);
            throw error;
        }
    }

    
}
