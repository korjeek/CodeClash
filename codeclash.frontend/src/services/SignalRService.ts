import * as signalR from '@microsoft/signalr';
import {Response} from "../interfaces/ResponseInterface.ts";
import axios from "axios";

const SIGNALR_API_URL = 'https://localhost:7282/rooms'

export default class SignalRService {
    private readonly connection: signalR.HubConnection;
    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(SIGNALR_API_URL, {withCredentials: true})
            .withAutomaticReconnect()
            .build();

        // this.connection.onclose(async (error) => {
        //     console.log(error);
        //     if (error && error.statusCode === 401) {
        //         console.log('401 Unauthorized - attempting to refresh session...');
        //
        //         // Проверка наличия куки
        //         const hasSpookyCookies = document.cookie
        //             .split('; ')
        //             .some((row) => row.startsWith('spooky-cookies='));
        //
        //         if (hasSpookyCookies) {
        //             try {
        //                 // Обновляем куки с помощью запроса на сервер
        //                 await axios.post('https://localhost:7282/auth/refresh-token', null, { withCredentials: true });
        //                 console.log('Session refreshed successfully.');
        //
        //                 // Переподключаемся
        //                 await this.connection.start();
        //                 console.log('Reconnected to SignalR hub.');
        //             } catch (refreshError) {
        //                 console.error('Failed to refresh session:', refreshError);
        //                 window.location.href = '/login'; // Перенаправляем на страницу авторизации
        //             }
        //         } else {
        //             console.warn('No spooky-cookies found. Redirecting to login page.');
        //             window.location.href = '/login'; // Перенаправляем на страницу авторизации
        //         }
        //     } else {
        //         console.error('SignalR connection closed unexpectedly:', error);
        //     }
        // });
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
            console.log(err)
            console.error('Error while establishing SignalR connection:', err);
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
