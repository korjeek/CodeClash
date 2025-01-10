import {useEffect, useState} from "react";
import {getAllProblems} from "../../services/ProblemService.ts";
import {Issue} from "../../interfaces/IssueInterfaces.ts";
import BaseNavBar from "../NavBars/BaseNavBar.tsx";
import {TabItem} from "../../Props/PageStateProps.ts";


export default function ProblemsPage() {
    const [problems, setProblems] = useState<Issue[]>([]);

    useEffect(() => {
        async function getIssues() {
            const problems = await getAllProblems()
            setProblems(problems)
        }

        getIssues();
    }, []);

    return (
        <div className="menu-page">
            <BaseNavBar tab={TabItem.Problems}/>
        </div>
    )
}