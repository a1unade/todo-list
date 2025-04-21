import React, { useState } from 'react';
import { AlertType } from '../../types/alert/alert-type.ts';
import { AlertContext } from './alert-context.tsx';

export const AlertProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [alerts, setAlerts] = useState<AlertType[]>([]);

    const addAlert = (message: string) => {
        const id = Date.now();
        setAlerts([...alerts, { id, message }]);
    };

    const removeAlert = (id: number) => {
        setAlerts(alerts.filter((alert) => alert.id !== id));
    };

    return (
        <AlertContext.Provider value={{ alerts, addAlert, removeAlert }}>
            {children}
        </AlertContext.Provider>
    );
};