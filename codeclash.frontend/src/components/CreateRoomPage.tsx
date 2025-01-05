import React, {useContext, useEffect, useState} from 'react';
import {CreateRoomData} from '../services/roomService.ts';
import { Room } from '../interfaces/roomInterfaces.ts'
import '../style/Default/BaseNavBar.css'
import '../style/CreateLobby/Main.css'
import '../style/CreateLobby/Inputs.css'
import '../style/CreateLobby/Buttons.css'
import '../style/CreateLobby/ProblemsList.css'
import {RoomServiceContext, useRoomService} from "./RoomServiceContext.tsx";
import {useSignalR} from "./SignalRContext.tsx";
import SignalRService from "./SignalRService.ts";
import {useNavigate} from "react-router-dom";
import BaseNavBar from "./NavBars/BaseNavBar.tsx";
import { motion } from "framer-motion";

const minutes = ["5 min", "10 min", "30 min", "60 min"]
const problems = ["Two Sum", "More than one!", "Find most massive subarray", "I like this one!", "Binary Search"]

const CreateRoomPage: React.FC = () => {
    const [time, setTime] = useState<string>('');
    const [issueId, setIssueId] = useState<string>('');
    const [roomKey, setRoomKey] = useState<string | null>(null);
    const [roomName, setName] = useState('');
    const [activeMin, setActiveMin] = useState(minutes[0]);
    const [activeProblem, setActiveProblem] = useState(null);
    const [inputValue, setInputValue] = useState<string>('');
    const [selectedTask, setSelectedTask] = useState(false);
    const signalR = new SignalRService()
    const navigate = useNavigate();

    useEffect(() => {
        async function fetchRooms() {
            //TODO: необходимо получать список существующих задач
        }

        fetchRooms();
    }, [])

    const handleCreateRoom = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            console.log({roomName, time, issueId})
            const room = {roomName, time, issueId}
            await signalR.startConnection()
            const createdRoom = await signalR.invoke<CreateRoomData, Room>("CreateRoom", room);
            if (createdRoom)
                await joinRoom(createdRoom.id)
        }
        catch {
            console.error('Failed to create room. Please try again.');
        }
    };

    const joinRoom = async (roomId: string) => navigate(`/lobby?roomId=${roomId}`);
    const [isSlided, setIsSlided] = useState(false);
    const handleSlide = () => {
        setIsSlided(!isSlided);
    };
    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setInputValue(event.target.value);
    };

    return (
        <div className="menu-page">
            <BaseNavBar/>
            <motion.div
                initial={{y: 0}}
                animate={{y: isSlided ? '-100%' : 0}}
                transition={{duration: 0.5}}
                className="content-wrapper">
                <form onSubmit={handleCreateRoom} className="grid-create-container">
                    <div className="step-container step-1">
                        <div className="step-description">
                            <h1>STEP 1</h1>
                            <b className="description">Come up with a name for your competition</b>
                        </div>
                        <div className="spacer"></div>
                        <div className="step-description">
                            <div className="input-wrapper">
                                <input
                                    type="text"
                                    name="room-name"
                                    id="room-name"
                                    placeholder="Name of the competition"
                                    onChange={handleInputChange}
                                />
                            </div>
                        </div>
                    </div>
                    <div className="step-container step-2">
                        <div className="step-description">
                            <h1>STEP 2</h1>
                            <b className="description">Choose how long your competition will last</b>
                        </div>
                        <div className="spacer"></div>
                        <div className="time-choose">
                            {minutes.map((minute) => (
                                <MinuteButton
                                    text={minute}
                                    active={activeMin === minute}
                                    setActive={setActiveMin}
                                    key={minute}
                                />
                            ))}
                        </div>
                    </div>
                    <div className="step-container step-3">
                        <div className="step-description">
                            <h1>STEP 3</h1>
                            <b className="description">Select the problem to be solved</b>
                        </div>
                        <div className="spacer"></div>
                        <div className="time-choose">
                            <button
                                onClick={handleSlide}
                                className={selectedTask ? "active-time-choose-button" : "time-choose-button"}>Select problem
                            </button>
                        </div>
                    </div>
                </form>
                <button
                    className={selectedTask && (inputValue.trim() !== '' ? 'lightgreen' : '')
                        ? "active-acc-create-room-btn" : "acc-create-room-btn"}
                    disabled={selectedTask}
                >
                    Create Room</button>
            </motion.div>
            <motion.div
                className="list-wrapper"
                initial={{y: '100%'}}
                animate={{y: isSlided ? 0 : '100%'}}
                transition={{duration: 0.5}}
            >
                <div className="new-content">
                    <h1>Problems</h1>
                    <div>
                        {problems.map((problem) => (
                            <TaskButton
                                text={problem}
                                active={activeProblem === problem}
                                setActive={setActiveProblem}
                                setTask={setSelectedTask}
                                handleSlide={handleSlide}
                                key={problem}
                            />
                        ))}
                    </div>
                </div>
            </motion.div>
        </div>
    );
};

export default CreateRoomPage;

const MinuteButton = ({text, active, setActive}) => {
    return (
        <button
            onClick={() => setActive(text)}
            className={active ? "active-time-choose-button" : "time-choose-button"}
        >{text}
        </button>
    )
}

const TaskButton = ({text, active, setActive, setTask, handleSlide}) => {
    return (
        <div className="problem-item">
            <div className="problem-item-description">{text}</div>
            <button
                onClick={() => {
                    setActive(text)
                    handleSlide()
                    setTask(true)
                }}
                className={active ? "active-time-choose-button" : "time-choose-button"}>Select</button>
        </div>
    )
}