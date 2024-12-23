// src/components/CreateRoomPage.tsx
import React, { useState } from 'react';

const Lobby: React.FC = () => {
    const [time, setTime] = useState<string>('');
    const [issueId, setIssueId] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [roomName, setName] = useState('');

    const handleCreateRoom = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);
    };

    return (
        <div style={{ padding: '20px' }}>
            <h1>Lobby</h1>
            <button type="submit" disabled={loading} style={{padding: '10px 20px'}}>
                {loading ? 'Creating Room...' : 'Create Room'}
            </button>
        </div>
    );
};

export default Lobby;