import apiClient from "../api-client/api-client.ts";
import {useNavigate} from "react-router-dom";

/// Запуск анимации для показа ошибок
export const triggerShake = (id: string, message: string) => {
    const input = document.getElementById(id)!;
    const error = document.getElementById(`${id}-error`)!;
    const messageElement = document.getElementById(`${id}-message`)!;

    input.classList.remove('shake');
    void input.offsetWidth;
    input.classList.add('error', 'shake');

    error.classList.remove('hidden');
    messageElement.textContent = message;

    setTimeout(() => {
        input.classList.remove('shake');
    }, 500);
};

/// Очистка ошибок при регистрации/входе
export const clearError = (id: string) => {
    const input = document.getElementById(id)!;
    const error = document.getElementById(`${id}-error`)!;
    const messageElement = document.getElementById(`${id}-message`)!;

    input.classList.remove('error');
    error.classList.add('hidden');
    messageElement.textContent = '';
};

/// Генерация JWT токена для шагов регистрации
export const generateJwt = <T extends object>(
    step: number,
    data: T,
    addAlert: (message: string) => void,
    navigate: ReturnType<typeof useNavigate>,
    startLoading: () => void,
    stopLoading: () => void
) => {
    startLoading();

    apiClient.post<{ token: string }>("Auth/GenerateStepJwtToken", {
        step: step + 1,
        data: data
    })
        .then(res => {
            stopLoading();
            if (res.status === 200) {
                navigate(`/register?t=${res.data.token}`);
            } else {
                addAlert("We couldn't verify your registration step. Please try again later.");
            }
        })
        .catch(error => {
            stopLoading();
            console.error(error);
            addAlert("An error occurred. Please try again later.");
        });
}