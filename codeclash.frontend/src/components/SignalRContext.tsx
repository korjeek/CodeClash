import React, {createContext, ReactNode, useContext, useEffect} from 'react';
import signalRService from "./SignalRService.ts";

interface SignalRContextProps {
    invoke: <TArg, TResult>(methodName: string, arg: TArg) => Promise<TResult | undefined>;
    stopConnection: () => Promise<void>;
}

const SignalRContext = createContext<SignalRContextProps>({
    invoke: async () => undefined,
    stopConnection: async () => {},
});

interface SignalRProviderProps {
    url: string;
    children: ReactNode;
}

export const useSignalR = () => useContext(SignalRContext);

export const SignalRProvider: React.FC<SignalRProviderProps> = ({ url, children }) => {
    useEffect(() => {
        const connect = async () => await signalRService.startConnection(url);
        connect();
    }, [url]);

    return (
        <SignalRContext.Provider value={{
            invoke: signalRService.invoke,
            stopConnection: signalRService.stopConnection
        }}>
            {children}
        </SignalRContext.Provider>
    );
};
