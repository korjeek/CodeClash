import React, { createContext, useContext, useState, ReactNode } from 'react';
import {Room} from "../interfaces/RoomInterfaces.ts";

interface RoomContextProps {
    room: Room | undefined;
    setRoom: (room: Room) => void;
}

const RoomContext = createContext<RoomContextProps | undefined>(undefined);

export const RoomDataProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [room, setRoom] = useState<Room>();

    return (
        <RoomContext.Provider value={{ room, setRoom }}>
            {children}
        </RoomContext.Provider>
    );
};

export const useRoomData = (): RoomContextProps => {
    const context = useContext(RoomContext);
    if (!context) {
        throw new Error('useData must be used within a DataProvider');
    }
    return context;
};
