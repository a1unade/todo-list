import {UserActionTypes} from "../../types/actions/user-actions.ts";

interface ICreateUser {
    type: UserActionTypes.CREATE_USER;
    payload: {
        // Имя пользователя
        name: string,
        // Фамилия пользователя
        surname: string,
        // Дата рождения пользователя
        birthDate: string,
        // Пол
        gender: string,
        // Страна
        country: string, // Скорее всего лишнее, просто на будущее
        // Ник пользователя
        nickname: string,
        // Электронная почта пользователя
        email: string,
        // Выполнен ли вход
        isAuthenticated: boolean,
        // Аватарка пользователя
        avatar: string,
    };
}

export default ICreateUser;