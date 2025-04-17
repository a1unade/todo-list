import {createContext} from "react";
import {AlertContextType} from "../types/alert/alert-context-type.ts";


export const AlertContext = createContext<AlertContextType | undefined>(undefined);