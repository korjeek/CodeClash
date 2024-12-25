// src/components/CreateRoomPage.tsx
import React, {useContext, useState} from 'react';
import {CreateRoomData, RoomMethods, RoomService} from '../services/roomService.ts';
import { Room } from '../interfaces/roomInterfaces.ts'
import {RoomServiceContext, useRoomService} from "./RoomServiceContext.tsx";

const CreateRoomPage: React.FC = () => {
    const [time, setTime] = useState<string>('');
    const [issueId, setIssueId] = useState<string>('');
    const [adminEmail, setAdminEmail] = useState<string>('');
    const [roomKey, setRoomKey] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [roomName, setName] = useState('');
    const roomService = useRoomService();

    const handleCreateRoom = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            await roomService.startConnection();
            console.log({roomName, time, issueId})
            const room = {roomName, time, issueId}
            const createdRoom = await roomService.actionWithRoom<CreateRoomData>(room, RoomMethods.CreateRoom);
            await joinRoom(createdRoom!.id)
            // setRoomKey(result.id);
            alert(`Room created successfully! Room Key: ${createdRoom}`);
        } catch (err) {
            console.error(err);
            setError('Failed to create room. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    const joinRoom = async (roomId: string) => window.location.href = `/lobby?roomId=${roomId}`;

    return (
        <div style={{ padding: '20px' }}>
            <h1>Create Room</h1>
            <form onSubmit={handleCreateRoom} style={{maxWidth: '400px', margin: '0 auto'}}>
                <div style={{marginBottom: '10px'}}>
                    <label htmlFor="time">Name: </label>
                    <input
                        id="roomName"
                        type="text"
                        value={roomName}
                        onChange={(e) => setName(e.target.value)}
                        required
                        style={{width: '100%', padding: '8px', marginBottom: '10px'}}
                    />
                </div>
                <div style={{marginBottom: '10px'}}>
                    <label htmlFor="time">Competition Time (e.g., 30m, 1h): </label>
                    <input
                        id="time"
                        type="text"
                        value={time}
                        onChange={(e) => setTime(e.target.value)}
                        required
                        style={{width: '100%', padding: '8px', marginBottom: '10px'}}
                    />
                </div>
                <div style={{marginBottom: '10px'}}>
                    <label htmlFor="issueId">Issue ID: </label>
                    <input
                        id="issueId"
                        type="text"
                        value={issueId}
                        onChange={(e) => setIssueId(e.target.value)}
                        required
                        style={{width: '100%', padding: '8px', marginBottom: '10px'}}
                    />
                </div>
                <button type="submit" style={{padding: '10px 20px'}}>
                    Create Room
                </button>
            </form>
            {roomKey && (
                <div style={{marginTop: '20px', color: 'green'}}>
                    <strong>Room Created:</strong> {roomKey}
                </div>
            )}
            {error && (
                <div style={{marginTop: '20px', color: 'red'}}>
                    <strong>Error:</strong> {error}
                </div>
            )}
        </div>
    );
};

export default CreateRoomPage;