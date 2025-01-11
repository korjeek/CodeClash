import {useEffect, useMemo, useState} from "react";
import {useNavigate, useParams} from "react-router-dom";
import { Editor } from "@monaco-editor/react"
import {getProblem} from "../../services/ProblemService.ts";
import {Issue} from "../../interfaces/IssueInterfaces.ts";
import SignalRService from "../../services/SignalRService.ts";
import MarkdownRenderer from "../MarkdownRenderer.tsx";
import '../../style/CodeSpace/Main.css'
import '../../style/CodeSpace/Markdown.css'
import '../../style/CodeSpace/CodeEditor.css'
import '../../style/CodeSpace/Buttons.css'
import '../../style/CodeSpace/Result.css'
import {convertTime} from "../../services/TimeConverter.ts";
import {Room} from "../../interfaces/RoomInterfaces.ts";
import {CheckSolutionRequest} from "../../interfaces/CompetitionInterfaces.ts";
import {UsersDTO} from "../../interfaces/UserInterfaces.ts";
import {useCompetitionData} from "../../contexts/CompetitionState.ts";
import {motion} from "framer-motion";
import {SolutionTestResultDTO} from "../../interfaces/CodeResultInterfaces.ts";

export default function CodeSpace(){
    const { setCompetitionResults } = useCompetitionData();
    const [problem, setProblem] = useState<Issue>();
    const [time, setTime] = useState('');
    const [code, setCode] = useState('');
    const [result, setResult] = useState<SolutionTestResultDTO>()
    const [selected, setSelected] = useState<string>('Code')
    const signalR = useMemo(() => new SignalRService(), []);
    const { param } = useParams<string>();
    const navigate = useNavigate();

    useEffect(() => {
        const fetchProblem = async ()    => {
            await signalR.startConnection();
            const problem = await getProblem(param!);
            setProblem(problem);
            setCode(problem.initialCode)
        }

        signalR.onUserAction((usersDTO: UsersDTO) => {
            setCompetitionResults(usersDTO.data);
            navigate("/competition/result")
        }, "CompetitionEnded")

        signalR.onUserAction<string>((time: string) => {
            setTime(time);
        }, "UpdateTimer")

        fetchProblem();
    }, [navigate, param, setCompetitionResults, signalR]);

    const handleEditorChange = (value: string | undefined) => {
        setCode(value || '');
    };

    const submitCode = async () => {
        console.log(code)
        const response = await signalR.invoke<CheckSolutionRequest, SolutionTestResultDTO>("CheckSolution",
            {solution: code, issueName: problem!.name, leftTime: convertTime(time)})
        setResult(response)
        setSelected("Results")
    }

    const quitRoom = async () => {
        const response = await signalR.invoke<undefined, Room>("QuitRoom", undefined)
        console.log(response)
        await signalR.stopConnection()
        navigate('/competitions');
    }

    if (!problem)
        return null;

    return (
        <div className="container">
            <div className="header">
                <button className="quit-button" onClick={quitRoom}>Quit</button>
                <div className="timer">{time}</div>
                <button className="submit-button" onClick={submitCode}>Submit</button>
                </div>
                <div className="content">
                    <div className="markdown-container default-container-border">
                        <div className="code-editor">
                            <div className="content-header md-header-border">
                                <div className="desc-item">
                                    <svg xmlns="http://www.w3.org/2000/svg" version="1.0" width="17.000000pt"
                                         height="17.000000pt" viewBox="0 0 512.000000 512.000000"
                                         preserveAspectRatio="xMidYMid meet">
                                        <g transform="translate(0.000000,512.000000) scale(0.100000,-0.100000)" fill="#6A00FF"
                                           stroke="none">
                                            <path
                                                d="M864 5106 c-157 -29 -271 -76 -403 -163 -213 -142 -358 -349 -434 -623 -20 -70 -21 -105 -25 -530 -4 -501 -3 -512 55 -580 52 -60 104 -84 183 -84 102 0 176 48 219 144 20 43 21 64 21 451 0 243 4 429 11 465 17 97 81 217 154 288 62 61 160 121 240 147 29 9 165 14 490 18 436 6 451 7 490 28 144 77 178 264 69 377 -72 74 -48 71 -549 73 -333 1 -468 -2 -521 -11z"/>
                                            <path
                                                d="M3279 5106 c-197 -72 -213 -339 -25 -439 40 -22 51 -22 491 -28 489 -6 480 -5 600 -67 140 -74 256 -231 284 -386 7 -36 11 -219 11 -459 0 -274 4 -412 12 -438 15 -51 69 -114 122 -141 56 -30 155 -31 211 -1 52 28 89 67 114 123 20 43 21 64 21 462 0 443 -6 521 -47 653 -109 350 -386 612 -748 708 -75 19 -110 21 -545 24 -369 2 -473 0 -501 -11z"/>
                                            <path
                                                d="M1199 4146 c-129 -46 -193 -194 -138 -316 25 -55 62 -95 114 -123 l40 -22 1345 0 1345 0 40 22 c145 78 178 263 68 377 -76 78 58 71 -1438 73 -1084 2 -1347 0 -1376 -11z"/>
                                            <path
                                                d="M1184 3261 c-119 -54 -175 -196 -123 -311 25 -55 62 -95 114 -123 l40 -22 1025 0 c981 0 1027 1 1060 19 90 47 134 119 135 216 0 99 -51 181 -140 221 -38 18 -93 19 -1055 19 -967 0 -1017 -1 -1056 -19z"/>
                                            <path
                                                d="M1199 2306 c-129 -46 -193 -194 -138 -316 25 -55 62 -95 114 -123 l40 -22 1345 0 1345 0 40 22 c144 77 178 264 69 377 -75 78 58 71 -1439 73 -1084 2 -1347 0 -1376 -11z"/>
                                            <path
                                                d="M144 1981 c-56 -25 -110 -84 -128 -139 -23 -66 -23 -836 -1 -968 38 -218 124 -390 278 -551 135 -140 301 -238 495 -291 l97 -26 470 -1 c463 0 471 0 510 22 144 77 178 264 69 377 -71 73 -49 70 -549 76 -428 6 -453 7 -516 28 -165 55 -306 196 -361 361 -21 63 -23 87 -28 511 -6 491 -5 483 -71 549 -69 69 -179 91 -265 52z"/>
                                            <path
                                                d="M4785 1981 c-22 -10 -56 -33 -74 -52 -66 -66 -65 -58 -71 -549 -5 -424 -7 -448 -28 -511 -55 -165 -196 -306 -361 -361 -63 -21 -88 -22 -516 -28 -496 -6 -476 -3 -544 -71 -42 -42 -61 -80 -68 -139 -11 -98 41 -195 131 -243 41 -22 44 -22 511 -22 l470 1 97 26 c375 102 654 381 755 753 l27 100 4 453 c4 508 3 517 -65 587 -71 73 -181 96 -268 56z"/>
                                            <path
                                                d="M1199 1426 c-128 -46 -193 -194 -138 -316 25 -55 62 -95 114 -123 l40 -22 1025 0 c981 0 1027 1 1060 19 108 57 160 172 129 283 -17 62 -97 147 -154 162 -63 17 -2028 14 -2076 -3z"/>
                                        </g>
                                    </svg>
                                    Description
                                </div>
                            </div>
                            <section className="md-section">
                                <div className="md-description">
                                    <MarkdownRenderer markdown={problem.description}/>
                                </div>
                            </section>
                        </div>
                    </div>
                    <div className="editor-container default-container-border">
                        <div className="code-editor">
                            <div className="content-header code-header-border">
                                <button className="page-code-button" onClick={() => setSelected('Code')}>
                                    <svg xmlns="http://www.w3.org/2000/svg" version="1.0" width="20.000000pt"
                                         height="20.000000pt" viewBox="0 0 512.000000 512.000000"
                                         preserveAspectRatio="xMidYMid meet">
                                        <g transform="translate(0.000000,512.000000) scale(0.100000,-0.100000)"
                                           fill="#6A00FF" stroke="none">
                                            <path
                                                d="M2890 4248 c-25 -14 -57 -42 -72 -64 -21 -30 -124 -379 -463 -1564 -239 -839 -435 -1541 -435 -1561 0 -52 46 -136 91 -166 54 -35 103 -45 168 -32 44 9 63 20 100 58 l47 46 437 1530 c252 884 437 1547 437 1571 0 58 -44 139 -93 172 -58 39 -155 43 -217 10z"/>
                                            <path
                                                d="M1210 3617 c-27 -8 -146 -120 -493 -466 -267 -266 -467 -473 -480 -496 -28 -54 -28 -136 0 -190 13 -23 213 -230 480 -496 391 -389 464 -457 499 -467 83 -24 149 -8 211 52 57 53 78 121 62 199 -11 51 -19 60 -387 429 l-376 378 376 378 c365 366 376 378 386 427 36 169 -112 302 -278 252z"/>
                                            <path
                                                d="M3767 3616 c-67 -24 -110 -71 -132 -141 -13 -41 -13 -60 -4 -105 l11 -55 376 -377 376 -378 -376 -378 -376 -377 -11 -55 c-33 -160 109 -292 267 -250 43 12 83 48 505 469 274 272 467 472 479 496 14 26 21 59 21 95 0 36 -7 69 -21 95 -12 24 -205 223 -479 496 -391 389 -464 457 -499 467 -53 15 -89 15 -137 -2z"/>
                                        </g>
                                    </svg>
                                    <span className={selected === 'Code' ? "motion-span-white" : "motion-span"}>Code</span>
                                    {(selected === 'Code') && (
                                        <motion.span
                                            layoutId="pill-tab"
                                            transition={{ type: "spring", duration: 0.5 }}
                                            className="page-code-button-active"
                                        ></motion.span>
                                    )}
                                </button>
                                <button className="page-code-button" style={{padding: '.3vh 1vh'}} onClick={() => setSelected('Results')}>
                                    <svg xmlns="http://www.w3.org/2000/svg" version="1.0" width="17.000000pt"
                                         height="17.000000pt" viewBox="0 0 512.000000 512.000000"
                                         preserveAspectRatio="xMidYMid meet">
                                        <g transform="translate(0.000000,512.000000) scale(0.100000,-0.100000)"
                                           fill="#6A00FF" stroke="none">
                                            <path
                                                d="M1300 4953 c-484 -54 -904 -390 -1068 -853 -68 -193 -67 -184 -67 -927 0 -663 0 -672 21 -699 39 -53 71 -69 134 -69 63 0 95 16 134 69 21 27 21 41 26 694 6 645 7 670 27 744 88 315 312 563 600 668 171 61 139 60 1453 60 1314 0 1282 1 1453 -60 181 -66 358 -208 468 -374 67 -101 110 -205 140 -337 19 -80 26 -96 63 -134 81 -85 214 -67 257 36 27 67 -33 314 -117 481 -56 112 -129 214 -223 312 -183 190 -387 305 -666 373 -64 15 -180 17 -1335 18 -696 1 -1281 0 -1300 -2z"/>
                                            <path
                                                d="M1215 3426 c-79 -35 -115 -128 -82 -209 9 -21 130 -136 360 -342 190 -171 346 -312 346 -315 0 -3 -156 -144 -346 -315 -245 -220 -350 -320 -361 -345 -28 -67 -4 -146 60 -193 34 -25 104 -33 145 -16 51 22 872 765 889 805 18 42 18 86 0 128 -10 23 -145 151 -435 412 -232 209 -434 384 -449 391 -36 16 -90 15 -127 -1z"/>
                                            <path
                                                d="M4740 3189 c-29 -12 -67 -46 -83 -74 -9 -15 -13 -266 -17 -925 -5 -888 -6 -906 -27 -981 -87 -308 -296 -547 -574 -658 -176 -70 -77 -66 -1474 -66 -1184 0 -1265 1 -1335 18 -358 87 -625 348 -722 706 -18 64 -22 117 -27 343 -6 248 -7 268 -27 294 -39 53 -71 69 -134 69 -63 0 -95 -16 -134 -69 -20 -26 -21 -40 -21 -299 0 -226 4 -286 19 -357 111 -513 491 -893 1006 -1006 81 -18 153 -19 1370 -19 1217 0 1289 1 1370 19 515 113 893 491 1006 1006 18 80 19 144 19 997 0 905 0 912 -21 939 -11 15 -32 37 -46 47 -31 23 -111 32 -148 16z"/>
                                            <path
                                                d="M2495 1986 c-67 -29 -105 -106 -91 -181 9 -47 59 -102 104 -115 25 -8 257 -10 714 -8 665 3 677 3 704 24 53 39 69 71 69 134 0 63 -16 95 -69 134 -27 21 -38 21 -714 23 -556 2 -693 0 -717 -11z"/>
                                        </g>
                                    </svg>
                                    <span className={selected === 'Results' ? "motion-span-white" : "motion-span"}>Results</span>
                                    {(selected === 'Results') && (
                                        <motion.span
                                            layoutId="pill-tab"
                                            transition={{ type: "spring", duration: 0.5 }}
                                            className="page-code-button-active"
                                            style={{padding: '.3vh 1vh'}}
                                        ></motion.span>
                                    )}
                                </button>
                            </div>
                            {(selected == 'Code') &&
                                <Editor
                                    className="editor-margin"
                                    defaultLanguage="csharp"
                                    theme="vs-dark"
                                    options={{
                                        selectOnLineNumbers: true,
                                        minimap: {enabled: false},
                                        contextmenu: false
                                    }}
                                    defaultValue={code}
                                    onChange={handleEditorChange}
                            />}
                            {(selected === 'Results') && (
                                result ?
                                    <div className="result-container">
                                        <h1 style={result.okResult ? {color: "#5EFC8D", marginBottom: 0} : {color: "#DA4167", marginBottom: 0}}>
                                            {result.okResult ? "Succeed" :
                                                result.errorResult.compileError ? "Compile Error" : "Wrong Answer"}
                                        </h1>
                                        {(result.okResult || !result.errorResult.compileError) &&
                                            <h3 style={{margin: 0}}>{result.okResult ?
                                                `${result.okResult.passedTestsCount}/${result.okResult.passedTestsCount}` :
                                                `${result.errorResult.failedTestError.passedTestCount}/${result.errorResult.failedTestError.allTestsCount}`} testcases passed
                                            </h3>
                                        }
                                        {result.errorResult && result.errorResult.compileError &&
                                            <div className="error-window">
                                                {result.errorResult.compileError.compileError}
                                            </div>
                                        }
                                        {result.errorResult && result.errorResult.failedTestError &&
                                            <div className="wrong-res">
                                                <h3 style={{margin: 0}}>Name of the failed test:</h3>
                                                {result.errorResult.failedTestError.failedTestName}
                                            </div>
                                        }
                                    </div> :
                                    <div className="nothing-yet">
                                    <h2>
                                            Nothing yet :)
                                        </h2>
                                    </div>
                            )}
                        </div>
                    </div>
                </div>
        </div>
    );
};
