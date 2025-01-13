import {createContext, useContext} from "react";
import {User} from "../interfaces/UserInterfaces.ts";

interface CompetitionContextProps {
    competitionResults: User[] | undefined;
    setCompetitionResults: (users: User[]) => void;
}

export const CompetitionContext = createContext<CompetitionContextProps | undefined>(undefined);

export const useCompetitionData = (): CompetitionContextProps => {
    const context = useContext(CompetitionContext);
    if (!context) {
        throw new Error('useData must be used within a DataProvider');
    }
    return context;
};