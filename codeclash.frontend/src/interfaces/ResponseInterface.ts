import { User } from "./userInterfaces.ts";
import {Room} from "./roomInterfaces.ts";

export interface DTOObject{

}

export interface Response<T> {
    data: T;
    error: string;
    success: boolean;
}