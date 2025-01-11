import React, {useEffect, useState} from 'react';
import {CreateRoomData, Room} from '../../interfaces/RoomInterfaces.ts'
import SignalRService from "../../services/SignalRService.ts";
import {useNavigate} from "react-router-dom";
import BaseNavBar from "../NavBars/BaseNavBar.tsx";
import { motion } from "framer-motion";
import {getAllProblems} from "../../services/ProblemService.ts";
import {Issue} from "../../interfaces/IssueInterfaces.ts";
import '../../style/Default/BaseNavBar.css'
import '../../style/CreateLobby/Main.css'
import '../../style/CreateLobby/Inputs.css'
import '../../style/CreateLobby/Buttons.css'
import '../../style/CreateLobby/ProblemsList.css'
import {MinuteButtonProps, TaskButtonProps} from "../../Props/ButtonsProps.ts";
import {TabItem} from "../../Props/PageStateProps.ts";

const minutes = ["5", "10", "30", "60"]

export default function CreateRoomPage() {
    const [isSlided, setIsSlided] = useState(false);
    const [problems, setProblems] = useState<Issue[]>([]);
    const [roomName, setRoomName] = useState('');
    const [activeMin, setActiveMin] = useState(minutes[0]);
    const [activeProblem, setActiveProblem] = useState('');
    const [inputValue, setInputValue] = useState<string>('');
    const [selectedTask, setSelectedTask] = useState(false);
    const [userInRoom, setUserInRoom] = useState<boolean>(false);
    const signalR = new SignalRService()
    const navigate = useNavigate();

    useEffect(() => {
        async function getIssues() {
            const problems = await getAllProblems()
            setProblems(problems)
        }

        getIssues();
    }, [])

    const createRoom = async () => {
        try {
            const time = activeMin === '60' ? `01:00:00` : `00:${activeMin}:00`
            const room = {roomName, time: time, issueId: activeProblem!}
            await signalR.startConnection()
            const createdRoom = await signalR.invoke<CreateRoomData, Room>("CreateRoom", room);
            if (createdRoom)
            {
                await signalR.invoke<string, Room>("JoinRoom", createdRoom.id)
                navigate(`/lobby`);
            }
            else
                setUserInRoom(true)
        }
        catch {
            console.error('Failed to create room. Please try again.');
        }
    };

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setInputValue(event.target.value);
        setRoomName(event.target.value);
    }

    return (
        <div className="menu-page">
            <BaseNavBar tab={TabItem.Competitions}/>
            <motion.div
                initial={{y: 0}}
                animate={{y: isSlided ? '-100%' : 0}}
                transition={{duration: 0.5}}
                className="content-wrapper">
                <div className="grid-create-container">
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
                                onClick={() => setIsSlided(!isSlided)}
                                className={selectedTask ? "active-time-choose-button" : "time-choose-button"}>Select problem
                            </button>
                        </div>
                    </div>
                </div>
                {userInRoom && <div className="wrong-login-warning">User is already in room.</div>}
                <button
                    className={selectedTask && (inputValue.trim() !== '' ? 'lightgreen' : '')
                        ? "active-acc-create-room-btn" : "acc-create-room-btn"}
                    disabled={!selectedTask || !inputValue.trim()}
                    onClick={createRoom}
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
                                issue={problem}
                                active={activeProblem === problem.id}
                                setActive={setActiveProblem}
                                setTask={setSelectedTask}
                                handleSlide={() => setIsSlided(!isSlided)}
                                key={problem.id}
                            />
                        ))}
                    </div>
                </div>
            </motion.div>
        </div>
    );
};

const MinuteButton: React.FC<MinuteButtonProps> = ({text, active, setActive}) => {
    return (
        <button
            onClick={() => setActive(text)}
            className={active ? "active-time-choose-button" : "time-choose-button"}
        >{text} min
        </button>
    )
}

const TaskButton: React.FC<TaskButtonProps> = ({issue, active, setActive, setTask, handleSlide}) => {
    return (
        <div className="problem-item">
            <div className="problem-item-description">{issue.name}</div>
            <button
                onClick={() => {
                    setActive(issue.id)
                    handleSlide();
                    setTask(true)
                }}
                className={active ? "active-time-choose-button" : "time-choose-button"}>Select</button>
        </div>
    )
}