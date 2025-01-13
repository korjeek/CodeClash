import {useEffect, useState} from "react";
import {getAllProblems} from "../../services/ProblemService.ts";
import {Issue} from "../../interfaces/IssueInterfaces.ts";
import BaseNavBar from "../NavBars/BaseNavBar.tsx";
import {TabItem} from "../../Props/PageStateProps.ts";
import useAxiosPrivate from "../../hooks/useAxiosPrivate.ts";


export default function ProblemsPage() {
    const [problems, setProblems] = useState<Issue[]>([]);
    const axiosPrivate = useAxiosPrivate()

    useEffect(() => {
        async function getIssues() {
            const problems = await getAllProblems(axiosPrivate)
            setProblems(problems)
        }

        getIssues();
    }, []);

    return (
        <div className="menu-page">
            <BaseNavBar tab={TabItem.Problems}/>
            <div className="content-wrapper">
                <h1>Problems</h1>
                {problems.map((problem) => (
                    <div className="problem-item-description" key={problem.id}>{problem.name}</div>
                ))}
            </div>
        </div>
    )
}