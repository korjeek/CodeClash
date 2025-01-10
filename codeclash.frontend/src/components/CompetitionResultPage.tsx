import {useCompetitionData} from "../contexts/CompetitionState.ts";
import BaseNavBar from "./NavBars/BaseNavBar.tsx";
import {TabItem} from "../Props/PageStateProps.ts";
import "../style/CompetitionResultPage/Main.css"

export default function CompetitionResultPage() {
    const { competitionResults } = useCompetitionData()
    console.log(competitionResults)

    return (
        <div className="menu-page">
            <BaseNavBar tab={TabItem.Competitions}/>
            <div className="content-wrapper">
                <h1 className="room-header">Results</h1>
                <div className="result-table">
                    <div className="result-header-item rank">Rank</div>
                    <div className="result-header-item">Name</div>
                    <div className="result-header-item">Points</div>
                    <div className="result-header-item">Runtime</div>
                    <div className="result-header-item">Send Time</div>
                    {competitionResults?.map((user, index) => (
                        <div className="row-wrapper">
                            <div className="rank-item" style={{gridArea: `${index + 2} / 1 / ${index + 2} / 1`}}>{index + 1}</div>
                            <div className="cell" style={{gridArea: `${index + 2} / 2 / ${index + 2} / 2`}}>{user.name}</div>
                            <div className="cell" style={{gridArea: `${index + 2} / 3 / ${index + 2} / 3`}}>{user.competitionOverhead}</div>
                            <div className="cell" style={{gridArea: `${index + 2} / 4 / ${index + 2} / 4`}}>{user.programWorkingTime}ms</div>
                            <div className="cell" style={{gridArea: `${index + 2} / 5 / ${index + 2} / 5`}}>{user.sentTime}</div>
                            <div className="cell-back" style={{gridArea: `${index + 2} / 2 / ${index + 2} / 6`}}></div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
}