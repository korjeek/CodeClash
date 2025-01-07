import * as signalR from '@microsoft/signalr';
import {Response} from "../interfaces/ResponseInterface.ts";
import {Room} from "../interfaces/RoomInterfaces.ts";

const SIGNALR_API_URL = 'https://localhost:7282/rooms'

export default class SignalRService {
    private readonly connection: signalR.HubConnection;

    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(SIGNALR_API_URL)
            .withAutomaticReconnect()
            .build();

        this.connection.on("UserJoined", (room: Room) => {
            console.log(`User joined: ${room}`);
            console.log(room.users);
        })

        this.connection.on("CompetitionStarted", (url: string) => {
            console.log("Competition started");
            window.location.href = url;
        })
    }

    public async startConnection(): Promise<void> {
        try {
            await this.connection.start();
            console.log('SignalR connected');
        }
        catch (err) {
            console.error('Error while establishing SignalR connection:', err);
        }
    }

    public async invoke<TArg, TResult>(methodName: string, arg: TArg): Promise<TResult | undefined> {
        try {
            const response = await this.connection.invoke<Response<TResult>>(methodName, arg);
            if (response.success)
                return response.data;
            console.log(response.error);
        }
        catch (err) {
            console.error('Error invoking method:', err);
        }
    }

    public async stopConnection(): Promise<void> {
        if (this.connection) {
            try {
                await this.connection.stop();
                console.log('SignalR connection stopped');
            } catch (err) {
                console.error('Error stopping SignalR connection:', err);
            }
        }
    }
}
