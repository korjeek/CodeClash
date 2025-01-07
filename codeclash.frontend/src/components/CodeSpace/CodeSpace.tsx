import {useEffect, useMemo, useState} from "react";
import { useParams } from "react-router-dom";
import { Editor } from "@monaco-editor/react"
import '../../style/CodeSpace/Main.css'
import ReactMarkdown from 'react-markdown';
import {getProblem} from "../../services/ProblemService.ts";
import {Issue} from "../../interfaces/IssueInterfaces.ts";
import SignalRService from "../../services/SignalRService.ts";

export default function CodeSpace(){
    const [problem, setProblem] = useState<Issue>();
    const [time, setTime] = useState('');
    const signalR = useMemo(() => new SignalRService(), []);
    const { param } = useParams<string>();

    useEffect(() => {
        const fetchProblem = async ()    => {
            await signalR.startConnection();
            console.log(signalR.connection.connectionId, signalR.connection.state);
            const problem = await getProblem(param!);
            setProblem(problem);
        }

        signalR.onUserAction<string>((time: string) => {
            setTime(time);
        }, "UpdateTimer")

        fetchProblem();
    }, [param, signalR]);



    if (!problem)
        return null;

    return (
        <div className="container">
            <div className="header">
                <button>Submit</button>
                <button>Quit</button>
                <div>{time}</div>
            </div>
            <div className="content">
                <div className="markdown-container">
                    <ReactMarkdown>{problem.description}</ReactMarkdown>
                </div>
                <div className="editor-container">
                    <div className="editor-wrapper">
                        <Editor
                            width="50vw"
                            height="100vh"
                            language="javascript"
                            theme="vs-dark"
                            options={{
                                selectOnLineNumbers: true,
                                minimap: {enabled: false}, // Отключение MiniMap
                                contextmenu: false // Отключение контекстного меню
                            }}
                        />
                    </div>
                </div>
            </div>
        </div>
    );
};
