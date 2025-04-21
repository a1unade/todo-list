import {useContext} from "react";
import {LoadingContext} from "../../contexts/loading/loading-context.tsx";
import {LoadingContextType} from "../../types/loading/loading-context-type.ts";

export const useLoading = (): LoadingContextType => {
    const context = useContext(LoadingContext);
    if (!context) {
        throw new Error('useLoading must be used within an LoadingProvider');
    }

    return context;
};