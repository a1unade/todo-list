import React, {useState} from "react";
import { LoadingContext } from "./loading-context";

export const LoadingProvider = ({ children }: { children: React.ReactNode }) => {
    const [loading, setLoading] = useState<boolean>(false);

    const startLoading = () => setLoading(true);
    const stopLoading = () => setLoading(false);

    return (
        <LoadingContext.Provider value={{ loading, startLoading, stopLoading }}>
            {children}
        </LoadingContext.Provider>
    );
};