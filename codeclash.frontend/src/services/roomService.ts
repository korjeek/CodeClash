import axios from 'axios';
import * as signalR from "@microsoft/signalr";
import {HubConnectionState} from "@microsoft/signalr";
import {Room} from "../interfaces/roomInterfaces.ts";
import {Response} from "../interfaces/ResponseInterface.ts";

const API_URL = 'https://localhost:7282/rooms';

export interface JoinQuitRoomData {
    roomId: string,
    userEmail: string
}

export interface SolutionResponse {    
    // задача решена?
    //               да - высылаем всем пользователям, что задача решена, соревнование окончено
    //               нет - высылаем ответ тому, кто послал запрос
}

export interface CreateRoomData{
    roomName: string,
    time: string,
    issueId: string
}

export interface StartCompetitionData{
    id: string,
    time: string
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
    public room!: Room;

    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(API_URL)
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Information)
            .build();
        
        this.connection.onclose(() => {
            console.error("Connection closed")
        })

        this.connection.on("UserJoined", (userId: string) => {
            console.log(`User joined: ${userId}`);
        })

        this.connection.on("UserLeft", (userId: string) => {
            console.log(`User left: ${userId}`);
        })

        this.connection.on("CompetitionStarted", (url) => {
            console.log(`Competition started: ${url}`);
            window.location.href = url;
        })
    }

    async startConnection(): Promise<void> {
        try {
            if (this.connection.state == HubConnectionState.Disconnected)
                await this.connection.start();
            console.log(`Status: ${this.connection.state}`);
        }
        catch (error) {
            console.log(error);
        }
    }

    async checkIsUserAdmin(): Promise<boolean> {
        console.log(this.connection.state)
        const result = await this.connection.invoke<Response<boolean>>(RoomMethods.CheckIsUserAdmin)
        return result.data;
    }

    async closeConnection(): Promise<void> {
        try {
            await this.connection.stop();
            console.log("Connection closed")
            console.log(this.connection.state)
        }
        catch (error) {
            console.log(error);
        }
    }

    async actionWithRoom<T>(requestData: T, method: string): Promise<Room | undefined> {
        try {
            const room = await this.connection.invoke<Response<Room>>(method, requestData);
            console.log(this.connection.state)
            if (room.success)
                return room.data;
            console.log(room.error)
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

export enum RoomMethods {
    JoinRoom = "JoinRoom",
    QuitRoom = "QuitRoom",
    CreateRoom = "CreateRoom",
    CheckIsUserAdmin = "CheckIsUserAdmin",
    StartCompetition = "StartCompetition",
}

export const getRoomsList = async () => {
    const response = await axios.get<Response<Room[]>>(`${API_URL}/get-rooms`);
    return response.data.data;
}
