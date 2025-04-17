import IUserState from "../../../interfaces/user-actions/user-state.ts";
import {UserAction, UserActionTypes} from "../../../types/actions/user-actions.ts";

// Начальное состояние пользователя
const initialState: IUserState = {
    // Имя пользователя
    name: "",
    // Фамилия пользователя
    surname: "",
    // Дата рождения пользователя
    birthDate: "",
    // Пол
    gender: "",
    // Страна
    country: "", // Скорее всего лишнее, просто на будущее
    // Ник пользователя
    nickname: "",
    // Электронная почта пользователя
    email: "",
    // Выполнен ли вход
    isAuthenticated: false,
    // Аватарка пользователя
    avatar: "",
}

export const userReducer = (state = initialState, action: UserAction) => {
    switch (action.type) {
        case UserActionTypes.UPDATE_USER_NAME:
            return {...state, name: action.payload, isAuthenticated: true}; // Смена имени у пользователя
        case UserActionTypes.UPDATE_USER_SURNAME:
            return {...state, surname: action.payload, isAuthenticated: true}; // Смена фамилии у пользователя
        case UserActionTypes.UPDATE_USER_NICKNAME:
            return {...state, nickname: action.payload, isAuthenticated: true}; // Смена ника у пользователя
        case UserActionTypes.UPDATE_USER_EMAIL:
            return {...state, email: action.payload, isAuthenticated: true}; // Смена электронной почты у пользователя
        case UserActionTypes.UPDATE_USER_AVATAR:
            return {...state, avatar: action.payload, isAuthenticated: true}; // Смена аватарки у пользователя
        case UserActionTypes.CREATE_USER: // Создание нового пользователя и сохранение в state
            return {
                name: action.payload.name,
                surname: action.payload.surname,
                nickname: action.payload.nickname,
                email: action.payload.email,
                birthDate: action.payload.birthDate,
                gender: action.payload.gender,
                country: action.payload.country,
                isAuthenticated: true,
                avatar: action.payload.avatar
            };
        case UserActionTypes.DELETE_USER: // Выход из аккаунта
            return {state: initialState}
        default:
            return state
    }
}