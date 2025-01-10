import React, {useState, ReactNode } from 'react';
import {User} from "../interfaces/UserInterfaces.ts";
import { CompetitionContext } from './CompetitionState.ts';

export const CompetitionDataProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [competitionResults, setCompetitionResults] = useState<User[]>();

    return (
        <CompetitionContext.Provider value={{ competitionResults, setCompetitionResults }}>
            {children}
        </CompetitionContext.Provider>
    );
};
