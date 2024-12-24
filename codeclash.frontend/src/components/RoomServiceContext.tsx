import React, {createContext, ReactNode, useContext, useState} from 'react';
import {RoomService} from "../services/roomService.ts";

const roomsServiceConstant = new RoomService();
const RoomServiceContext = createContext<RoomService>(roomsServiceConstant);

export function RoomServiceProvider({children}: {children: ReactNode}) {
    return (
        <RoomServiceContext.Provider value={ roomsServiceConstant }>
            {children}
        </RoomServiceContext.Provider>
    );
}

export function useRoomService() {
    return useContext(RoomServiceContext);
}