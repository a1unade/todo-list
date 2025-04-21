import { createContext } from 'react';
import {LoadingContextType} from "../../types/loading/loading-context-type.ts";

export const LoadingContext = createContext<LoadingContextType | undefined>(undefined);