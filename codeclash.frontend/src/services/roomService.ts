import axios from 'axios';
import * as signalR from "@microsoft/signalr";
import { RoomOptions, Room } from "../interfaces/roomInterfaces.ts";
import {DTO, Response} from "../interfaces/ResponseInterface.ts";

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

interface CreateRoomData{
    roomName: string,
    time: string,
    issueId: string
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

        this.connection.on("UserJoined", (userId: string) => {
            console.log(`User joined: ${userId}`);
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

    async createRoom(createRoomData: CreateRoomData): Promise<Room> {
        try {
            console.log(createRoomData)
            const room = await this.connection.invoke<Room>("CreateRoom", createRoomData);

            console.log(room);
            return room;
        }
        catch (error) {
            console.log(error);
            throw error;
        }
    }

    async actionWithRoom(roomId: string, method: string){
        try {
            const room = await this.connection.invoke<Response<Room>>(method, roomId);
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
    QuitRoom = "QuitRoom"
}

export const getRoomsList = async () => {
    const response = await axios.get<Response<Room[]>>(`${API_URL}/get-rooms`);
    return response.data.data;
}
