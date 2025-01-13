import * as signalR from '@microsoft/signalr';
import {Response} from "../interfaces/ResponseInterface.ts";
import axios from "../api/axios.ts";

const SIGNALR_API_URL = 'https://localhost:7282/rooms'

export default class SignalRService {
    private readonly connection: signalR.HubConnection;
    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(SIGNALR_API_URL,
                {
                    withCredentials: true,
                    accessTokenFactory(): string | Promise<string> {
                        return document.cookie
                            .split('; ')
                            .find(row => row.startsWith('spooky-cookie='))
                            ?.split('=')[1] || '';
                    }
                })
            .withAutomaticReconnect()
            .build();
    }

    public onUserAction = <T>(callback: (...args: T[]) => void, method: string) => {
        this.connection.on(method, callback);
    };

    public async startConnection(): Promise<void> {
        try {
            if (this.connection.state == signalR.HubConnectionState.Disconnected){
                await this.connection.start();
            }
        }
        catch (err) {
            await refreshToken();
            this.connection.start().catch(err => console.error('Error while reconnecting:', err));
        }
    }

    public async invoke<TArg, TResult>(methodName: string, arg: TArg): Promise<TResult | undefined> {
        try {
            let response: Response<TResult>;

            if (arg !== undefined)
                response = await this.connection.invoke<Response<TResult>>(methodName, arg);
            else
                response = await this.connection.invoke<Response<TResult>>(methodName);

            if (response.success)
                return response.data;
        }
        catch (err) {
            await refreshToken();
            return this.invoke<TArg, TResult>(methodName, arg);
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

const refreshToken = async () => {
    try {
        const refreshToken = document.cookie
            .split('; ')
            .find(row => row.startsWith('olega-na-front='))
            ?.split('=')[1];

        if (refreshToken) {
            const response = await axios.post('/auth/refresh-token', undefined, {
                withCredentials: true
            });

            const { access_token } = response.data;

            // Устанавливаем новый аксес токен в cookies
            document.cookie = `access_token=${access_token}; HttpOnly; Secure; SameSite=Strict; Path=/`;
        }
    } catch (error) {
        console.error('Token refresh failed:', error);
    }
};
