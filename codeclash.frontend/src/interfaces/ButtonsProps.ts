import React from 'react';
import {Issue} from "./IssueInterfaces.ts";

export interface ChipProps {
    text: string;
    selected: boolean;
    setSelected: React.Dispatch<React.SetStateAction<string>>;
}

export interface MinuteButtonProps {
    text: string;
    active: boolean;
    setActive: React.Dispatch<React.SetStateAction<string>>;
}

export interface TaskButtonProps {
    issue: Issue;
    active: boolean;
    setActive: React.Dispatch<React.SetStateAction<string>>;
    setTask: React.Dispatch<React.SetStateAction<boolean>>;
    handleSlide: () => void;
}