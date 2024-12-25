import React from "react";
import { useParams } from "react-router-dom";
import { Editor } from "@monaco-editor/react"
import '../../style/CodeSpace/Main.css'
import ReactMarkdown from 'react-markdown';

export default function CodeSpace(){
    const { param } = useParams();
    console.log(param);

    return (
        <div className="container">
            <div className="header">
                <button>Submit</button>
                <button>Quit</button>
            </div>
            <div className="content">
                <div className="markdown-container">
                    <ReactMarkdown>{'### Markdown Content # This is a sample Markdown content.'}</ReactMarkdown>
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
