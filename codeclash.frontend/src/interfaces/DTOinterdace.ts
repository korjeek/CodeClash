import { User } from "./userInterfaces.ts";
import {Room} from "./roomInterfaces.ts";

export interface DTOObject{

}

export interface RoomsDTO {
    data: Room[];
    error: string;
    success: boolean;
}